using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RollerFate.Application.Abstractions.Auth;

namespace RollerFate.Infrastructure.Auth;

public class JwtSigningService : IJwtSigningService
{
    public SigningCredentials Credentials { get; set; }
    public ECDsaSecurityKey PublicKey { get; set; }
    public IReadOnlyList<SecurityKey> ValidationKeys { get; }
    public string CurrentKeyId { get; }

    public JwtSigningService(IOptions<AuthSettings> options)
    {
        var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

        var privateKeyBase64 = options.Value.SecretKey;
        if (!string.IsNullOrEmpty(privateKeyBase64))
        {
            var keyBytes = Convert.FromBase64String(privateKeyBase64);
            ecdsa.ImportPkcs8PrivateKey(keyBytes, out _);
        }
        else
        {
            // dev-режим — генерируем новый ключ при каждом старте
            ecdsa.GenerateKey(ECCurve.NamedCurves.nistP256);
            Console.WriteLine("Новый EC ключ сгенерирован:");
            Console.WriteLine(Convert.ToBase64String(ecdsa.ExportPkcs8PrivateKey()));
        }

        CurrentKeyId = DateTime.UtcNow.ToString("yyyy-MM");

        var privateSecurityKey = new ECDsaSecurityKey(ecdsa) { KeyId = CurrentKeyId };
        Credentials = new SigningCredentials(privateSecurityKey, SecurityAlgorithms.EcdsaSha256);

        var publicEcdsa = ECDsa.Create(ecdsa.ExportParameters(includePrivateParameters: false));
        PublicKey = new ECDsaSecurityKey(publicEcdsa) { KeyId = CurrentKeyId };

        ValidationKeys = [PublicKey];
    }
}
