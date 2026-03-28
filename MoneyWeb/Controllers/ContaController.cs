using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyWeb.Helpers;
using MoneyWeb.Models.Entities;
using MoneyWeb.Models.ViewModels;
using MoneyWeb.Repository.Interfaces;

namespace MoneyWeb.Controllers
{
    [RequireLogin]
    public class ContaController : BaseController
    {
        private readonly IContaRepository _repository;
        private readonly IMapper _mapper;
        private const string _nomeForm = "ContaForm";

        public ContaController(IContaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private async Task<Conta> GetConta(int id)
        {
            return await _repository.GetContaById(id, UsuarioId) ?? throw new Exception("A conta não existe!");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var contas = _mapper.Map<IEnumerable<ContaViewModel>>(await _repository.GetContas(UsuarioId));
                return View(contas);
            }
            catch (Exception ex)
            {
                return ExibirViewErro($"Erro ao listar contas: {ex.Message}");
            }
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Criar Conta";
            ViewBag.Action = "Create";

            return View(_nomeForm);
        }

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ContaViewModel contaUpdate = _mapper.Map<ContaViewModel>(await GetConta(id));

                ViewBag.Title = "Editar Conta";
                ViewBag.Action = "Update";

                return View(_nomeForm, contaUpdate);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao editar conta: {ex.Message}", false, "Index");
            }
        }

        public async Task<IActionResult> Read(int id)
        {
            try
            {
                ContaViewModel contaRead = _mapper.Map<ContaViewModel>(await GetConta(id));
                return View(contaRead);
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao visualizar conta: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _repository.Delete(await GetConta(id));

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível excluir no banco de dados!");

                return ExibirMensagem("Conta excluída com sucesso!", true, "Index");

            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao excluir conta: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContaViewModel contaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Criar Conta";
                    ViewBag.Action = "Create";

                    return View(_nomeForm, contaViewModel);
                }

                Conta contaInsert = _mapper.Map<Conta>(contaViewModel);
                contaInsert.UsuarioId = this.UsuarioId;
                _repository.Add(contaInsert);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível criar no banco de dados!");

                return ExibirMensagem("Conta criada com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao criar conta: {ex.Message}", false, "Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update (ContaViewModel contaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = "Editar Conta";
                    ViewBag.Action = "Update";

                    return View(_nomeForm, contaViewModel);
                }

                Conta conta = await GetConta(contaViewModel.Id);

                Conta contaUpdate = _mapper.Map(contaViewModel, conta);
                _repository.Update(contaUpdate);

                if (!await _repository.SaveChanges())
                    throw new Exception("Não foi possível salvar no banco de dados!");

                return ExibirMensagem("Conta salva com sucesso!", true, "Index");
            }
            catch (Exception ex)
            {
                return ExibirMensagem($"Erro ao salvar conta: {ex.Message}", false, "Index");
            }
        }
    }
}