using Serilog;
using Serilog.Events;
using System;

namespace g_vendas.Logger
{
    internal class loggerC_
    {
        private static bool _isInitialized = false;

        // Inicializa o logger apenas uma vez
        public static void Inicializar(string caminhoArquivo = "logs/app.txt", LogEventLevel nivelMinimo = LogEventLevel.Information)
        {
            if (_isInitialized)
                return;

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(caminhoArquivo));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(nivelMinimo)
                .WriteTo.Console()
                .WriteTo.File(caminhoArquivo, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            _isInitialized = true;
        }

        // Métodos utilitários para log
        public static void Info(string mensagem, params object[] args) =>
            Log.Information(mensagem, args);

        public static void Error(string mensagem, Exception ex = null, params object[] args)
        {
            if (ex != null)
                Log.Error(ex, mensagem, args);
            else
                Log.Error(mensagem, args);
        }

        public static void Debug(string mensagem) => Log.Debug(mensagem);

        // Finaliza o logger (chame ao encerrar o app)
        public static void Fechar() => Log.CloseAndFlush();
    }
}
