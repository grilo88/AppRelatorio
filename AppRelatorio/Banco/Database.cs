using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Text;
using Xamarin.Forms;
using AppRelatorio.Model;

namespace AppRelatorio.Banco
{
    public static class Database
    {
        public static string ConnectionString { get; set; }
        public static string CaminhoArquivoBanco { get; set; }

        // Construtor Estático
        static Database()
        {
            ICaminho dep = DependencyService.Get<ICaminho>();
            CaminhoArquivoBanco = dep.ObterCaminho("db.sqlite");

            SqliteConnectionStringBuilder sb = new SqliteConnectionStringBuilder();
            sb.DataSource = CaminhoArquivoBanco;
            ConnectionString = sb.ToString();

            using (SqliteConnection con = new SqliteConnection(ConnectionString))
            {
                con.Open();

                if (con.TabelaExiste<Login>()) con.ExcluirTabela<Login>();
                if (con.TabelaExiste<Relatorio>()) con.ExcluirTabela<Relatorio>();
                if (con.TabelaExiste<Publicador>()) con.ExcluirTabela<Publicador>();

                con.CriarTabela<Login>();
                con.CriarTabela<Relatorio>();
                con.CriarTabela<Publicador>();

                //if (!con.TabelaExiste<Login>()) con.CriarTabela<Login>();
                //if (!con.TabelaExiste<Publicador>()) con.CriarTabela<Publicador>();
                //if (!con.TabelaExiste<Relatorio>()) con.CriarTabela<Relatorio>();
            }
        }
    }
}
