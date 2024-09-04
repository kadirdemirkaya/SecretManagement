using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecretManagement;
using SecretManager.LS.Services;
using System.Text.Json.Serialization;

namespace SecretManager.LS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretManagerController : ControllerBase
    {
        ISecretsManagerService _secretsManagerService;
        ISecretManagementFactory _secretManagementFactory;

        #region V1
        //private readonly ISecretsManagerService _secretsManagerService;

        //public SecretManagerController(ISecretsManagerService secretsManagerService)
        //{
        //    _secretsManagerService = secretsManagerService;
        //}

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateSecret(string secretName, string secretValue)
        //{
        //    await _secretsManagerService.CreateSecretAsync(secretName, secretValue);
        //    return Ok("Secret created successfully.");
        //}

        //[HttpPost("create-person")]
        //public async Task<IActionResult> CreatePersonSecret(string secretName, string secretValue)
        //{
        //    Person person = new()
        //    {
        //        Name = "person",
        //        Password = secretValue
        //    };

        //    await _secretsManagerService.CreateSecretAsync(secretName, JsonConvert.SerializeObject(person));
        //    return Ok("Secret created successfully.");
        //}

        //[HttpGet("get")]
        //public async Task<IActionResult> GetSecret(string secretName)
        //{
        //    var secretValue = await _secretsManagerService.GetSecretValueAsStringAsync(secretName);
        //    return Ok(secretValue);
        //}


        //[HttpGet("get-person")]
        //public async Task<IActionResult> GetPersonSecret(string secretName)
        //{
        //    var secretValue = await _secretsManagerService.GetSecretValueAsync<Person>(secretName);
        //    return Ok(secretValue);
        //}


        //[HttpPut("update")]
        //public async Task<IActionResult> UpdateSecret(string secretName, string newSecretValue)
        //{
        //    await _secretsManagerService.UpdateSecretAsync(secretName, newSecretValue);
        //    return Ok("Secret updated successfully.");
        //}

        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteSecret(string secretName)
        //{
        //    await _secretsManagerService.DeleteSecretAsync(secretName);
        //    return Ok("Secret deleted successfully.");
        //}
        #endregion V1

        #region V2
        //public SecretManagerController(ISecretsManagerService secretsManagerService)
        //{
        //    _secretsManagerService = secretsManagerService;
        //}

        //[HttpPost("create")]
        //public async Task<ActionResult<bool>> CreateSecret(string secretName, string secretValue)
        //{
        //    bool res = await _secretsManagerService.CreateSecretAsync(secretName, secretValue);
        //    return Ok(res);
        //}

        //[HttpGet("get")]
        //public async Task<ActionResult<bool>> GetSecret(string secretName)
        //{
        //    string res = await _secretsManagerService.GetSecretValueAsStringAsync(secretName);
        //    return Ok(res);
        //}

        //[HttpDelete("delete")]
        //public async Task<ActionResult<bool>> DeleteSecret(string secretName)
        //{
        //    bool res = await _secretsManagerService.DeleteSecretAsync(secretName);
        //    return Ok(res);
        //}
        #endregion V2

        #region V3

        public SecretManagerController(ISecretManagementFactory secretManagementFactory)
        {
            _secretManagementFactory = secretManagementFactory;
            // _secretsManagerService = _secretManagementFactory.Create(SecretType.VaultSharp);
            _secretsManagerService = _secretManagementFactory.Create(SecretType.Aws);
        }


        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateSecret(string secretName, string secretValue)
        {
            bool res = await _secretsManagerService.CreateSecretAsync(secretName, secretValue);
            return Ok(res);
        }

        [HttpGet("get")]
        public async Task<ActionResult<bool>> GetSecret(string secretName)
        {
            string res = await _secretsManagerService.GetSecretValueAsStringAsync(secretName);
            return Ok(res);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> DeleteSecret(string secretName)
        {
            bool res = await _secretsManagerService.DeleteSecretAsync(secretName);
            return Ok(res);
        }
        #endregion V3

    }

    class Person
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
