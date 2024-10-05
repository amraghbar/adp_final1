using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project_.DAL.Models;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ServiceController(ApplicationDbContext context ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var serr = context.Services.ToList();
            var ServiceVM = mapper.Map<IEnumerable<ServiceVM>>(serr);
            return View(ServiceVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceFormVM vm)
        {

            if(ModelState.IsValid)
            {
                var ser = mapper.Map<Service>(vm);
                context.Add(ser);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [HttpGet]

        public IActionResult Details(int id) {
            var x = context.Services.Find(id);
            if(x == null)
            {
                return NotFound();
            }
           
            
            return View(mapper.Map<ServiceDetailsVM>(x));

        }
        [HttpGet]
        public IActionResult Delete(int id) {
            var x = context.Services.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ServiceVM>(x));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            var service = context.Services.Find(id);

            if (service is null)
            {
                return RedirectToAction(nameof(Index));
            }

            context.Services.Remove(service);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            var x = context.Services.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            return View(mapper.Map<ServiceDetailsVM>(x));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ServiceDetailsVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = context.Services.Find(model.Id);
            if (service == null)
            {
                return NotFound();
            }

            service.Name = model.Name;
            service.Description = model.Description;
            service.IsDeleted = model.IsDeleted;

            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}