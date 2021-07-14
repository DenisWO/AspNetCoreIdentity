using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy ="Delete")]
        public IActionResult AdminClaim()
        {
            return View("Admin");
        }

        [Authorize(Policy = "Write")]
        public IActionResult AdminWrite()
        {
            return View("Admin");
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte";
                modelError.Title = "Ocorreu um erro!";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "A página que você está procurando não existe! <br/> Em caso de dúvidas, contate nosso suporte";
                modelError.Title = "Ops! Página não encontrada!";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "Você não tem permissão para fazer isso";
                modelError.Title = "Acesso Negado";
                modelError.ErrorCode = id;
            }
            else
                return StatusCode(404);
            return View("Error", modelError);
        }
    }
}
