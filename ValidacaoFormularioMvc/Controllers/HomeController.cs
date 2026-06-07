using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ValidacaoFormularioMvc.Models;

namespace ValidacaoFormularioMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Validar(string nome, string email, int idade)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            ViewBag.Erro = "Erro: O nome não pode estar vazio.";
            return View("Index"); 
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            ViewBag.Erro = "Erro: O email informado é inválido (deve conter '@').";
            return View("Index");
        }

        if (idade <= 0)
        {
            ViewBag.Erro = "Erro: A idade deve ser maior que 0.";
            return View("Index");
        }

        ViewBag.Sucesso = $"Tudo certo, {nome}! Formulário validado com sucesso no servidor.";
        return View("Index");
    }

    public IActionResult Privacy()
    {
        ViewBag.Contador = 0;
        return View();
    }

    [HttpPost]
    public IActionResult Contar(string acao, int valorAtual)
    {

        if (acao == "aumentar")
        {
            valorAtual++;
        }
        else if (acao == "diminuir")
        {
            valorAtual--;
        }
        else if (acao == "zerar")
        {
            valorAtual = 0;
        }

        ViewBag.Contador = valorAtual;

        return View("Privacy");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}