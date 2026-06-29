using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace RegistroVentas.Api.Security;

public sealed class Aes256CryptoService
{
    private readonly byte[] _key;

    public Aes256CryptoService(IOptions<CryptoOptions> options)
    {
        _key = Convert.FromBase64String(options.Value.AesKeyBase64);

        if (_key.Length != 32)
        {
            throw new InvalidOperationException("La llave AES debe ser de 32 bytes para AES-256.");
        }
    }

    public string Decrypt(string encryptedBase64)
    {
        var payload = Convert.FromBase64String(encryptedBase64);

        if (payload.Length <= 16)
        {
            throw new CryptographicException("El secreto cifrado no contiene IV y ciphertext válidos.");
        }

        var iv = payload[..16];
        var cipherText = payload[16..];

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = _key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor();

        var plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    public string EncryptForDev(string value)
    {
        var plainBytes = Encoding.UTF8.GetBytes(value);

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Key = _key;
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();

        var cipherText = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var payload = new byte[aes.IV.Length + cipherText.Length];

        Buffer.BlockCopy(aes.IV, 0, payload, 0, aes.IV.Length);
        Buffer.BlockCopy(cipherText, 0, payload, aes.IV.Length, cipherText.Length);

        return Convert.ToBase64String(payload);
    }
}