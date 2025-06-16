namespace myfinance_web_netcore.Models;

public class PlanoContaModel
{
    public int Id { get; set; }  // Não nullable, já que o banco gera o Id automaticamente
    public required string Descricao { get; set; }
    public required string Tipo { get; set; } // "R" para Receita, "D" para Despesa

}
