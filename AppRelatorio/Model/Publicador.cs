using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Data.Sqlite;
using AppRelatorio.Atributos;
using AppRelatorio.Banco;
using AppRelatorio.Enumerador;

namespace AppRelatorio.Model
{
    [Table("Publicador")]
    public class Publicador : CRUD<Publicador>
    {
        [Key]
        [AutoIncrement]
        public int Id { get; set; }
        [Indexed(Name = "Idx_Nome_Sobrenome")]
        public string Nome { get; set; }
        [Indexed(Name = "Idx_Nome_Sobrenome")]
        public string Sobrenome { get; set; }
        public SituacaoPublicadorEnum Situacao { get; set; } = SituacaoPublicadorEnum.Ativo;
        public string Email { get; set; }
        public long? Telefone { get; set; }
        public DateTime Nascimento { get; set; }
        public AtribuicaoEnum Atribuicao { get; set; } = AtribuicaoEnum.PublicadorNaoBatizado;
        

        //public static Publicador Obter(int Id)
        //{
        //    using (SQLiteConnection con = new SQLiteConnection(Database.CaminhoArquivoBanco))
        //    {
        //        return con.Query<Publicador>($"SELECT * FROM Publicador WHERE Id={Id} LIMIT 1").FirstOrDefault();
        //    }
        //}
    }
}
