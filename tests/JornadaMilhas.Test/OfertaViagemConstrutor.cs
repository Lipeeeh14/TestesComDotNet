using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
        [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
        [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
        [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(
            string origem, string destino, string dataIda, 
            string dataVolta, double preco, bool validacao)
        {
            // Padrão AAA
            // Cenário - Arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), 
                DateTime.Parse(dataVolta));

            // Ação - Act
            OfertaViagem oferta = new(rota, periodo, preco);

            // Validação - Assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroQuandoRotaOuPeriodoInvalidoQuandoRotaNula()
        {
            Rota rota = null!;
            Periodo periodo = new(new DateTime(2024, 2, 1),
                new DateTime(2024, 2, 5));
            double preco = 100.0;

            OfertaViagem oferta = new(rota, periodo, preco);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", 
                oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(-500)]
        [InlineData(0)]
        public void RetornaMensagemDeErroDePrecoInvalido(double preco) 
        {
            Rota rota = new("Rio de Janeiro", "São Paulo");
            Periodo periodo = new(new DateTime(2024, 2, 1),
                new DateTime(2024, 2, 5));

            OfertaViagem oferta = new(rota, periodo, preco);

            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.",
                oferta.Erros.Sumario);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos() 
        {
            Rota rota = null!;
            Periodo periodo = new(new DateTime(2024, 6, 1),
                new DateTime(2024, 5, 10));
            double preco = -100;
            int quantidadeEsperada = 3;

            OfertaViagem oferta = new(rota, periodo, preco);

            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}