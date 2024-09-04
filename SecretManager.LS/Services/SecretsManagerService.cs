using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace SecretManager.LS.Services
{
    //public class SecretsManagerService : ISecretsManagerService
    //{
    //    private readonly IAmazonSecretsManager _secretsManager;

    //    public SecretsManagerService()
    //    {
    //        _secretsManager = new AmazonSecretsManagerClient(new AmazonSecretsManagerConfig
    //        {
    //            ServiceURL = "http://localhost:4566",
    //            AuthenticationRegion = "eu-central-1",
    //        });
    //    }

    //    // Generic metot
    //    public async Task<T> GetSecretValueAsync<T>(string secretName)
    //    {
    //        var request = new GetSecretValueRequest
    //        {
    //            SecretId = secretName
    //        };

    //        try
    //        {
    //            var response = await _secretsManager.GetSecretValueAsync(request);

    //            return JsonConvert.DeserializeObject<T>(response.SecretString);
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"An error occurred: {e.Message}");
    //            throw;
    //        }
    //    }

    //    public async Task<string> GetSecretValueAsStringAsync(string secretName)
    //    {
    //        var request = new GetSecretValueRequest
    //        {
    //            SecretId = secretName
    //        };

    //        try
    //        {
    //            var response = await _secretsManager.GetSecretValueAsync(request);
    //            return response.SecretString;
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"An error occurred: {e.Message}");
    //            throw;
    //        }
    //    }

    //    public async Task CreateSecretAsync<T>(string secretName, T secretValue)
    //    {
    //        var secretString = JsonConvert.SerializeObject(secretValue);

    //        var request = new CreateSecretRequest
    //        {
    //            Name = secretName,
    //            SecretString = secretString
    //        };

    //        try
    //        {
    //            var response = await _secretsManager.CreateSecretAsync(request);
    //            Console.WriteLine($"Secret created: {response.ARN}");
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"An error occurred: {e.Message}");
    //            throw;
    //        }
    //    }

    //    public async Task DeleteSecretAsync(string secretName)
    //    {
    //        var request = new DeleteSecretRequest
    //        {
    //            SecretId = secretName
    //        };

    //        try
    //        {
    //            var response = await _secretsManager.DeleteSecretAsync(request);
    //            Console.WriteLine($"Secret deleted: {response.ARN}");
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"An error occurred: {e.Message}");
    //            throw;
    //        }
    //    }
    //    public async Task UpdateSecretAsync<T>(string secretName, T newSecretValue)
    //    {
    //        var secretString = JsonConvert.SerializeObject(newSecretValue);

    //        var request = new PutSecretValueRequest
    //        {
    //            SecretId = secretName,
    //            SecretString = secretString
    //        };

    //        try
    //        {
    //            var response = await _secretsManager.PutSecretValueAsync(request);
    //            Console.WriteLine($"Secret updated: {response.ARN}");
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"An error occurred: {e.Message}");
    //            throw;
    //        }
    //    }
    //}
}
