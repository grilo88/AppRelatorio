using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;
using AppRelatorio.Banco;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppRelatorio.Atributos;

namespace AppRelatorio.Model
{
    public class PublicadorDetalhes : INotifyPropertyChanged
    {
        #region Campos
        private long idPublicador;
        private string nome;
        private string sobrenome;
        private string email;
        private long? telefone;
        private DateTime nascimento;
        private string atribuicao;
        private byte mesRef;
        private int publicacoes;
        private int videos;
        private int horas;
        private int revisitas;
        private int estudos;
        private string observacao;
        private string situacao;
        #endregion

        #region Propriedades Públicas
        [Hidden]
        public long IdPublicador { get => idPublicador; set { idPublicador = value; OnPropertyChanged(); } }
        public string Nome { get => nome; set { nome = value; OnPropertyChanged(); } }
        public string Sobrenome { get => sobrenome; set { sobrenome = value; OnPropertyChanged(); } }
        public string Situacao { get => situacao; set { situacao = value; OnPropertyChanged(); } }
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        public long? Telefone { get => telefone; set { telefone = value; OnPropertyChanged(); } }
        public DateTime Nascimento { get => nascimento; set { nascimento = value; OnPropertyChanged(); } }

        [Description("Atribuição")]
        public string Atribuicao { get => atribuicao; set { atribuicao = value; OnPropertyChanged(); } }
        [Description("Mês")]
        public byte MesRef { get => mesRef; set { mesRef = value; OnPropertyChanged(); } }
        [Description("Publicações")]
        public int Publicacoes { get => publicacoes; set { publicacoes = value; OnPropertyChanged(); } }
        [Description("Vídeos")]
        public int Videos { get => videos; set { videos = value; OnPropertyChanged(); } }
        public int Horas { get => horas; set { horas = value; OnPropertyChanged(); } }
        public int Revisitas { get => revisitas; set { revisitas = value; OnPropertyChanged(); } }
        public int Estudos { get => estudos; set { estudos = value; OnPropertyChanged(); } }
        [Description("Observação")]
        public string Observacao { get => observacao; set { observacao = value; OnPropertyChanged(); } }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public static ObservableCollection<PublicadorDetalhes> Lista(string coluna = "", string valor = "")
        {
            ObservableCollection<PublicadorDetalhes> collection =
                new ObservableCollection<PublicadorDetalhes>();

            using (SqliteConnection con = new SqliteConnection(Database.ConnectionString))
            {
                con.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT PUB.Id AS IdPublicador,Nome,Sobrenome,Situacao,Email,Telefone,Nascimento,PUB.Atribuicao,MesRef,Publicacoes,Videos,Horas,Revisitas,Estudos,Observacao ");
                sb.Append("FROM Publicador AS PUB ");
                sb.Append("LEFT JOIN Relatorio AS REL ON REL.IdPublicador = PUB.Id ");

                if (coluna != "" && valor != "")
                {
                    // Filtro para o campo Pesquisa
                    sb.Append($"WHERE {coluna} LIKE '%{valor}%'");
                }

                using (SqliteCommand com = new SqliteCommand(sb.ToString(), con))
                using (SqliteDataReader dr = com.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        PublicadorDetalhes item = new PublicadorDetalhes();
                        PropertyInfo[] props = item.GetType().GetProperties();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            string name = dr.GetName(i);
                            object value = dr.GetValue(i);

                            PropertyInfo prop;
                            if ((prop = props.Where(x => x.Name == name).FirstOrDefault()) != null)
                            {
                                if (value is DBNull)
                                    value = null;
                                else if (prop.PropertyType == typeof(DateTime))
                                    value = Convert.ToDateTime(value, new System.Globalization.CultureInfo("en-us"));

                                prop.SetValue(item, value);
                            }
                        }
                        collection.Add(item);
                    }

                    return collection;
                }
            }
        }
    }
}
