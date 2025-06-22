using g_vendas.BLL;
using g_vendas.Models;
using System.Collections.Generic;

namespace g_vendas.Controllers
{
    public class PizzaSaboresController
    {
        /// <summary>
        /// Retorna todos os sabores de pizza cadastrados.
        /// </summary>
        public List<PizzaSabores> ObterTodosSabores()
        {
            return PizzaSaboresBLL.ObterTodosSabores();
        }

        /// <summary>
        /// Exporta os sabores para um arquivo Excel.
        /// </summary>
        public bool ExportarSaboresParaExcel()
        {
            var lista = ObterTodosSabores();
            return PizzaSaboresBLL.ExportarParaExcel(lista);
        }

        /// <summary>
        /// Calcula a proporção média dos sabores cadastrados.
        /// </summary>
        //public decimal CalcularProporcaoMedia()
        //{
        //    return PizzaSaboresBLL.CalcularProporcaoMedia();
        //}

        public void InsertProporcao(int id_item, int id_sabor, string pizzaProporcao)
        {
            PizzaSaboresBLL.InserirProporcao(id_item, id_sabor, pizzaProporcao);
        }
    }
}
