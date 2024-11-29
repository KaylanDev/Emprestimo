using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Emprestimo.Data;
using Emprestimo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
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
                emprestimo.DataUltimaAtualizacao = DateTime.Now;
                _db.Emprestimos.Add(emprestimo);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Cadastro Realizado com sucesso";
                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Algum erro ocorreu ao Cadastrar!";

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
                var emprestimoDB = _db.Emprestimos.Find(emprestimo.Id);

                emprestimoDB.Recebedor = emprestimo.Recebedor;
                emprestimoDB.Fornecedor = emprestimo.Fornecedor;
                emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;

                _db.Emprestimos.Update(emprestimoDB);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Edição Realizada com sucesso";
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
            TempData["MensagemSucesso"] = "Exclusão Realizada com sucesso";
            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados emprestimo");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }


             
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Emprestimos";

            dataTable.Columns.Add("Recebedor",typeof(string));
            dataTable.Columns.Add("Fornecedoe",typeof(string));
            dataTable.Columns.Add("Livro",typeof(string));
            dataTable.Columns.Add("Data Emprestimo",typeof(string));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataUltimaAtualizacao);
                });
            }
            return dataTable;
        }

    }
}
