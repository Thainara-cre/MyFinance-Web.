namespace myfinance_web_dotnet_domain.Entities;

public class PlanoConta
{
    public int Id { get; set; }  // Não nullable, já que o banco gera o Id automaticamente
    public string Descricao { get; set; }
    public string Tipo { get; set; } // "R" para Receita, "D" para Despesa
}