using g_vendas.Logger;
using g_vendas.UI;
using System;
using System.Windows.Forms;

namespace g_vendas
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para a aplicação.
        /// </summary>
        [STAThread]

        static void startLogg(loggerC_ logger)
        {
            loggerC_.Inicializar();

        }

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            loggerC_ logger = new loggerC_();
            try
            {
                try
                {
                    if (logger == null)
                    {
                        loggerC_.Info("objeto recebido como nulo, verificar passagem");
                    }
                }
                catch (Exception ex)
                {
                    loggerC_.Error($"erro ao identificar objeto {ex}");
                }
                startLogg(logger);
            }
            catch (Exception e)
            {
                loggerC_.Error($"erro ao inicializar {e}");
            }

            Application.Run(new FormLogin());
        }
    }
}
