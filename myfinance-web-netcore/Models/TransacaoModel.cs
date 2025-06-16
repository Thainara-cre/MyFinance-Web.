using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using  myfinance_web_dotnet_domain.Entities;
namespace myfinance_web_netcore.Models;

public class TransacaoModel
{
    public int Id { get; set; }
    public string? Historico { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; }
    public int? PlanoContaId { get; set; }  // Alterado para int? para ser compatível com a base de dados caso seja nullable
    public string? Tipo { get; set; }
    public IEnumerable<SelectListItem>? ListaPlanoContas { get; set; }
    [ForeignKey("PlanoContaId")]
    public PlanoConta? PlanoConta { get; set; }
}
