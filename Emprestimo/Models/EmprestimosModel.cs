using System.ComponentModel.DataAnnotations;

namespace Emprestimo.Models
{
    public class EmprestimosModel
    {
        public int  Id { get; set; }

        [Required(ErrorMessage = "Digite o Recebedor!")]
        public string  Recebedor { get; set; }

        [Required(ErrorMessage = "Digite o Fornecedor!")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "Digite o Livro!")]
        public string  LivroEmprestado { get; set; }
        public DateTime  DataUltimaAtualizacao { get; set; } = DateTime.Now;
    }
}
