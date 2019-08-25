using AppRelatorio.Atributos;
using AppRelatorio.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppRelatorio.Model
{
    [Table("Relatorio")]
    public class Relatorio : CRUD<Relatorio>
    {
        [Key]
        [AutoIncrement]
        public int Id { get; set; }
        [Indexed, NotNull]
        public int IdPublicador { get; set; }   // Campo Obrigatório
        [NotNull]
        public DateTime MesRef { get; set; }    // Campo Obrigatório
        public int Publicacoes { get; set; }
        public int Videos { get; set; }
        [NotNull]
        public int Horas { get; set; }          // Campo Obrigatório
        public int Revisitas { get; set; }
        public int Estudos { get; set; }
        public string Observacao { get; set; }
        public string Atribuicao { get; set; }
    }
}
