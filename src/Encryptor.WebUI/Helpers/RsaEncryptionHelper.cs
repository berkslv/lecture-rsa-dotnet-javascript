using System.Security.Cryptography;
using System.Text;

namespace Encryptor.WebUI.Helpers;


public class RsaEncryptionHelper
{
    private readonly RSA _rsa = RSA.Create();
    
    public RsaEncryptionHelper(IConfiguration configuration)
    {
        var privateKeyPem = configuration["RsaKey"];
        if (string.IsNullOrEmpty(privateKeyPem)) throw new ArgumentNullException(nameof(privateKeyPem));
        ImportKey(privateKeyPem);
    }
   
    
    public void ImportKey(string privateKey)
    {
        
        _rsa.ImportFromPem(privateKey.ToCharArray());
    }
    
    public string GetPublicKey()
    { 
        var publicKey = _rsa.ExportSubjectPublicKeyInfoPem();
        return publicKey;
    }
    
    public string GetPrivateKey()
    {
        var privateKey = _rsa.ExportPkcs8PrivateKeyPem();
        return privateKey;
    }
    
    public string Encrypt(string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        var encryptedData = _rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
        return Convert.ToBase64String(encryptedData);
    }
    
    public string Decrypt(string data)
    {
        var dataBytes = Convert.FromBase64String(data);
        var decryptedData = _rsa.Decrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);
        return Encoding.UTF8.GetString(decryptedData);
    }

}