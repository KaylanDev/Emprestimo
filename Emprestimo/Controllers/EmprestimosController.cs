using Emprestimo.Data;
using Emprestimo.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Editar()
        {
            return View();
        }



    }
}
