using Microsoft.AspNetCore.Mvc;
using TasteTest.Services;
using TasteTest.Models;
using System.Text.Json.Nodes;

namespace TasteTest.Controllers
{
    //[Route("Prodotti/[action]")]
    public class ProdottiController : Controller
    {
        private readonly IProductService _prodService;

        public ProdottiController(IProductService prodService)
        {
            _prodService = prodService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var prodotti = await _prodService.GetAllProdAsync();
        //    return View(prodotti);
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var prodotti = await _prodService.GetAllProdAsync();
            var model = new ProdottiViewModel
            {
                Prodotto = new Prodotti(),        // vuoto per il form
                ListaProdotti = prodotti
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var prodotto = await _prodService.GetByIdProdAsync(id);
            if (prodotto == null)
                return NotFound();
            return View(prodotto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Prodotti prodotto)
        {
            if (!ModelState.IsValid)
                return View(prodotto);

            bool updated = await _prodService.UpdateProdAsync(prodotto);
            if (!updated)
            {
                ModelState.AddModelError("", "Errore nell'aggiornamento del prodotto.");
                return View(prodotto);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Prodotti prodotto)
        {
            if (!ModelState.IsValid)
                return View(prodotto);

            bool added = await _prodService.AddProdAsync(prodotto);
            if (!added)
            {
                ModelState.AddModelError("", "Errore nell'inserimento del prodotto.");
                return View(prodotto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateorUpdate(ProdottiViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ListaProdotti = await _prodService.GetAllProdAsync();
                return View("Index", model);
            }

            if (model.Prodotto.IDProdotto.HasValue && model.Prodotto.IDProdotto.Value > 0)
            {
                bool updated = await _prodService.UpdateProdAsync(model.Prodotto);
                if (!updated)
                {
                    ModelState.AddModelError("", "Errore nell'aggiornamento del prodotto.");
                    model.ListaProdotti = await _prodService.GetAllProdAsync();
                    return View("Index", model);
                }
            }
            else
            {
                bool added = await _prodService.AddProdAsync(model.Prodotto);
                if (!added)
                {
                    ModelState.AddModelError("", "Errore nell'inserimento del prodotto.");
                    model.ListaProdotti = await _prodService.GetAllProdAsync();
                    return View("Index", model);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] JsonObject data)
        {
            if (data == null || !data.TryGetPropertyValue("IDProdotto", out var idNode) || idNode == null)
                return BadRequest("IDProdotto non valido.");

            int idProdotto = idNode.GetValue<int>();

            bool deleted = await _prodService.DeleteProdAsync(idProdotto);
            if (!deleted)
                return StatusCode(500, "Errore durante l'eliminazione del prodotto.");

            return Ok();
        }
    }
}