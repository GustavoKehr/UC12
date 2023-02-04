using Projeto_Teste;

namespace TesteMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SomarDoisNumeros()
        {
            //Arrange - Prepara��o

            double pNum = 1;
            double sNum = 1;
            double rNum = 2;

            //Act - Acao
            var resultado = Operacoes.Somar(pNum, sNum);

            //Assert - 
            Assert.AreEqual(rNum, resultado);
        }

        [DataTestMethod]
        [DataRow(1, 1, 2)]
        [DataRow(2, 2, 4)]
        [DataRow(2, 1, 2)]

        public void SomarDoisNumerosLista(double pNum, double sNum, double rNum) 
        {
            var resultado = Operacoes.Somar(pNum, sNum);

            Assert.AreEqual(rNum, resultado);
        }
    }
}