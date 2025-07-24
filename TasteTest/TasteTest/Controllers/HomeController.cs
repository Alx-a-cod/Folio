using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TasteTest.Models;
using TasteTest.Services;

namespace TasteTest.Controllers;

public class HomeController : Controller
{
    private readonly IOrderService _orderService;

    public HomeController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Mostra la Home con lo storico degli ordini
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var ordini = await _orderService.GetOrdiniAsync();
        return View(ordini);
    }

    // Elimina un ordine
    [HttpPost]
    public async Task<IActionResult> DeleteOrdine(int ordineId)
    {
        bool success = await _orderService.DeleteOrdineAsync(ordineId);
        return Json(new { success });
    }

    // Ottieni i dettagli di un ordine (per il dropdown)

    [HttpGet]
    public async Task<IActionResult> GetDettagliOrdine(int ordineId)
    {
        var dettagli = await _orderService.GetDettagliOrdineAsync(ordineId);

        if (dettagli == null || dettagli.Count == 0)
            return Content("Nessun dettaglio disponibile per questo ordine.");

        //<°)))><
        // Restituisce la partial view con il primo elemento (per come è pensato adesso ce ne è solo uno.
        // Si potrebbe pensare ad uno storico ordini associati a cliente.) Cute per eventuale futuro report/statistica/storico in BI. 
        return PartialView("DettaglioOrdinePartialView", dettagli.First());
    }

}