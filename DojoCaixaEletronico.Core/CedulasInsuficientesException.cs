using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico.Core
{
    public class CedulasInsuficientesException : ApplicationException
    {
        public decimal Montante { get; private set; }

        public CedulasInsuficientesException(decimal montante) : base(
            "Não há cédulas suficientes para o valor do saque.")
        {
            this.Montante = montante;
        }
    }
}
