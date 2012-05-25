using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico.Core
{
    public class CaixaEletronico
    {
        private decimal[] CedulasDisponiveis = { 100m, 50m, 20m, 10m };

        public decimal[] Sacar(decimal montante)
        {
            if (montante <= 0)
                throw new ArgumentOutOfRangeException("montante", montante, "O montante deve ser um valor positivo.");

            decimal montanteRestante = montante;
            List<decimal> cedulasEmitidas = new List<decimal>();

            // Recupera o valor da maior cedula que seja menor ou igual ao montante restante
            decimal cedula = CedulasDisponiveis.Max(c => c <= montanteRestante ? c : 0m);

            while (montanteRestante > 0)
            {
                if (montanteRestante < cedula)
                    cedula = CedulasDisponiveis.Max(c => c <= montanteRestante ? c : 0m);

                if (cedula == 0)
                {
                    // Não encontrou uma cedula que seja menor ou igual ao montante restante
                    throw new CedulasInsuficientesException(montante); 
                }

                cedulasEmitidas.Add(cedula);
                montanteRestante -= cedula;
            }

            return cedulasEmitidas.ToArray();
        }
    }

}
