using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasyAuthProvider;

public class EasyAuthAuthenticationHandler : AuthenticationHandler<EasyAuthOptions>
{
    public EasyAuthAuthenticationHandler(IOptionsMonitor<EasyAuthOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        AuthenticateResult result = AuthenticateResult.NoResult();

        if (Request.Headers.ContainsKey("X-MS-CLIENT-PRINCIPAL-ID"))
        {
            var principalId = Request.Headers["X-MS-CLIENT-PRINCIPAL-ID"][0];
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, principalId));

            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name, ClaimTypes.NameIdentifier, "");

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            result = AuthenticateResult.Success(ticket);
        }

        return Task.FromResult(result);
    }
}