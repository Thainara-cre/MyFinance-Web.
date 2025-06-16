using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
   using Microsoft.Extensions.Logging;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_service.Interfaces;
using myfinance_web_netcore.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace myfinance_web_netcore.Controllers
{
    [Route("controller")]
    public class PlanoContaController : Controller
    {
        private readonly ILogger<PlanoContaController>_logger;
        private readonly IPlanoContaService _planoContaService;

        public PlanoContaController(
            ILogger<PlanoContaController> logger,
            IPlanoContaService planoContaService) 
        {
            _logger = logger;
            _planoContaService = planoContaService;
        }

        [HttpGet("/PlanoConta/Index")]
        public IActionResult Index() 
        {
            var listaPlanoContas = _planoContaService.ListarRegistros();
            List<PlanoContaModel> listaPlanoContaModel = new List<PlanoContaModel>();

            foreach (var item in listaPlanoContas) 
            {
                var itemPlanoConta = new PlanoContaModel() {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Tipo = item.Tipo,
                };
                listaPlanoContaModel.Add(itemPlanoConta);
            }
            ViewBag.ListaPlanoConta = listaPlanoContaModel;
            return View();
        }

        [HttpPost("/PlanoConta/Cadastrar")]
        [HttpPost("/PlanoConta/Cadastrar/{id}")]
        public IActionResult Cadastrar(PlanoContaModel model) 
        {   
            var planoConta = new PlanoConta()
            {
                Id = model.Id,
                Descricao = model.Descricao,
                Tipo = model.Tipo
            };
            _planoContaService.Cadastrar(planoConta);
            return RedirectToAction("Index");
        }

        [HttpGet("/PlanoConta/Cadastrar")]
        [HttpGet("/PlanoConta/Cadastrar/{id}")]
        public IActionResult Cadastrar(int id) 
        {   
            if(id != 0)
            {
                var planoConta = _planoContaService.RetornarRegistro((int)id);
                var planoContaModel = new PlanoContaModel()
                {
                    Id = planoConta.Id,
                    Descricao = planoConta.Descricao,
                    Tipo = planoConta.Tipo
                };
                return View(planoContaModel);
            }
            else
            {
                return View();
            }
        }

        [HttpGet("/PlanoConta/Excluir/{id}")]
        public IActionResult Excluir(int? id) 
        {   
            _planoContaService.Excluir((int)id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration =0, Location = ResponseCacheLocation.None, NoStore = true)]
        
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}