using g_vendas.BLL_s;
using g_vendas.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Teste1.Data.Query;

namespace g_vendas.Controllers
{
    public class ItensVendasController
    {
        private readonly ItensVendasBLL _bll = new ItensVendasBLL();

        // Listar todos os itens de venda
        public object ListarItens()
        {
            try
            {
                return _bll.BuscarItensVendas();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar itens: {ex.Message}");
            }
        }

        // Buscar itens por venda
        public object BuscarItensPorVenda(int idVenda)
        {
            try
            {
                var itens = _bll.BuscarItensPorVenda(idVenda);
                return itens;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar itens: {ex.Message}");
            }
        }

        // Cadastrar novo item de venda
        public void CadastrarItem(ItensVendas item)
        {
            try
            {
                MessageBox.Show($"Inserindo item: Venda={item.Id_Venda}, Observação={item.Observacao}");
                _bll.InserirItem(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar item: {ex.Message}");
            }
        }


        // Atualizar item de venda
        public void AtualizarItem(ItensVendas item)
        {
            try
            {
                _bll.AtualizarItem(item);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar item: {ex.Message}");
            }
        }

        // Remover item de venda
        public void RemoverItem(int idItem)
        {
            try
            {
                _bll.RemoverItem(idItem);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao remover item: {ex.Message}");
            }
        }

        // Calcular o valor total de um item
        public decimal CalcularTotalItem(ItensVendas item)
        {
            try
            {
                return _bll.CalcularValorTotalItem(item);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao calcular valor total: {ex.Message}");
            }
        }

        // Validar dados do item antes do cadastro/atualização
        public string ValidarItem(ItensVendas item)
        {
            try
            {
                if (item == null)
                    return "O item não pode ser nulo.";

                if (item.Quantidade <= 0)
                    return "A quantidade deve ser maior que zero.";

                if (item.Preco_Unitario < 0)
                    return "O preço unitário não pode ser negativo.";

                return "Dados do item são válidos";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na validação: {ex.Message}");
            }
        }

        // Função para exportar para Excel (vazia)
        public void ExportarParaExcel(List<ItensVendas> itens)
        {
            _bll.ExportarItensParaExcel(itens);
        }

        public int CadastrarItemERetornarId(ItensVendas item)
        {
            // Monte o dicionário de valores conforme o padrão do seu projeto
            var valores = new Dictionary<string, object>
            {
                { "id_venda", item.Id_Venda },
                { "id_produto", item.Id_Produto },
                { "quantidade", item.Quantidade },
                { "preco_unitario", item.Preco_Unitario },
                { "observacao", item.Observacao }
            };
            var consultor = new ConsultorUniversal<ItensVendas>(
                tabela: "itens_venda",
                campos: "*",
                armazenador: null
                );
            long idGerado = consultor.Inserir("itens_venda", valores);
            return (int)idGerado;
        }

    }
}
