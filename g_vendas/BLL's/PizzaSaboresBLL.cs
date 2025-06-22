using g_vendas.DAL_s;
using g_vendas.Logger;
using g_vendas.Models;
using System.Collections.Generic;

namespace g_vendas.BLL
{
    internal static class PizzaSaboresBLL
    {
        /// <summary>
        /// Obtém todos os sabores de pizza do repositório de dados
        /// </summary>
        public static List<PizzaSabores> ObterTodosSabores()
        {
            return PizzaSaboresDAL.search();
        }

        //public static decimal CalcularProporcaoMedia()
        //{
        //    //var listaPsab = PizzaSaboresDAL.search();
        //    return PizzaSaboresDAL.CalcularProporcaoMedia();
        //}
        /// <summary>
        /// Exporta dados para Excel com tratamento de erros e validações
        /// </summary>
        /// <param name="sabores">Lista de sabores a ser exportada</param>
        /// <returns>True se a exportação foi bem sucedida</returns>
        public static bool ExportarParaExcel(List<PizzaSabores> sabores)
        {
            if (sabores == null || sabores.Count == 0)
            {
                loggerC_.Error("Tentativa de exportação com lista vazia");
                return false;
            }

            try
            {
                PizzaSaboresDAL.exportToExcel(sabores);
                loggerC_.Info("Exportação para Excel realizada com sucesso");
                return true;
            }
            catch (System.Exception ex)
            {
                loggerC_.Error("Erro na exportação para Excel: " + ex.Message, ex);
                return false;
            }
        }

        // Adicione aqui outros métodos de negócio conforme necessário
        public static void InserirProporcao(int id_item, int id_sabor, string pizzaProporcao)
        {
            PizzaSaboresDAL.insertProporcao(id_item, id_sabor, pizzaProporcao);
        }
    }
}
