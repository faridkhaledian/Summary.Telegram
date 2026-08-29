namespace Summary.Telegram.Services
{
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;
    using Summary.Telegram.Settings;
    using TL;
    using WTelegram;
    using Microsoft.AspNetCore.Hosting;

    public interface IAuthorizeService
    {
        Task SendCodeAsync();
        Task<bool> IsLoggedInAsync();
    }

    public class AuthorizeService : IAuthorizeService
    {
        private readonly Client _client;
        private readonly TelegramSettings _options;

        public AuthorizeService(IOptions<TelegramSettings> options,
            IWebHostEnvironment environment)
        {
            _options = options.Value;

            _client = new Client(what => what switch
            {
                "api_id" => _options.Api_Id,
                "api_hash" => _options.Api_Hash,
                "session_pathname" => environment.ContentRootPath,
                _ => null
            });
        }

        public async Task SendCodeAsync()
        {
            await _client.ConnectAsync();

            await _client.Auth_SendCode(
                _options.Mobile,
                int.Parse(_options.Api_Id),
                _options.Api_Hash,
                new CodeSettings());
        }

        public async Task<bool> IsLoggedInAsync()
        {
            await _client.ConnectAsync();

            try
            {
                var users = await _client.Users_GetUsers(new InputUserBase[] { new InputUserSelf() });
                return users.Length > 0;
            }
            catch (RpcException)
            {
                return false;
            }
        }
    }
}