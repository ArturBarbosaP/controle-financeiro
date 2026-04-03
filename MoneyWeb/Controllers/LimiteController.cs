using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;
using System.Threading.Tasks;

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

        private async Task<Limite> GetLimite(int id)
        {
            return await _repository.GetLimiteById(id, UsuarioId) ?? throw new Exception("O limite não existe!");
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

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                LimiteViewModel limiteUpdate = _mapper.Map<LimiteViewModel>(await GetLimite(id));

                ViewBag.Title = "Editar Limite";
                ViewBag.Action = "Update";

                return View(_nomeForm, limiteUpdate);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao editar limite: {ex.Message}", false, "Index");
            }
        }

        public async Task<IActionResult> Read(int id)
        {
            try
            {
                LimiteViewModel limiteRead = _mapper.Map<LimiteViewModel>(await GetLimite(id));
                return View(limiteRead);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao visualizar limite: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _repository.Delete(await GetLimite(id));

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível excluir no banco de dados!");

                return ExibirMensagem("Limite excluído com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao excluir limite: {ex.Message}", false, "Index");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(LimiteViewModel limiteViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Editar Limite";
                    ViewBag.Action = "Update";

                    return View(_nomeForm, limiteViewModel);
                }

                Limite limite = await GetLimite(limiteViewModel.Id);
                limite.ValorLimite = limiteViewModel.ValorLimite;

                _repository.Update(limite);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível salvar no banco de dados!");

                return ExibirMensagem("Limite salvo com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao salvar limite: {ex.Message}", false, "Index");
            }
        }
    }
}