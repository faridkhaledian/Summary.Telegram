namespace Summary.Telegram
{
    using Core.DisplayManagement.Handlers;
    using Core.Modules;
    using Core.Navigation;
    using Core.Security.Permissions;
    using Core.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Summary.Telegram.Settings;

    [Feature(Telegram.Features.Telegram)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, Menu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, TelegramSettingsDisplayDriver>();
            services.AddTransient<IConfigureOptions<TelegramSettings>, TelegramSettingsConfiguration>();
        }
    }
}