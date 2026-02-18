using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("UsuarioForm");
        }
    }
}