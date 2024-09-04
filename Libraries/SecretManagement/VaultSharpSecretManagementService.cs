
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;

namespace SecretManagement
{
    public class VaultSharpSecretManagementService : ISecretsManagerService
    {
        private readonly IVaultClient _vaultClient;
        private readonly string environment;
        public VaultSharpSecretManagementService(IConfiguration configuration)
        {
            var authMethod = new TokenAuthMethodInfo("root");
            var vaultClientSettings = new VaultClientSettings("http://localhost:8200", authMethod); // URL sadece Vault sunucusunun adresi
            _vaultClient = new VaultClient(vaultClientSettings);
            environment = configuration["Environment"] ?? "Development";
        }
        public async Task<bool> CreateSecretAsync<T>(string secretName, T secretValue)
        {
            try
            {
                var secretData = new Dictionary<string, object>
                {
                    { "data", secretValue },
                };

                await _vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(CreateSecretPath(secretName), secretValue);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating secret: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSecretAsync(string secretName)
        {
            try
            {
                await _vaultClient.V1.Secrets.KeyValue.V2.DeleteSecretAsync(CreateSecretPath(secretName));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting secret: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetSecretValueAsStringAsync(string secretName)
        {
            try
            {
                var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(CreateSecretPath(secretName));
                return JsonConvert.SerializeObject(secret.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting secret: {ex.Message}");
                return null;
            }
        }

        public async Task<T> GetSecretValueAsync<T>(string secretName)
        {
            try
            {
                var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(CreateSecretPath(secretName));
                var secretJson = JsonConvert.SerializeObject(secret.Data);
                return JsonConvert.DeserializeObject<T>(secretJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting secret: {ex.Message}");
                return default;
            }
        }

        private string CreateSecretPath(string secretName)
            => $"{environment}/{secretName}";
    }
}
