using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels;
namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService _service;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        public ActorsController(ILogger<HomeController> logger, IMapper mapper, IActorService service)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }
        // GET Actors
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var actors = _mapper.Map<IEnumerable<ActorDto>, IEnumerable<ActorViewModel>>(_service.GetAllActors());
            return View(actors);
        }
        // Get Actors/Details/5
        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ActorViewModel>(_service.GetActor((int) id));
           
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        //Get Actors/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        // Post Actors/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,BirthDate")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _service.AddActor(_mapper.Map<ActorDto>(inputModel));
                _logger.LogInformation($"Actor has been added!\nFirstName: {inputModel.FirstName}\nLastName: {inputModel.LastName}\nBirthdate: {inputModel.BirthDate.ToShortDateString()}");
                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        // Get: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var editModel = _mapper.Map<EditActorViewModel>(_service.GetActor((int) id));
            if (editModel == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Actor has been edited!\nFirstName: {editModel.FirstName}\nLastName: {editModel.LastName}\nBirthdate: {editModel.BirthDate.ToShortDateString()}");

            return View(editModel);
        }
        // Post Actors/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,BirthDate")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var actor = _mapper.Map<ActorDto>(editModel);
                actor.Id = id;
                var result = _service.UpdateActor(actor);
                if (result == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(editModel);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        // Get Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deleteActor = _mapper.Map<DeleteActorViewModel>(_service.GetActor((int) id));
            if (deleteActor == null)
            {
                return NotFound();
            }
            return View(deleteActor);
        }
        //Post: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _service.DeleteActor(id);
            if (actor == null)
            {
                return NotFound();
            }
            _logger.LogTrace($"Actor with id {actor.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }
    }
}