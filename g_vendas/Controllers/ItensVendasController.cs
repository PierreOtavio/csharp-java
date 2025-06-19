using g_vendas.BLL_s;
using g_vendas.Models;
using System;
using System.Collections.Generic;

namespace g_vendas.Controllers
{
    internal class ItensVendasController
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
                _bll.InserirItem(item);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar item: {ex.Message}");
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
    }
}
