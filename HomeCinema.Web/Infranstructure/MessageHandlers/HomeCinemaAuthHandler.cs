using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HomeCinema.Web.Infranstructure.Extensions;

namespace HomeCinema.Web.Infranstructure.MessageHandlers
{
    internal class HomeCinemaAuthHandler : DelegatingHandler
    {
        private IEnumerable<string> _authHeaderValues;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                request.Headers.TryGetValues("Authorization", out _authHeaderValues);
                if (_authHeaderValues == null)
                {
                    return base.SendAsync(request, cancellationToken);
                }

                var tokens = _authHeaderValues.FirstOrDefault();
                tokens = tokens?.Replace("Basic", "").Trim();

                if (!string.IsNullOrEmpty(tokens))
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValues = decodedString.Split(':');
                    var membershipService = request.GetMembershipService();
                    var membershipCtx = membershipService.ValidateUser(tokensValues[0], tokensValues[2]);

                    if (membershipCtx.User != null)
                    {
                        var principal = membershipCtx.Principal;
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }
    }
}