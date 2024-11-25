using Emprestimo.Data;
using Emprestimo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace Emprestimo.Controllers
{
    public class EmprestimosController : Controller
    {
        readonly private AplicationDbContext _db;

        public EmprestimosController(AplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimo = _db.Emprestimos;
            return View(emprestimo);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            EmprestimosModel emprestimos = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if(emprestimos == null)
            {
                return NotFound();
            }

            return View(emprestimos);
        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimo);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }


        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            EmprestimosModel emprestimos = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimos == null)
            {
                return NotFound();
            }

            return View(emprestimos);
        }
        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimo)
        {
            if (emprestimo == null)
            {
                return NotFound();
            }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
