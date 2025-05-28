using System;

namespace g_vendas.Models
{
    internal class Venda
    {
        public int id_venda { get; set; } // Adicionar ID
        public DateTime data_venda { get; set; }
        public decimal valor_total { get; set; }
        public decimal desconto { get; set; }
        public forma_pagamento FormaPagamento { get; set; } // Propriedade para forma de pagamento

        public enum forma_pagamento
        {
            Dinheiro = 1,
            Cartao = 2,
            PIX = 3,
            Outro = 4
        }

        // Construtor com parâmetros
        public Venda(DateTime data_venda, decimal valor_total, decimal desconto, forma_pagamento formaPagamento)
        {
            this.data_venda = data_venda;
            this.valor_total = valor_total;
            this.desconto = desconto;
            this.FormaPagamento = formaPagamento;
        }

        // Construtor padrão
        public Venda()
        {
            data_venda = DateTime.Now;
            valor_total = 0;
            desconto = 0;
            FormaPagamento = forma_pagamento.Dinheiro;
        }

        // Propriedade calculada para valor final
        public decimal ValorFinal => valor_total - desconto;
    }
}
