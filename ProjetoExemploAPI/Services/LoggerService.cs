using Microsoft.Extensions.Logging;
using System;

namespace ProjetoExemploAPI.Services
{
    public class LoggerService : ILogger, IDisposable
    {

        #region Métodos

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }

        #endregion Métodos
    }
}
