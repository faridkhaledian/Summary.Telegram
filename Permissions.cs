namespace Summary.Telegram
{
    using Core.Security.Permissions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Permissions : IPermissionProvider
    {
        internal static Permission ManageTelegramSettings =
            new Permission(nameof(ManageTelegramSettings), "Manage Telegram Settings");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageTelegramSettings }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new []{ ManageTelegramSettings }
                }
            };
        }
    }
}