using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SegurosInfinite.Services.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            System.Text.Encodings.Web.UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Verificar si el encabezado de autorización está presente
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                try
                {
                    // Decodificar las credenciales de base64
                    var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':');
                    if (credentials.Length == 2)
                    {
                        var username = credentials[0];
                        var password = credentials[1];

                        // Validar las credenciales
                        if (username == "user_root" && password == "admin")
                        {
                            var claims = new[] { new Claim(ClaimTypes.Name, username) };
                            var identity = new ClaimsIdentity(claims, "Basic");
                            var principal = new ClaimsPrincipal(identity);
                            var ticket = new AuthenticationTicket(principal, "BasicAuthentication");

                            return Task.FromResult(AuthenticateResult.Success(ticket));
                        }
                    }
                    else
                    {
                        return Task.FromResult(AuthenticateResult.Fail("Invalid credentials format"));
                    }
                }
                catch (FormatException)
                {
                    // Captura el error si la cadena base64 es inválida
                    return Task.FromResult(AuthenticateResult.Fail("Invalid authorization header format"));
                }
            }

            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
        }
    }
}
