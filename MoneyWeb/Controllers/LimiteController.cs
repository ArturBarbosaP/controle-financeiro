using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
    public class LimiteController : BaseController
    {
        private readonly ILimiteRepository _repository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "LimiteForm";

        public LimiteController(ILimiteRepository repository, ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _repository = repository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _categoriaRepository.GetCategoriasDeDespesa(UsuarioId);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var limites = _mapper.Map<IEnumerable<LimiteViewModel>>(await _repository.GetLimites(UsuarioId));
                return View(limites);
            }
            catch (Exception ex)
            {
                return ExibirViewErro(ex.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Title = "Criar Limite";
                ViewBag.Action = "Create";
                ViewBag.Categorias = await GetCategorias();

                return View(_nomeForm);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar limite: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LimiteViewModel limiteViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Criar Limite";
                    ViewBag.Action = "Create";
                    ViewBag.Categorias = await GetCategorias();

                    return View(_nomeForm, limiteViewModel);
                }

                Limite limiteInsert = _mapper.Map<Limite>(limiteViewModel);

                _repository.Add(limiteInsert);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível criar no banco de dados!");

                return ExibirMensagem("Limite criado com sucesso!", true, "Index");
            }
            catch (DbUpdateException ex) when (ex.InnerException.Message.Contains("CATEGORIA_ID_UNIQUE"))
            {
                ModelState.AddModelError(nameof(LimiteViewModel.CategoriaId), "Já existe um limite para a categoria selecionada!");

                ViewBag.Title = "Criar Limite";
                ViewBag.Action = "Create";
                ViewBag.Categorias = await GetCategorias();

                return View(_nomeForm, limiteViewModel);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar limite: {ex.Message}", false, "Index");
            }
        }
    }
}