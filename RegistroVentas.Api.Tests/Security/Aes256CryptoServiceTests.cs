using Microsoft.Extensions.Options;
using RegistroVentas.Api.Security;

namespace RegistroVentas.Api.Tests.Security;

public sealed class Aes256CryptoServiceTests
{
    [Fact]
    public void EncryptForDev_And_Decrypt_ReturnsOriginalValue()
    {
        // Arrange
        var service = CreateCryptoService();
        const string plainText = "mi-palabra-secreta";

        // Act
        var encrypted = service.EncryptForDev(plainText);
        var decrypted = service.Decrypt(encrypted);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(encrypted));
        Assert.NotEqual(plainText, encrypted);
        Assert.Equal(plainText, decrypted);
    }

    [Fact]
    public void Decrypt_WithInvalidBase64_ThrowsFormatException()
    {
        // Arrange
        var service = CreateCryptoService();

        // Act + Assert
        Assert.Throws<FormatException>(() =>
            service.Decrypt("esto-no-es-base64")
        );
    }

    private static Aes256CryptoService CreateCryptoService()
    {
        var options = Options.Create(new CryptoOptions
        {
            AesKeyBase64 = "MDEyMzQ1Njc4OWFiY2RlZjAxMjM0NTY3ODlhYmNkZWY="
        });

        return new Aes256CryptoService(options);
    }
}