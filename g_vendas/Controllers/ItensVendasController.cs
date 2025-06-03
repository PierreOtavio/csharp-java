using g_vendas.BLL_s;
using g_vendas.Models;
using System;
using System.Linq;

namespace g_vendas.Controllers
{
    internal class ItensVendasController
    {
        public class ResponseModel
        {
            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public object Dados { get; set; }

            public ResponseModel(bool sucesso, string mensagem, object dados = null)
            {
                Sucesso = sucesso;
                Mensagem = mensagem;
                Dados = dados;
            }
        }

        private readonly ItensVendasBLL _bll = new ItensVendasBLL();

        // Listar todos os itens de venda
        public ResponseModel ListarItens()
        {
            try
            {
                var itens = _bll.BuscarItensVendas();
                return new ResponseModel(true, "Itens carregados com sucesso!", itens);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao carregar itens: {ex.Message}");
            }
        }

        // Buscar itens por venda
        public ResponseModel BuscarItensPorVenda(int idVenda)
        {
            try
            {
                var itens = _bll.BuscarItensPorVenda(idVenda);
                string mensagem = itens.Any() ? $"{itens.Count} itens encontrados" : "Nenhum item encontrado para essa venda";
                return new ResponseModel(true, mensagem, itens);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao buscar itens: {ex.Message}");
            }
        }

        // Cadastrar novo item de venda
        public ResponseModel CadastrarItem(ItensVendas item)
        {
            try
            {
                _bll.InserirItem(item);
                return new ResponseModel(true, "Item cadastrado com sucesso!", item);
            }
            catch (ArgumentException ex)
            {
                return new ResponseModel(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao cadastrar item: {ex.Message}");
            }
        }

        // Atualizar item de venda
        public ResponseModel AtualizarItem(ItensVendas item)
        {
            try
            {
                _bll.AtualizarItem(item);
                return new ResponseModel(true, "Item atualizado com sucesso!", item);
            }
            catch (ArgumentException ex)
            {
                return new ResponseModel(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao atualizar item: {ex.Message}");
            }
        }

        // Remover item de venda
        public ResponseModel RemoverItem(int idItem)
        {
            try
            {
                _bll.RemoverItem(idItem);
                return new ResponseModel(true, "Item removido com sucesso!");
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao remover item: {ex.Message}");
            }
        }

        // Calcular o valor total de um item
        public ResponseModel CalcularTotalItem(ItensVendas item)
        {
            try
            {
                decimal total = _bll.CalcularValorTotalItem(item);
                return new ResponseModel(true, $"Valor total calculado: {total:C}", total);
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro ao calcular valor total: {ex.Message}");
            }
        }

        // Validar dados do item antes do cadastro/atualização
        public ResponseModel ValidarItem(ItensVendas item)
        {
            try
            {
                if (item == null)
                    return new ResponseModel(false, "O item não pode ser nulo.");

                if (item.Quantidade <= 0)
                    return new ResponseModel(false, "A quantidade deve ser maior que zero.");

                if (item.Preco_Unitario < 0)
                    return new ResponseModel(false, "O preço unitário não pode ser negativo.");

                return new ResponseModel(true, "Dados do item são válidos");
            }
            catch (Exception ex)
            {
                return new ResponseModel(false, $"Erro na validação: {ex.Message}");
            }
        }
    }
}
