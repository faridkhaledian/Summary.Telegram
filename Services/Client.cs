namespace Summary.Telegram.Services
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using Summary.Telegram.Settings;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using WTelegram;

    public interface ITelegramClientService
    {
        Task<Client> GetClientAsync();
    }

    public class TelegramClientService : ITelegramClientService
    {
        private Client _client;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private readonly TelegramSettings _options;

        private readonly IWebHostEnvironment _environment;

        public TelegramClientService(
            IOptions<TelegramSettings> options,
            IWebHostEnvironment environment)
        {
            _options = options.Value;
            _environment = environment;
        }

        public async Task<Client> GetClientAsync()
        {
            if (_client != null) return _client;

            await _lock.WaitAsync();

            try
            {
                if (_client != null) return _client;

                _client = new Client(what => what switch
                {
                    "api_id" => _options.Api_Id,
                    "api_hash" => _options.Api_Hash,
                    "session_pathname" => Path.Combine(_environment.ContentRootPath, "App_Data", "telegram.session"),
                    _ => null
                });

                await _client.ConnectAsync();

                return _client;
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}