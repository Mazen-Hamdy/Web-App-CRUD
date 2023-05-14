using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationCRUD.Data;
using WebApplicationCRUD.Models;

namespace WebApplicationCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Result = _context.Employees.Include(x => x.Department)
                .OrderBy(x => x.EmployeeName).ToList();
            return View(Result);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            var Result = _context.Employees.Find(Id);
            return View("Create",Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model)
        {
            UploadImage(model);
            if (ModelState.IsValid)
            {
                _context.Employees.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
            return View(model);
        }

        public IActionResult Delete(int? Id)
        {
            var Result = _context.Employees.Find(Id);
            if (Result != null)
            {
                _context.Employees.Remove(Result);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private void UploadImage(Employee model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                // upload Image
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                model.UserImage = ImageName;
            }
            else if (model.UserImage == null && model.EmployeeId == null)
            {
                // Not uplaod Image & new employee
                model.UserImage = "DefaultImage.jpg";
            }
            else
            {
                // Edit
                model.UserImage = model.UserImage;
            }
        }
    }
}
