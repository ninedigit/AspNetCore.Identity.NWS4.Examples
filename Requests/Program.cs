using System.Net.Mime;
using System.Text;
using NineDigit.AspNetCore.Identity.NWS4;

//////////////////////////////////////////////////

// Authentication keys
var publicKey = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
var privateKey = "a7ffc6f8bf1ed76651c14756a061d662f580ff4de43b49fa82d80a4b80f8434a";

// Amount of time the signature will be valid. This is essential to avoid Replay attack.
var requestTimeWindow = TimeSpan.FromSeconds(300);

var options = new AuthorizationHeaderSignerOptions
{
    // Use this options when tunneling connection (e.g. Ngrok)
    AllowForwardedHostHeader = true,
    // No need to set this value. Default: X-Forwarded-Host
    ForwardedHostHeaderName = "X-Forwarded-Host"
};

var signer = new AuthorizationHeaderSigner(options);

//////////////////////////////////////////////////

HttpRequestMessage requestMessage;
string body;

// 1. GET request ////////////////////////////////

requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost/users?id=1")
{
    Headers = { { "__tenant", "39ff67bf-0182-4903-c820-2dd75eed9d21" } }
};
await signer.SignRequestAsync(requestMessage, publicKey, privateKey);
//await signer.ValidateSignatureAsync(requestMessage, privateKey, requestTimeWindow);

// 2. POST request ///////////////////////////////

body = @"{
    ""Key"": ""Value""
}";

requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost/users?id=1")
{
    Headers = { { "__tenant", "39ff67bf-0182-4903-c820-2dd75eed9d21" } },
    Content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json)
};
await signer.SignRequestAsync(requestMessage, publicKey, privateKey);
//await signer.ValidateSignatureAsync(requestMessage, privateKey, requestTimeWindow);

// 3. PUT request ////////////////////////////////

body = @"{
    ""Key"": ""Value""
}";

requestMessage = new HttpRequestMessage(HttpMethod.Put, "http://localhost/users?id=1")
{
    Headers = { { "__tenant", "39ff67bf-0182-4903-c820-2dd75eed9d21" } },
    Content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json)
};
await signer.SignRequestAsync(requestMessage, publicKey, privateKey);
//await signer.ValidateSignatureAsync(requestMessage, privateKey, requestTimeWindow);

// 3. DELETE request /////////////////////////////

requestMessage = new HttpRequestMessage(HttpMethod.Delete, "http://localhost/users?id=1")
{
    Headers = { { "__tenant", "39ff67bf-0182-4903-c820-2dd75eed9d21" } }
};
await signer.SignRequestAsync(requestMessage, publicKey, privateKey);
//await signer.ValidateSignatureAsync(requestMessage, privateKey, requestTimeWindow);