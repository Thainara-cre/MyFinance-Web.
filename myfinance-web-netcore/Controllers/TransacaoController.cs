using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;
using myfinance_web_netcore.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace myfinance_web_netcore.Controllers
{
    [Route("controller")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController>_logger;
        private readonly ITransacaoService _transacaoService;
        private readonly IPlanoContaService _planoContaService;

        public TransacaoController(
            ILogger<TransacaoController> logger,
            ITransacaoService TransacaoService,
            IPlanoContaService PlanoContaService) 
        {
            _logger = logger;
            _transacaoService = TransacaoService;
            _planoContaService = PlanoContaService;
        }

        [HttpGet("Transacao/Index")]
        public IActionResult Index() 
        {
            var listaTransacaos = _transacaoService.ListarRegistros();
            List<TransacaoModel> listaTransacaoModel = new List<TransacaoModel>();

            foreach (var item in listaTransacaos) 
            {
                var itemTransacao = new TransacaoModel() {
                    Id = item.Id,
                    Historico = item.Historico,
                    Data = item.Data,
                    Valor = item.Valor,
                    Tipo = item.PlanoConta.Tipo,
                    PlanoContaId = item.PlanoContaId
                };
                listaTransacaoModel.Add(itemTransacao);
            }
            ViewBag.ListaTransacao = listaTransacaoModel;
            return View();
        }

        [HttpPost("Transacao/Cadastrar")]
        [HttpPost("Transacao/Cadastrar/{id}")]
        public IActionResult Cadastrar(TransacaoModel model) 
        {   
            var transacao = new Transacao()
            {
                Id = (int)model.Id,
                Historico = (string)model.Historico,
                Data = model.Data,
                Valor = model.Valor,
                PlanoContaId = (int)model.PlanoContaId
            };
            Console.WriteLine($"Id: {transacao.Id}");
            Console.WriteLine($"Historico: {transacao.Historico}");
            Console.WriteLine($"Data: {transacao.Data}");
            Console.WriteLine($"Valor: {transacao.Valor}");
            Console.WriteLine($"PlanoContaId: {transacao.PlanoContaId}");
            Console.WriteLine(transacao);
            _transacaoService.Cadastrar(transacao);
            return RedirectToAction("Index");
        }

        [HttpGet("Transacao/Cadastrar")]
        [HttpGet("Transacao/Cadastrar/{id}")]
        public IActionResult Cadastrar(int id) 
        {   
            var listaPlanoContas = new SelectList(_planoContaService.ListarRegistros(), "Id", "Descricao");

            var itemTransacao = new TransacaoModel()
            {
                Data = DateTime.Now,
                ListaPlanoContas = listaPlanoContas
            };

            if(id != 0)
            {
                var transacao = _transacaoService.RetornarRegistro((int)id);
                    
                itemTransacao.Id = (int)transacao.Id;
                itemTransacao.Historico = transacao.Historico;
                itemTransacao.Data = transacao.Data;
                itemTransacao.Valor = transacao.Valor;
                itemTransacao.PlanoContaId = transacao.PlanoContaId;
            }
            
            return View(itemTransacao);
        }

        [HttpGet("Transacao/Excluir/{id}")]
        public IActionResult Excluir(int? id) 
        {   
            _transacaoService.Excluir((int)id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration =0, Location = ResponseCacheLocation.None, NoStore = true)]
        
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}