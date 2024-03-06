using System.Security.Cryptography;
using System.Text;

namespace Encryptor.WebUI.Helpers;


// 2B7DD13BD977779D8CC8BFEA18976
public class RsaEncryptionHelper
{
    private readonly RSA _rsa = RSA.Create();
    
    public RsaEncryptionHelper()
    {
        var privateKeyPem = @"-----BEGIN PRIVATE KEY-----
MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCuan+y++Eqc2Vx
3QIt9gv0r6rzfcxpsBRubcWCmI+8tqb40oflv5dViPSiCSWNgg5xKk7K8WTeEQQ6
NDg1IJ2OwoQ2dfzat5qlpfV9EeF3u8iTY/hyQaaYFwB77cV9t5Czb8oG/+IIOByo
rJVds9tAoKjssKUZ3W9IU7ffElZjZrbjoiy/H4z8u9fOq8IL9Zf3pHgzv2FxF4BP
Jamqr4s1VtMqGJ5g18wV1OD9gcz9pJOHHUVieZ0+xP4WD4+1wCv/uwEgIcqmEs0o
s6birHZL1X/CSqBSPc8e/+kkZyyzoF+MBWPzvAwmW32alIxz2ZV0Z+jJtoOrh/qV
qbrGAf+RAgMBAAECggEAEWYu4IJHg0ZZOZtgRwj7RtjKXzluraFi3GRHdoB1IFCB
RiOsamMrO91qeAqdDCmL+saLbyvXEd8VMqA4djZPeWkWqt8ozwHPY9RzMZuZyCm7
t9Zad71sWtI6mmJNF/46qWfOudWHbSX51+rFiMAzMFaGm3wAsFyaaBbv6gkohIhV
rrMvpIuV8X8JNI8/VNlOR6vExHd/3uWKi8vPrFEFTNQ1UE+WJDkVojcmx0t63jlP
8C/O4ofaJeCYRkAaM9+FXnM+jVNf0qZU+3JGYmy1R/B7L+LyrbG2uYTaBa1Ba9zG
sXD1HQ4dctK+MltBd4p+MWgHAQV+xuVmGGDkylpb8QKBgQDP9zY4UzLmnt0werJF
9SGmAJU51Y/+gannUC/+XYqnK/NA5iUDR86rlYCJbqQCceyMYJSYuePumygWOVL9
rHnu1/fTMG/sXnRktmTSFemgeCYfPyn8/KeZKn6E5xw8GbtfLJLA8uM4DakDf9vS
pilKDYT4UbZyKgq7/yodPXbgCQKBgQDWs4gL4yOtDLAY/OQKZvoyXClapldkVFRz
f97pBJQGLYfdTzkVFviWsgI/yrA+t5tlzaWtZieuv2KsSmazVyO2W5ei9HILx4yR
FK7K56OM5o7wqjN62ZmF0R+422YNaAyqYAd9nhLjgdsrmZ9FTxhQ91asBSH1AO3j
6xaZUvh1SQKBgAZuo/ur/xgI89hrAxaM1WSYAgWO6Gw7wHCKF2HrrL0s69InDCAE
2YyPDDGz/ViiA2n4FsB+h2E65UuCrGFyMzdC8MRUbDHIXhs7VPT2fopbDPrMblUH
z3s6SD1+FG57cUMpUsSq/oIeUgrsqnTidMZ4kpNHm7f+OuTDqJ7M5t9ZAoGAAfm2
570YR/BU8nXpNztJVAtLCh17sl2gRUvI5kX3grMKi/u9n7cNZH2Qzbt0sa8Iy///
ZUAKX249Xy50EXRczMG8/G/ZWMhmP7N8BDvrYlGAwTAftyKnafbJnu7N2pO5ghvO
FdbNf7BjLtyD/aRDqgMMlhqZ/GIczjsMgy6jQJkCgYATxkjdW/mnWLDDZAYm1V10
Gnm7YDMwX3gKdhpoYleeeapZiDlurkR3YAEvQezcnhv3NpjaKQtF1/3Q0fZJ0wBc
5FflaevHRjHTa0IrU5QIa48dLDx+jxn+cUCFx9kirkb7JcfJLZWtToPMLfCZt324
P2ogZgWBwweVPiz1/voEXw==
-----END PRIVATE KEY-----";

        _rsa.ImportFromPem(privateKeyPem.ToCharArray());
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