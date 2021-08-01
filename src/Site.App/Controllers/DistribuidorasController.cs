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
    public class DistribuidorasController : BaseController
    {
        private readonly IDistribuidoraRepository distribuidoraRepository;
        private readonly IMapper mapper;
        private readonly IDistribuidoraService distribuidoraService;

        public DistribuidorasController(IDistribuidoraRepository distribuidoraRepository, IMapper mapper, IDistribuidoraService distribuidoraService,INotificador notificador): base(notificador)
        {
            this.distribuidoraRepository = distribuidoraRepository;
            this.mapper = mapper;
            this.distribuidoraService = distribuidoraService;
        }




        // GET: Distribuidoras
        public async Task<IActionResult> Index()
        {
            return View(mapper.Map<IEnumerable<DistribuidoraViewModel>>(await  distribuidoraRepository.ObterTodos()));
        }

        // GET: Distribuidoras/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var distribuidoraViewModel = await ObterDistribuidoraProduto(id);
                
            if (distribuidoraViewModel == null)
            {
                return NotFound();
            }

            return View(distribuidoraViewModel);
        }

        // GET: Distribuidoras/Create
        public IActionResult Create()
        {
        
            return View();
        }

        // POST: Distribuidoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DistribuidoraViewModel distribuidoraViewModel)
        {
 
            if (!ModelState.IsValid) return View(distribuidoraViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await UploadArquivo(distribuidoraViewModel.ImagemUpload, imgPrefixo))
            {
                return View(distribuidoraViewModel);
            }

            distribuidoraViewModel.Imagem = imgPrefixo + distribuidoraViewModel.ImagemUpload.FileName;

            var distribuidora = mapper.Map<Distribuidora>(distribuidoraViewModel);

           await distribuidoraService.Adicionar(distribuidora);

            if (!OperacaoValida()) return View(distribuidoraViewModel);

           return  RedirectToAction(nameof(Index));


        }

        // GET: Distribuidoras/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        { 

            var distribuidoraViewModel = await ObterDistribuidoraPorId(id);

            if (distribuidoraViewModel == null) return NotFound();
         


            return View(distribuidoraViewModel);
        }

        // POST: Distribuidoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,DistribuidoraViewModel distribuidoraViewModel)
        {
            if (id != distribuidoraViewModel.Id) return NotFound();

            var distribuidoraAtt = await ObterDistribuidoraPorId(id);

            distribuidoraViewModel.Imagem = distribuidoraAtt.Imagem ;
            distribuidoraViewModel.Filmes = distribuidoraAtt.Filmes;

            
            if (!ModelState.IsValid) return View(distribuidoraViewModel);

            if(distribuidoraViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(distribuidoraViewModel.ImagemUpload, imgPrefixo))
                {
                    return View(distribuidoraViewModel);
                }

                distribuidoraAtt.Imagem = imgPrefixo + distribuidoraViewModel.ImagemUpload.FileName;
            }

            distribuidoraAtt.Nome = distribuidoraViewModel.Nome;
            distribuidoraAtt.Sobre = distribuidoraViewModel.Sobre;
          

            var distribuidora = mapper.Map<Distribuidora>(distribuidoraAtt);

            await distribuidoraService.Adicionar(distribuidora);


            if (!OperacaoValida()) return View(ObterDistribuidoraPorId(id));

            return RedirectToAction(nameof(Index));
        }

        // GET: Distribuidoras/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var distribuidoraViewModel = await ObterDistribuidoraPorId(id);
            
            if (distribuidoraViewModel == null)
            {
                return NotFound();
            }

            return View(distribuidoraViewModel);
        }

        // POST: Distribuidoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var distribuidoraViewModel = await ObterDistribuidoraPorId(id);

            if (distribuidoraViewModel == null) return NotFound();

            await distribuidoraService.Remover(id);


            if (!OperacaoValida()) return View(distribuidoraViewModel);

            return RedirectToAction(nameof(Index));
        }


        private async Task<DistribuidoraViewModel> ObterDistribuidoraPorId(Guid id)
        {
            return mapper.Map<DistribuidoraViewModel>(await distribuidoraRepository.ObterDistribuidoraPorId(id));
        }

        private async Task<DistribuidoraViewModel> ObterDistribuidoraProduto(Guid id)
        {
            return mapper.Map<DistribuidoraViewModel>(await distribuidoraRepository.ObterDistribuidoraProduto(id));
        }
    }
}
