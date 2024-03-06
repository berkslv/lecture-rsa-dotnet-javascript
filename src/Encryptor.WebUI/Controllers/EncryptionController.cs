using System.Security.Cryptography;
using Encryptor.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Encryptor.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EncryptionController : ControllerBase
{
    private readonly RsaEncryptionHelper _rsaEncryptionHelper;
    
    public EncryptionController(RsaEncryptionHelper rsaEncryptionHelper)
    {
        _rsaEncryptionHelper = rsaEncryptionHelper;
    }
    
    [HttpPost("Encrypt")]
    public string Encrypt([FromBody] EncryptionRequest request)
    {
        return _rsaEncryptionHelper.Encrypt(request.Data);
    }
    
    [HttpPost("Decrypt")]
    public string Decrypt([FromBody] EncryptionRequest request)
    {
        return _rsaEncryptionHelper.Decrypt(request.Data);
    }
    
    [HttpGet("PublicKey")]
    public string PublicKey()
    {
        return _rsaEncryptionHelper.GetPublicKey();
    }
    
    [HttpGet("PrivateKey")]
    public string PrivateKey()
    {
        return _rsaEncryptionHelper.GetPrivateKey();
    }
    
    [HttpPost("ImportKey")]
    public void ImportKey([FromBody] ImportKeyRequest request)
    {
        _rsaEncryptionHelper.ImportKey(request.PrivateKey);
    }
    
}

public record ImportKeyRequest
{
    public string PrivateKey { get; init; } = null!;
}

public record EncryptionRequest
{
    public string Data { get; init; } = null!;
}