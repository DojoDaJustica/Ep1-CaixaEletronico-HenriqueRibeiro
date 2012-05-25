using CaixaEletronico.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CaixaEletronico.CoreTests
{
    /// <summary>
    /// Testes do Caixa Eletrônico
    ///</summary>
    [TestClass]
    public class CaixaEletronicoTest
    {
        [TestMethod]
        public void Sacar100ReaisDeveRetornarUmaCedulaDe100()
        {
            var caixa = new Core.CaixaEletronico();
            var cedulas = caixa.Sacar(100m);

            Assert.AreEqual(ContarCedulas(cedulas, 100m), 1);
        }

        [TestMethod]
        public void Sacar150ReaisDeveRetornarUmaCedulaDe100MaisUmaCedulaDe50()
        {
            var caixa = new Core.CaixaEletronico();
            var cedulas = caixa.Sacar(150m);
            
            Assert.IsTrue(ContarCedulasDe100(cedulas) == 1 && ContarCedulasDe50(cedulas) == 1);
        }

        [TestMethod]
        public void Sacar570ReaisDeveRetornarCincoCedulasDe100MaisUmaCedulaDe50MaisUmaCedulaDe20()
        {
            var caixa = new Core.CaixaEletronico();
            var cedulas = caixa.Sacar(570m);

            Assert.IsTrue(
                ContarCedulasDe100(cedulas) == 5 &&
                ContarCedulasDe50(cedulas) == 1 &&
                ContarCedulasDe20(cedulas) == 1);
        }

        [TestMethod]
        public void Sacar10590ReaisDeveRetornarCentoECincoCedulasDe100MaisUmaCedulaDe50MaisDuasCedulasDe20()
        {
            CaixaEletronico.Core.CaixaEletronico caixa = new Core.CaixaEletronico();
            var cedulas = caixa.Sacar(10590m);

            Assert.IsTrue(
                ContarCedulasDe100(cedulas) == 105 &&
                ContarCedulasDe50(cedulas) == 1 &&
                ContarCedulasDe20(cedulas) == 2);
        }

        [TestMethod]
        public void Sacar160ReaisDeveRetornarUmaCedulaDe100MaisUmaCedulaDe50MaisUmaCedulaDe10()
        {
            CaixaEletronico.Core.CaixaEletronico caixa = new Core.CaixaEletronico();
            var cedulas = caixa.Sacar(160m);

            Assert.IsTrue(
                ContarCedulasDe100(cedulas) == 1 &&
                ContarCedulasDe50(cedulas) == 1 &&
                ContarCedulasDe10(cedulas) == 1);
        }

        [TestMethod]
        public void NaoDevePermitirMontanteNegativo()
        {
            var caixa = new Core.CaixaEletronico();

            try
            {
                caixa.Sacar(-250m);
                Assert.Fail("Deveria ter lançado a Exception ArgumentOutOfRangeException.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void NaoDevePermitirMontanteZero()
        {
            var caixa = new Core.CaixaEletronico();

            try
            {
                caixa.Sacar(0m);
                Assert.Fail("Deveria ter lançado a Exception ArgumentOutOfRangeException.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DeveNegarOSaquePorFaltaDeCedula()
        {
            var caixa = new Core.CaixaEletronico();

            try
            {
                caixa.Sacar(327m);
                Assert.Fail("Deveria ter lançado a exception CedulasInsuficientesException.");
            }
            catch (CedulasInsuficientesException)
            {
                Assert.IsTrue(true);
            }
        }

        #region Métodos Auxiliares

        private int ContarCedulas(decimal[] cedulas, decimal valorCedula)
        {
            return cedulas.Count(c => c == valorCedula);
        }

        private int ContarCedulasDe100(decimal[] cedulas)
        {
            return ContarCedulas(cedulas, 100m);
        }

        private int ContarCedulasDe50(decimal[] cedulas)
        {
            return ContarCedulas(cedulas, 50m);
        }

        private int ContarCedulasDe20(decimal[] cedulas)
        {
            return ContarCedulas(cedulas, 20m);
        }

        private int ContarCedulasDe10(decimal[] cedulas)
        {
            return ContarCedulas(cedulas, 10m);
        }

        #endregion
    }
}
