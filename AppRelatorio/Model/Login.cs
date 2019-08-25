using AppRelatorio.Atributos;
using AppRelatorio.Banco;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRelatorio.Model
{
    [Table("Login")]
    public class Login : CRUD<Login>
    {
        [Key]
        [AutoIncrement]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string PerguntaSecreta { get; set; }
        public string RespostaSecreta { get; set; }
    }
}
