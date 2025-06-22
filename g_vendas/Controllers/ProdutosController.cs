using g_vendas.BLL;
using g_vendas.DAL_s;
using g_vendas.Models;
using System.Collections.Generic;

namespace g_vendas.Controllers
{
    public class ProdutosController
    {
        public List<Produtos> ListarProdutos()
        {
            return ProdutosBLL.GetProdutos();
        }

        public int AdicionarProduto(Produtos produto)
        {
            return ProdutosBLL.InsertProduto(produto);
        }

        public void ExportarProdutosParaExcel(List<Produtos> produtos)
        {
            ProdutosDAL.exportToExcel(produtos);
        }

        //public List<string> ListarTiposProduto()
        //{
        //    return ProdutosBLL.GetTiposProduto();
        //}
    }
}
