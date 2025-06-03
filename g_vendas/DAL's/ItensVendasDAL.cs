using ClosedXML.Excel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            string dkPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string archiveName = $"RelatórioItensVendas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string completePath = Path.Combine(dkPath, archiveName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ItensVendas");

                worksheet.Cell(1, 1).Value = "Id Item";
                worksheet.Cell(1, 2).Value = "Id Venda";
                worksheet.Cell(1, 3).Value = "Id Produto";
                worksheet.Cell(1, 4).Value = "Quantidade";
                worksheet.Cell(1, 5).Value = "Preco Unitário";
                worksheet.Cell(1, 6).Value = "Observacao";

                int linha = 2;
                foreach (var itens in listaItens)
                {
                    worksheet.Cell(linha, 2).Value = itens.Id_Item;
                    worksheet.Cell(linha, 3).Value = itens.Id_Venda;
                    worksheet.Cell(linha, 4).Value = itens.Id_Produto;
                    worksheet.Cell(linha, 5).Value = itens.Quantidade;
                    worksheet.Cell(linha, 6).Value = itens.Preco_Unitario;
                    worksheet.Cell(linha, 7).Value = itens.Observacao;
                    linha++;
                }

                workbook.SaveAs(completePath);
            }
            Console.WriteLine($"arquivo salvo na área de trabalho {archiveName}");
        }
    }
}
