using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemDesconto
{
    [Fact]
    public void RetornaPrecoAtualizadoQuandoAplicadoDesconto() 
    {
        Rota rota = new("Rio de Janeiro", "São Paulo");
        Periodo periodo = new(new DateTime(2024, 2, 1),
            new DateTime(2024, 2, 5));
        double precoOriginal = 100.0;
        double desconto = 20.00;
        double precoComDesconto = precoOriginal - desconto;
        OfertaViagem oferta = new(rota, periodo, precoOriginal);
        
        oferta.Desconto = desconto;

        Assert.Equal(precoComDesconto, oferta.Preco);
    }

    [Theory]
    [InlineData(120, 30)]
    [InlineData(100, 30)]
    public void RetornaDescontoMaximoQuandoValorDescontoMaiorOuIgualAoPreco(
        double desconto, double precoComDesconto)
    {
        Rota rota = new("Rio de Janeiro", "São Paulo");
        Periodo periodo = new(new DateTime(2024, 2, 1),
            new DateTime(2024, 2, 5));
        double precoOriginal = 100.00;
        OfertaViagem oferta = new(rota, periodo, precoOriginal);

        oferta.Desconto = desconto;

        Assert.Equal(precoComDesconto, oferta.Preco, tolerance: 0.001);
    }

    [Fact]
    public void RetornaPrecoOriginalQuandoValorDescontoForNegativo()
    {
        Rota rota = new("Rio de Janeiro", "São Paulo");
        Periodo periodo = new(new DateTime(2024, 2, 1),
            new DateTime(2024, 2, 5));
        double precoOriginal = 100.0;
        double desconto = -10.00;
        OfertaViagem oferta = new(rota, periodo, precoOriginal);

        oferta.Desconto = desconto;

        Assert.Equal(precoOriginal, oferta.Preco, tolerance: 0.001);
    }
}
