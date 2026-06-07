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

    // Método que apenas exibe a página principal
    public IActionResult Index()
    {
        return View();
    }

    // Método POST que recebe os dados do formulário
    [HttpPost]
    public IActionResult Validar(string nome, string email, int idade)
    {
        // Regra 1: Nome não pode estar vazio
        if (string.IsNullOrWhiteSpace(nome))
        {
            ViewBag.Erro = "Erro: O nome não pode estar vazio.";
            return View("Index"); // Retorna a página Index com o erro
        }

        // Regra 2: Email deve conter "@"
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            ViewBag.Erro = "Erro: O email informado é inválido (deve conter '@').";
            return View("Index");
        }

        // Regra 3: Idade deve ser > 0
        if (idade <= 0)
        {
            ViewBag.Erro = "Erro: A idade deve ser maior que 0.";
            return View("Index");
        }

        // Se passar por todas as validações:
        ViewBag.Sucesso = $"Tudo certo, {nome}! Formulário validado com sucesso no servidor.";
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}