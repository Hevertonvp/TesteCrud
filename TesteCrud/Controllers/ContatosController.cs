using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteCrud.Models;
using TesteCrud.Models.Dto;
using TesteCrud.Services;

namespace TesteCrud.Controllers
{
    public class ContatosController : Controller
    {
        private readonly ContatoService _service;

        public ContatosController(ContatoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var contatos = await _service.FindAllAsync();
            return View(contatos);
        }

        public IActionResult Create()
        {
            return View("Create", new Contato());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contato contato)
        {
            await _service.InsertAsync(contato);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var obj = _service.FindById(id).Result;
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contato contato)
        {
            await _service.UpdateAsync(contato);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int id)
        {
            var obj = _service.FindById(id).Result;
            return View(obj);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> FiltrarPorCidade()
        {
            var dados = _service.FiltrarPorCidade().Result;
            var txt = Relatorio(dados);
            ViewBag.relatorio = txt;

            return View("Index", await _service.FindAllAsync());
        }

        private string Relatorio(IList<ContatoResumoDto> dados)
        {
            var result = new List<string>();
            var txt = "Contatos  ";
            foreach (var item in dados)
            {
                if (result.Count == 0)
                {
                    txt += item.Cidade.ToUpper() + ": </br>";
                    txt += SelecionarMesPorExtenso(Convert.ToInt32(item.Mes)) + ":  " + item.TotalContatos + ", " + item.TotalPorSexo + " " + (item.Sexo == 'M' ? "Masculino" : "Feminino") + "</br>";
                }
                else
                {
                    if (result.Last().ToLower().Contains(item.Cidade.ToLower()))
                    {
                        txt += SelecionarMesPorExtenso(Convert.ToInt32(item.Mes)) + ":  " + item.TotalContatos + ", " + item.TotalPorSexo + " " + (item.Sexo == 'M' ? "Masculino" : "Feminino") + "</br>";
                    }
                    else
                    {
                        txt += "Contatos " + item.Cidade.ToUpper() + ": </br>" + SelecionarMesPorExtenso(Convert.ToInt32(item.Mes)) + ": " + item.TotalContatos + ", " + item.TotalPorSexo + " " + (item.Sexo == 'M' ? "Masculino" : "Feminino") + "</br>"; ;
                    }
                }
                result.Add(txt);

            }
            return txt;
        }

        private string SelecionarMesPorExtenso(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
                default:
                    return "Nâo encontrado";

            }

        }
    }
}
