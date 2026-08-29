namespace Summary.Telegram.Settings
{
    using Core.DisplayManagement.Entities;
    using Core.DisplayManagement.Handlers;
    using Core.DisplayManagement.Views;
    using Core.Entities;
    using Core.Environment.Shell;
    using Core.Settings;
    using Core.Workflows;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services;
    using System.Threading.Tasks;

    public class TelegramSettings
    {
        public string Token { get; set; }
        public string Mobile { get; set; }
        public string Api_Id { get; set; }
        public string Api_Hash { get; set; }
    }

    public class TelegramSettingsDisplayDriver : SectionDisplayDriver<ISite,
        TelegramSettings>
    {
        private readonly IShellHost _host;
        private readonly ShellSettings _shell;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IAuthorizationService _authorize;
        private readonly IAuthorizeService _auth;

        public TelegramSettingsDisplayDriver(IShellHost host,
            ShellSettings settings,
            IHttpContextAccessor httpContext,
            IAuthorizationService authorize,
            IAuthorizeService auth)
        {
            _host = host;
            _shell = settings;
            _httpAccessor = httpContext;
            _authorize = authorize;
            _auth = auth;
        }

        public override async Task<IDisplayResult> EditAsync(TelegramSettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageWorkflows))
            {
                return null;
            }

            var init = Initialize<TelegramSettings>("TelegramSettings_Edit", model =>
            {
                model.Token = settings.Token;
                model.Mobile = settings.Mobile;
                model.Api_Id = settings.Api_Id;
                model.Api_Hash = settings.Api_Hash;
            });
            return init.Location("Content:5").OnGroup("Telegram");
        }

        public override async Task<IDisplayResult> UpdateAsync(TelegramSettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageWorkflows))
            {
                return null;
            }
            if (context.GroupId == "Telegram")
            {
                await context.Updater.TryUpdateModelAsync(settings, Prefix);
                await _host.ReloadShellContextAsync(_shell);

                if (string.IsNullOrWhiteSpace(settings.Api_Hash) is false)
                    if (await _auth.IsLoggedInAsync() is false)
                        await _auth.SendCodeAsync();
            }

            return await EditAsync(settings, context);
        }
    }

    public class TelegramSettingsConfiguration : IConfigureOptions<TelegramSettings>
    {
        private readonly ISiteService _site;
        private readonly ILogger<TelegramSettingsConfiguration> _logger;

        public TelegramSettingsConfiguration(ISiteService site,
            ILogger<TelegramSettingsConfiguration> logger)
        {
            _site = site;
            _logger = logger;
        }

        public void Configure(TelegramSettings options)
        {
            var settings = _site.GetSiteSettingsAsync().GetAwaiter().GetResult().As<TelegramSettings>();
            options.Token = settings.Token;
            options.Mobile = settings.Mobile;
            options.Api_Id = settings.Api_Id;
            options.Api_Hash = settings.Api_Hash;
        }
    }
}