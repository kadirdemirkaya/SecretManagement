using Microsoft.Extensions.Configuration;

namespace SecretManagement
{
    public class SecretManagementFactory : ISecretManagementFactory
    {
        private readonly IConfiguration _configuration;

        public SecretManagementFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISecretsManagerService Create(SecretType secretType)
        {
            return secretType switch
            {
                SecretType.Aws => new AwsSecretsManagerService(_configuration),
                SecretType.VaultSharp => new VaultSharpSecretManagementService(_configuration),
                _ => new AwsSecretsManagerService(_configuration),
            };
        }
    }
}
