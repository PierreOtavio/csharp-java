using g_vendas.ExportacaoExcel;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using Teste1.Data.Query;

namespace g_vendas.DAL_s
{
    internal class ProdutosDAL
    {
        public static List<Produtos> buscarProdutos()
        {
            List<Produtos> listaProd = new List<Produtos>();
            Action<Produtos> storeOn = p => listaProd.Add(p);

            var consultorUni = new ConsultorUniversal<Produtos>
                (
                    tabela: "produtos",
                    campos: "*",
                    armazenador: storeOn
                );
            consultorUni.BuscarEArmazenar();
            return listaProd;
        }

        public static int insertProd(Produtos produtos)
        {
            var valores = new Dictionary<string, object>
            {
                {"nome", produtos.Nome },
                {"preco", produtos.Preco },
                {"descricao", produtos.Descricao },
                {"tipo", produtos.Tipo }
            };
            var consultor = new ConsultorUniversal<Produtos>(
                tabela: "produtos",
                campos: "*",
                armazenador: null
            );
            long idGerado = consultor.Inserir("produtos", valores);
            return (int)idGerado;
        }


        public static void exportToExcel(List<Produtos> listaProd)
        {
            ExcelExporter.ExportToExcel(listaProd, "Produtos");
        }
    }
}
