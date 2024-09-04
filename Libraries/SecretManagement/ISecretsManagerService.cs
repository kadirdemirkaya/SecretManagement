namespace SecretManagement
{
    public interface ISecretsManagerService
    {
        Task<bool> CreateSecretAsync<T>(string secretName, T secretValue);
        Task<bool> DeleteSecretAsync(string secretName);
        Task<string> GetSecretValueAsStringAsync(string secretName);
        Task<T> GetSecretValueAsync<T>(string secretName);
    }
}