using g_vendas.DAL_s;
using g_vendas.Logger;
using g_vendas.Models;
using System;
using System.Collections.Generic;

namespace g_vendas.BLL_s
{
    internal class SaboresPizzaBLL
    {
        public List<SaboresPizza> searchSabores()
        {
            try
            {
                loggerC_.Info("Trying to search all kinds of flavours");
                return SaboresPizzaDAL.buscarSabores();
            }
            catch (Exception e)
            {
                loggerC_.Error($"{e}");
                throw new ApplicationException("Falha ao carregar produtos do banco.", e);
            }
        }

        public void insertSabor(SaboresPizza sabores)
        {
            try
            {
                if (string.IsNullOrEmpty(sabores.nome))
                {
                    loggerC_.Error("Tentativa de inserir sabor com nome vazio.");
                    throw new ArgumentException("O nome do sabor é obrigatório.");
                }

                if (string.IsNullOrWhiteSpace(sabores.descricao))
                {
                    loggerC_.Error("Tentativa de inserir sabor com descrição vazio.");
                    throw new ArgumentException("A descrição do sabor é obrigatório.");
                }
                SaboresPizzaDAL.insertSabor(sabores);
            }
            catch (Exception ex)
            {
                loggerC_.Error($"errors: {ex}");
            }
        }

        public static bool ExportarParaExcel(List<SaboresPizza> sabores)
        {
            if (sabores == null || sabores.Count == 0)
            {
                loggerC_.Error("Tentativa de exportação com lista vazia");
                return false;
            }

            try
            {
                SaboresPizzaDAL.exportToExcel(sabores);
                loggerC_.Info("Exportação para Excel realizada com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                loggerC_.Error("Erro na exportação para Excel: " + ex.Message, ex);
                return false;
            }
        }
    }
}
