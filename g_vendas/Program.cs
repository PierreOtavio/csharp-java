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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormVendas()); // Altere para o nome do seu formulário principal
        }
    }
}
