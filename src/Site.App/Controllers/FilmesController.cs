using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Site.App.Data;
using Site.App.Models;
using Site.Business.Interfaces;
using Site.Business.Model;

namespace Site.App.Controllers
{
    public class FilmesController : BaseController
    {
        private readonly IFilmesRepository filmesRepository;
        private readonly IMapper mapper;
        private readonly IDistribuidoraRepository distribuidoraRepository;
        private readonly IFilmeService filmeService;



        public FilmesController(IFilmesRepository filmesRepository, IMapper mapper, IDistribuidoraRepository distribuidoraRepository, IFilmeService filmeService, INotificador notificador) : base(notificador)
        {
            this.filmesRepository = filmesRepository;
            this.mapper = mapper;
            this.distribuidoraRepository = distribuidoraRepository;
            this.filmeService = filmeService;
        }




        // GET: Filmes
      /*  public async Task<IActionResult> Index()
        {
            return View(mapper.Map<IEnumerable<FilmeViewModel>>(await  filmesRepository.ObterFilmesDistribuidora()));
        }*/


        // GET: Filmes
        public async Task<IActionResult> Index(string searchString)
        {
            var filmes = await filmesRepository.ObterFilmesDistribuidora();
            if (!String.IsNullOrEmpty(searchString))
            {
                filmes = filmes.Where(f => f.Nome.Contains(searchString));

                
            }
            return View(mapper.Map<IEnumerable<FilmeViewModel>>(filmes));
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var filmeViewModel = await ObterFilme(id);


            if (filmeViewModel == null) return NotFound();
        
            return View(filmeViewModel);
        }

        // GET: Filmes/Create
        public async Task<IActionResult> Create()
        {
            var filme = await PopularDistribuidora(new FilmeViewModel());
            return View(filme);
        }

        // POST: Filmes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmeViewModel filmeViewModel)
        {

            filmeViewModel = await PopularDistribuidora(filmeViewModel);

            if (!ModelState.IsValid) return View(filmeViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(filmeViewModel.ImagemUpload, imgPrefixo))
            {
                return View(filmeViewModel);
            }

            filmeViewModel.Imagem = imgPrefixo + filmeViewModel.ImagemUpload.FileName;

            await filmeService.Adicionar(mapper.Map<Filme>(filmeViewModel));

            if (!OperacaoValida()) return View(filmeViewModel);

            

            return RedirectToAction(nameof(Index));
        }

        // GET: Filmes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var filmeViewModel = await ObterFilme(id);


            if (filmeViewModel == null) return NotFound();

            return View(filmeViewModel);
        }

            // POST: Filmes/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,FilmeViewModel filmeViewModel)
        {
            if (id != filmeViewModel.Id) return NotFound();

            var filmeAtt = await ObterFilme(id);

            filmeViewModel.Distribuidora = filmeAtt.Distribuidora;
            filmeViewModel.Imagem = filmeAtt.Imagem;

        
            if (!ModelState.IsValid) return View(filmeViewModel);

            if (filmeViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(filmeViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(filmeViewModel);
                }

                filmeAtt.Imagem = imgPrefixo + filmeViewModel.ImagemUpload.FileName;
            }

            filmeAtt.Nome = filmeViewModel.Nome;
            filmeAtt.Sinopse = filmeViewModel.Sinopse;
            filmeAtt.Valor = filmeViewModel.Valor;
            filmeAtt.Categoria = filmeViewModel.Categoria;

            await filmeService.Atualizar(mapper.Map<Filme>(filmeAtt));

            if (!OperacaoValida()) return View(await ObterFilme(id));

            return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Filmes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var filmeViewModel = await ObterFilme(id);

            if (filmeViewModel == null) return NotFound();
     

            return View(filmeViewModel);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var filmeViewModel = await ObterFilme(id);
            if (filmeViewModel == null) return NotFound();

            await filmeService.Remover(id);

            if (!OperacaoValida()) return View(filmeViewModel);

            return RedirectToAction(nameof(Index));
        }

        
        private async Task<FilmeViewModel> ObterFilme(Guid id)
        {
            var filme = mapper.Map<FilmeViewModel>(await filmesRepository.ObterFilmeDistribuidora(id));

            filme.Distribuidoras = mapper.Map<IEnumerable<DistribuidoraViewModel>>(await distribuidoraRepository.ObterTodos());

            return filme;
        }

        private async Task<FilmeViewModel> PopularDistribuidora(FilmeViewModel filmeViewModel)
        {
            filmeViewModel.Distribuidoras = mapper.Map<IEnumerable<DistribuidoraViewModel>>(await distribuidoraRepository.ObterTodos());
            return filmeViewModel;
        }
        public async Task<IActionResult> Home()
        {
            return View(mapper.Map<IEnumerable<FilmeViewModel>>(await filmesRepository.ObterFilmesDistribuidora()));
        }



    }
}
