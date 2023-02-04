﻿using CalculadoraIMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculadoraIMC
{
    [TestClass]
    public class CalculoIMCTest
    {
        [TestMethod]
        public void CalcularIMC()
        {
            double peso = 110;
            double altura = 1.79;
            //34.33

            var resultadoIMC = Operacoes.CalcularIMC(peso, altura);

            Assert.AreEqual(34.33, resultadoIMC);
        }
    }
}
