using Microsoft.AspNetCore.Mvc;
using TasteTest.Models;
using TasteTest.Services;

//[Route("Clienti/[action]")]
public class ClientiController : Controller
{
    private readonly IClientService _clienteService;

    public ClientiController(IClientService clienteService)
    {
        _clienteService = clienteService;
    }

    // Azione MVC che mostra la pagina con la lista clienti (server rendered)
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clienti = await _clienteService.GetAllAsync();
        var clientiVm = clienti.Select(c => new ClientiViewModel
        {
            IDCliente = c.IDCliente,
            Nome = c.Nome,
            Cognome = c.Cognome,
            Email = c.Email
        });
        return View(clientiVm);
    }

    // API: Creazione o aggiornamento cliente da chiamata AJAX, JSON in body
    [HttpPost]
    public async Task<IActionResult> CreateorUpdate([FromBody] ClientiViewModel clientiVm)
    {
        if (!ModelState.IsValid)
        {
            // Restituisco errore 400 con dettagli (puoi personalizzare)
            return BadRequest(ModelState);
        }

        var cliente = new Cliente
        {
            IDCliente = clientiVm.IDCliente,
            Nome = clientiVm.Nome,
            Cognome = clientiVm.Cognome,
            Email = clientiVm.Email
        };

        if (cliente.IDCliente.HasValue && cliente.IDCliente > 0)
            await _clienteService.UpdateAsync(cliente);
        else
            cliente.IDCliente = await _clienteService.AddAsync(cliente); // Recupera ID nuovo

        // Restituisci il cliente aggiornato in formato JSON
        return Ok(new ClientiViewModel
        {
            IDCliente = cliente.IDCliente,
            Nome = cliente.Nome,
            Cognome = cliente.Cognome,
            Email = cliente.Email
        });
    }

    // Azione MVC GET per editare cliente, ritorna la view form con dati
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null || cliente.IDCliente == 0)
            return NotFound();

        var clientiVm = new ClientiViewModel
        {
            IDCliente = cliente.IDCliente,
            Nome = cliente.Nome,
            Cognome = cliente.Cognome,
            Email = cliente.Email
        };
        return View(clientiVm);
    }

    // Azione MVC POST per editare cliente tramite form tradizionale (non AJAX)
    [HttpPost]
    public async Task<IActionResult> Edit(ClientiViewModel clientiVm)
    {
        if (!ModelState.IsValid)
            return View(clientiVm);

        var cliente = new Cliente
        {
            IDCliente = clientiVm.IDCliente,
            Nome = clientiVm.Nome,
            Cognome = clientiVm.Cognome,
            Email = clientiVm.Email
        };

        await _clienteService.UpdateAsync(cliente);
        return RedirectToAction(nameof(Index));
    }

    // API per eliminazione cliente tramite AJAX (JSON id nel body)
    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        if (id <= 0)
            return BadRequest("IDCliente non valido.");

        bool success = await _clienteService.DeleteAsync(id);
        if (success)
            return Ok(new { message = "Cliente eliminato." });
        else
            return StatusCode(500, "Errore durante l'eliminazione");
    }

    // API per ottenere la lista clienti in JSON (utile per refresh tabella AJAX)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clienti = await _clienteService.GetAllAsync();
        var clientiVm = clienti.Select(c => new ClientiViewModel
        {
            IDCliente = c.IDCliente,
            Nome = c.Nome,
            Cognome = c.Cognome,
            Email = c.Email
        });
        return Json(clientiVm);
    }

    [HttpPost]
    public async Task<IActionResult> ValidateEmail([FromBody] EmailRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            return BadRequest(new { isValid = false, message = "Email mancante" });

        var clienti = await _clienteService.GetAllAsync();

        bool exists;

        if (request.IDCliente.HasValue && request.IDCliente.Value > 0)
        {
            // Escludi il cliente corrente dall'analisi
            exists = clienti.Any(c =>
                c.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)
                && c.IDCliente != request.IDCliente.Value);
        }
        else
        {
            // Nuovo cliente, controlla normalmente
            exists = clienti.Any(c =>
                c.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(new { isValid = !exists });
    }

}
