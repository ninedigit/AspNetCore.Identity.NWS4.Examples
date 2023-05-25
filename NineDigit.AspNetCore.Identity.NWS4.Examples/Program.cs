using System.Net.Mime;
using System.Text;
using NineDigit.AspNetCore.Identity.NWS4;

HttpRequestMessage requestMessage;

string publicKey = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
string privateKey = "a7ffc6f8bf1ed76651c14756a061d662f580ff4de43b49fa82d80a4b80f8434a";

// Amount of time the signature will be valid. This is essential to avoid Replay attack.
TimeSpan requestTimeWindow = TimeSpan.FromSeconds(300);

var options = new AuthorizationHeaderSignerOptions
{
    AllowForwardedHostHeader = true,                // Use this options when tunneling connection (e.g. Ngrok)
    ForwardedHostHeaderName = "X-Forwarded-Host"    // No need to set this value. Default: X-Forwarded-Host
};

var signer = new AuthorizationHeaderSigner(options);

////////// # 1.1. GET
requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/users?id=1")
{
    Headers = { { "__tenant", "39ff67bf-0182-4903-c820-2dd75eed9d21" } }
};
await signer.SignRequestAsync(requestMessage, publicKey, privateKey);
await signer.ValidateSignatureAsync(requestMessage, privateKey, requestTimeWindow);

////////// # 1.2. POST

string body = @"{
    ""Key"": ""Value""
}";

requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost/users?id=1")
{
    Headers = { { "__tenant", "39ff67bf-0182-4903-c820-2dd75eed9d21" } },
    Content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json)
};
await signer.SignRequestAsync(requestMessage, publicKey, privateKey);
await signer.ValidateSignatureAsync(requestMessage, privateKey, requestTimeWindow);