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
using AppRelatorio.Enumerador;
using System.Diagnostics;

namespace AppRelatorio.Model
{
    public class PublicadorDetalhes : INotifyPropertyChanged, IEditableObject
    {
        #region Campos Privados
        private long idPublicador;
        private string nome;
        private string sobrenome;
        private string email;
        private long? telefone;
        private DateTime nascimento;
        private AtribuicaoEnum atribuicao;
        private byte mesRef;
        private int publicacoes;
        private int videos;
        private int horas;
        private int revisitas;
        private int estudos;
        private string observacao;
        private SituacaoPublicadorEnum situacao;
        #endregion

        #region Propriedades Públicas
        [Hidden]
        public long IdPublicador { get => idPublicador; set { idPublicador = value; OnPropertyChanged(); } }
        public string Nome { get => nome; set { nome = value; OnPropertyChanged(); } }
        public string Sobrenome { get => sobrenome; set { sobrenome = value; OnPropertyChanged(); } }
        public SituacaoPublicadorEnum Situacao { get => situacao; set { situacao = value; OnPropertyChanged(); } }
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        public long? Telefone { get => telefone; set { telefone = value; OnPropertyChanged(); } }
        public DateTime Nascimento { get => nascimento; set { nascimento = value; OnPropertyChanged(); } }

        [Description("Atribuição")]
        public AtribuicaoEnum Atribuicao { get => atribuicao; set { atribuicao = value; OnPropertyChanged(); } }
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

        public static ObservableCollection<PublicadorDetalhes> Lista(string coluna = "", string valor = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT PUB.Id AS IdPublicador,Nome,Sobrenome,Situacao,Email,Telefone,Nascimento,PUB.Atribuicao,MesRef,Publicacoes,Videos,Horas,Revisitas,Estudos,Observacao ");
            sb.Append("FROM Publicador AS PUB ");
            sb.Append("LEFT JOIN Relatorio AS REL ON REL.IdPublicador = PUB.Id ");

            if (coluna != "" && valor != "")
            {
                // Filtro para o campo Pesquisa
                sb.Append($"WHERE {coluna} LIKE '%{valor}%'");
            }

            return CRUD<PublicadorDetalhes>.ToList(sb.ToString());
        }

        #region Implementação de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        #region Implementação de IEditableObject
        private Dictionary<string, object> storedValues;

        public void BeginEdit()
        {
            this.storedValues = this.BackUp();
        }

        public void CancelEdit()
        {
            if (this.storedValues == null)
                return;

            foreach (var item in this.storedValues)
            {
                var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
                var pDesc = itemProperties.FirstOrDefault(p => p.Name == item.Key);
                if (pDesc != null)
                    pDesc.SetValue(this, item.Value);
            }
        }

        public void EndEdit()
        {
            if (this.storedValues != null)
            {
                this.storedValues.Clear();
                this.storedValues = null;
            }
            Debug.WriteLine("Fim de Edição");
        }

        protected Dictionary<string, object> BackUp()
        {
            var dictionary = new Dictionary<string, object>();
            var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var pDescriptor in itemProperties)
            {
                if (pDescriptor.CanWrite)
                    dictionary.Add(pDescriptor.Name, pDescriptor.GetValue(this));
            }
            return dictionary;
        }
        #endregion
    }
}
