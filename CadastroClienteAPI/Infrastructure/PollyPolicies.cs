using Polly.Extensions.Http;
using Polly;

namespace CadastroClienteAPI.Infrastructure
{
    static class PollyPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        Console.WriteLine($"Tentativa {retryAttempt} falhou. Tentando novamente em {timespan.TotalSeconds} segundos.");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30),
                    onBreak: (outcome, timespan) =>
                    {
                        Console.WriteLine("Circuito interrompido. Nenhuma requisição será feita pelos próximos 30 segundos.");
                    },
                    onReset: () =>
                    {
                        Console.WriteLine("Circuito fechado. Requisições sendo feitas novamente.");
                    });
        }
    }

}
