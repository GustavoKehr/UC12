using Projeto_Teste;

namespace TesteXUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void SomarDoisNumeros()
        {
            double pNum = 1;
            double sNum = 1;
            double rNum = 2;

            var resultado = Operacoes.Somar(pNum, sNum);

            Assert.Equal(rNum, resultado);  
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(2, 1, 2)]

        public void SomarDoisNumerosListar(double pNUm, double sNum, double rNUm)
        {
            var resultado = Operacoes.Somar(pNUm, sNum);

            Assert.Equal(rNUm, resultado);
        }

    }   
}