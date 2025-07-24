using Microsoft.AspNetCore.Mvc;
using TasteTest.Models;
using TasteTest.Services;
using System.Threading.Tasks;
using TasteTest.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasteTest.Utility;
using System.Data;
using System.Diagnostics;

namespace TasteTest.Controllers
{
    public class AcquistaController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;

        public AcquistaController(
            IOrderService orderService,
            IProductService productService,
            IClientService clientService)
        {
            _orderService = orderService;
            _productService = productService;
            _clientService = clientService;
        }

        // Mostra la pagina iniziale con lista prodotti e clienti
        public async Task<IActionResult> Index()
        {
            var prodotti = await _productService.GetAllProdAsync();
            var clienti = await _clientService.GetAllAsync();

            ViewBag.Prodotti = prodotti;
            ViewBag.Clienti = clienti;

            return View();
        }

        // <°)))>< Per mostrare la pagina di acquisto per un cliente specifico. Magari da implementare con una logica di login.
        //public async Task<IActionResult> Acquista(int clienteId)
        //{
        //    var ordineAttivo = await _orderService.GetOrdineAttivoPerCliente(clienteId);
        //    var prodotti = await _productService.GetAllProdAsync();

        //    var viewModel = new AcquistaViewModel
        //    {
        //        Ordine = ordineAttivo,
        //        Prodotti = prodotti.ToList()
        //    };

        //    return View(viewModel);
        //}

        // Aggiunge un prodotto al carrello (ordine "Carrello")
        [HttpPost]
        public async Task<IActionResult> AggiungiProdottoAlCarrello(int clienteId, int prodottoId, int quantita)
        {
            var successo = await _orderService.AggiungiProdottoAlCarrello(clienteId, prodottoId, quantita);
            if (!successo)
            {
                TempData["Errore"] = "Errore durante l'aggiunta del prodotto al carrello.";
            }
            return RedirectToAction(nameof(Index), new { clienteId });
        }

        // Rimuove un prodotto dal carrello
        [HttpPost]
        public async Task<IActionResult> RimuoviProdottoDalCarrello(int ordineId, int prodottoId, int clienteId)
        {
            var successo = await _orderService.RimuoviProdottoDalCarrello(ordineId, prodottoId);
            if (!successo)
            {
                TempData["Errore"] = "Errore durante la rimozione del prodotto dal carrello.";
            }
            return RedirectToAction(nameof(Index), new { clienteId });
        }

        // Completa l'ordine inviato in JSON
        [HttpPost]
        public async Task<IActionResult> CompletaOrdine([FromBody] CompletaOrdineRequest request)
        {
            if (request == null || request.Prodotti == null || request.Prodotti.Count == 0)
            {
                return Json(new { success = false, message = "Dati ordine non validi." });
            }

            var successo = await _orderService.CompletaOrdine(request.ClienteId, request.Prodotti);
            return Json(new { success = successo, message = successo ? "Ordine completato con successo." : "Errore durante il completamento dell'ordine." });
        }

        // Annulla un ordine specifico
        [HttpPost]
        public async Task<IActionResult> AnnullaOrdine(int ordineId, int clienteId)
        {
            var successo = await _orderService.AnnullaOrdine(ordineId);
            if (!successo)
            {
                TempData["Errore"] = "Errore durante l'annullamento dell'ordine.";
            }
            return RedirectToAction(nameof(Index), new { clienteId });
        }

        // Ottiene il prezzo di un prodotto via AJAX
        [HttpGet]
        public async Task<IActionResult> GetPrezzoProdotto(int idProdotto)
        {
            var prezzo = await _productService.GetPrezzoProdottoAsync(idProdotto);
            if (prezzo.HasValue)
                return Json(new { success = true, prezzo = prezzo.Value });

            return Json(new { success = false, prezzo = 0 });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrdine(int idOrdine)
        {
            var success = await _orderService.DeleteOrdineAsync(idOrdine);
            return Json(new { success });
        }

        [HttpGet]
        public async Task<IActionResult> DettaglioOrdine(int idOrdine)
        {
            var dettaglio = await _orderService.GetDettaglioOrdineAsync(idOrdine);
            if (dettaglio == null || !dettaglio.Prodotti.Any())
                return NotFound();

            return PartialView("_DettaglioOrdinePartial", dettaglio);
        }
    }
}
