using Microsoft.Extensions.Configuration;

namespace SecretManagement
{
    public interface ISecretManagementFactory
    {
        ISecretsManagerService Create(SecretType secretType);
    }
}