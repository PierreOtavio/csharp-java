using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using Teste1.Data.Query;

namespace g_vendas.DAL_s
{
    internal class ItensVendasDAL
    {
        public static List<ItensVendas> buscarItensVendas()
        {
            List<ItensVendas> listaItens = new List<ItensVendas>();
            Action<ItensVendas> storeInList = i => listaItens.Add(i);

            var consultor = new ConsultorUniversal<ItensVendas>
                (
                tabela: "itens_venda",
                campos: "*",
                armazenador: storeInList
                );

            consultor.BuscarEArmazenar();
            return listaItens;
        }

        public static void insertItem(ItensVendas itens)
        {
            var valores = new Dictionary<string, object>
            {
                {"id_item", itens.Id_Item },
                {"id_venda", itens.Id_Venda },
                {"id_produto", itens.Id_Produto },
                {"quantidade", itens.Quantidade },
                {"preco_unitario", itens.Preco_Unitario },
                {"observacao", itens.Observacao }
            };

            var consultor = new ConsultorUniversal<ItensVendas>
                (
                tabela: "vendas",
                campos: "*",
                armazenador: null
                );
            consultor.Inserir("itens_venda", valores);
        }

        public static void updateItem(ItensVendas itens)
        {
            // 1. Prepara os valores que serão atualizados (exceto o id_item, que é a chave primária)
            var valores = new Dictionary<string, object>
            {
                {"id_venda", itens.Id_Venda},
                {"id_produto", itens.Id_Produto},
                {"quantidade", itens.Quantidade},
                {"preco_unitario", itens.Preco_Unitario},
                {"observacao", itens.Observacao}
            };

            // 2. Prepara as condições para o WHERE (usando o id_item)
            var condicoes = new Dictionary<string, object>
            {
                {"id_item", itens.Id_Item}
            };

            // 3. Cria o consultor universal (armazenador pode ser null, pois não vamos armazenar nada)
            var consultor = new ConsultorUniversal<ItensVendas>(
                tabela: "itens_venda",
                campos: "*",
                armazenador: null
            );

            // 4. Chama o método Update do consultor
            consultor.Update("itens_venda", valores, condicoes);
        }

        public static void deleteItem(int idItem)
        {
            // Prepara as condições para o WHERE
            var condicoes = new Dictionary<string, object>
            {
                {"id_item", idItem}
            };

            var consultor = new ConsultorUniversal<ItensVendas>(
                tabela: "itens_venda",
                campos: "*",
                armazenador: null
            );

            consultor.Delete("itens_venda", condicoes);
        }


        public static void exportToExcel(List<ItensVendas> listaItens)
        {
            ExcelExporter.ExportToExcel(listaItens, "RelatórioItens");
        }
    }
}
