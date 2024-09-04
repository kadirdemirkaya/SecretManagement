using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace SecretManagement
{
    public class AwsSecretsManagerService : ISecretsManagerService
    {
        private readonly IAmazonSecretsManager _secretsManager;
        private readonly string environment;

        public AwsSecretsManagerService(IConfiguration configuration)
        {
            _secretsManager = new AmazonSecretsManagerClient(new AmazonSecretsManagerConfig
            {
                ServiceURL = "http://localhost:4566",
                AuthenticationRegion = "eu-central-1",
            });
            environment = configuration["Environment"] ?? "Development";
        }

        public async Task<bool> CreateSecretAsync<T>(string secretName, T secretValue)
        {
            try
            {
                var listRequest = new ListSecretsRequest();
                var listResponse = await _secretsManager.ListSecretsAsync(listRequest);

                var existingSecret = listResponse.SecretList
                    .FirstOrDefault(s => s.Name == secretName
                                     && s.Tags.Any(tag => tag.Key == "Environment" && tag.Value == environment));

                if (existingSecret != null)
                {
                    throw new InvalidOperationException($"A secret with the name {secretName} already exists for environment {environment}");
                }

                var createSecretRequest = new CreateSecretRequest
                {
                    Name = secretName,
                    SecretString = JsonSerializer.Serialize(secretValue),
                    Tags = new List<Tag>()
                };

                createSecretRequest.Tags.Add(new Tag { Key = "Environment", Value = environment });

                var response = await _secretsManager.CreateSecretAsync(createSecretRequest);

                if (!string.IsNullOrEmpty(response.ARN))
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating secret {environment}:{secretName} {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteSecretAsync(string secretName)
        {
            try
            {
                var listRequest = new ListSecretsRequest();
                var listResponse = await _secretsManager.ListSecretsAsync(listRequest);

                var secret = listResponse.SecretList
                    .FirstOrDefault(s => s.Name == secretName
                                     && s.Tags.Any(tag => tag.Key == "Environment" && tag.Value == environment))
                    ?? throw new InvalidOperationException($"Secret {secretName} not found for environment {environment}");

                var deleteRequest = new DeleteSecretRequest
                {
                    SecretId = secret.ARN,
                    ForceDeleteWithoutRecovery = true
                };

                var response = await _secretsManager.DeleteSecretAsync(deleteRequest);

                if (!string.IsNullOrEmpty(response.ARN))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting {environment}:{secretName} {ex.Message}", ex);
            }
        }

        public async Task<string> GetSecretValueAsStringAsync(string secretName)
        {
            try
            {
                var request = new GetSecretValueRequest
                {
                    SecretId = secretName
                };

                var listRequest = new ListSecretsRequest();
                var listResponse = await _secretsManager.ListSecretsAsync(listRequest);
                var secret = listResponse.SecretList
                    .FirstOrDefault(s => s.Name == secretName
                                    && s.Tags.Any(tag => tag.Key == "Environment" && tag.Value == environment))
                    ?? throw new InvalidOperationException($"Secret {secretName} not found for environment {environment}");
                request.SecretId = secret.ARN;

                return (await _secretsManager.GetSecretValueAsync(request)).SecretString;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching {environment}:{secretName} {ex.Message}", ex);
            }
        }

        public async Task<T> GetSecretValueAsync<T>(string secretName)
        {
            try
            {
                var request = new GetSecretValueRequest
                {
                    SecretId = secretName
                };

                var listRequest = new ListSecretsRequest();
                var listResponse = await _secretsManager.ListSecretsAsync(listRequest);
                var secret = listResponse.SecretList
                    .FirstOrDefault(s => s.Name == secretName
                                    && s.Tags.Any(tag => tag.Key == "Environment" && tag.Value == environment))
                    ?? throw new InvalidOperationException($"Secret {secretName} not found for environment {environment}");
                request.SecretId = secret.ARN;

                return JsonSerializer.Deserialize<T>((await _secretsManager.GetSecretValueAsync(request)).SecretString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching {environment}:{secretName} {ex.Message}", ex);
            }
        }
    }
}
