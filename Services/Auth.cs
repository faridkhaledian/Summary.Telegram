namespace Summary.Telegram.Services
{
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;
    using Summary.Telegram.Settings;
    using TL;

    public interface IAuthorizeService
    {
        Task SendCodeAsync();
        Task<bool> IsLoggedInAsync();
    }

    public class AuthorizeService : IAuthorizeService
    {
        private readonly TelegramSettings _options;
        private readonly ITelegramClientService _client;

        public AuthorizeService(IOptions<TelegramSettings> options,
            ITelegramClientService client)
        {
            _options = options.Value;
            _client = client;
        }

        public async Task SendCodeAsync()
        {
            var client = await _client.GetClientAsync();

            await client.Auth_SendCode(
                _options.Mobile,
                int.Parse(_options.Api_Id),
                _options.Api_Hash,
                new CodeSettings());
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var client = await _client.GetClientAsync();

            try
            {
                var users = await client.Users_GetUsers(new InputUserBase[] { new InputUserSelf() });
                return users.Length > 0;
            }
            catch (RpcException)
            {
                return false;
            }
        }
    }
}