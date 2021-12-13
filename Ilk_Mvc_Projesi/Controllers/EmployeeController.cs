using Ilk_Mvc_Projesi.Models;
using Ilk_Mvc_Projesi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc_Project1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly NorthwindContext _context;
        public EmployeeController(NorthwindContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Employees.Include(x => x.Orders).ThenInclude(x => x.OrderDetails).ThenInclude(x => x.Product).OrderBy(x => x.FirstName).ToList();
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            var model = _context.Employees.Include(x => x.Orders).ThenInclude(x => x.OrderDetails).ThenInclude(x => x.Product).FirstOrDefault(x => x.EmployeeId == id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var employee = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Title = model.LastName
            };

            _context.Employees.Add(employee);
            try
            {
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { id = employee.EmployeeId });
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, $"{model.FirstName} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.LastName} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.Title} eklenirken bir hata oluştu. Tekrar Deneyin");
                return View(model);
            }
        }

        public IActionResult Delete(int? employeeId)
        {
            var silinecek = _context.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);

            try
            {
                _context.Employees.Remove(silinecek);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }
            TempData["silinen_employee"] = silinecek.FirstName;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var model = new EmployeeViewModel()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == model.EmployeeId);

            try
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Title = model.Title;
                _context.Update(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Detail), new { id = employee.EmployeeId });
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, $"{model.FirstName} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.LastName} eklenirken bir hata oluştu. Tekrar Deneyin");
                ModelState.AddModelError(string.Empty, $"{model.Title} eklenirken bir hata oluştu. Tekrar Deneyin");
                return View(model);
            }
        }
    }
}
