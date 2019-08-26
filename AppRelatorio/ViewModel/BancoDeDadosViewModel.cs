using AppRelatorio.Atributos;
using AppRelatorio.View;
using Syncfusion.Data;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace AppRelatorio.ViewModel
{
    public class BancoDeDadosViewModel : INotifyPropertyChanged
    {
        readonly BancoDeDadosView page;

        #region Campos Privados
        private object itemSelecionado;
        private ObservableCollection<Model.PublicadorDetalhes> listaPublicadorDetalhes;
        #endregion

        #region Controles View
        private SfDataGrid DataGrid { get; set; }

        
        #endregion

        #region Propriedades de Vínculo
        public object ItemSelecionado { get => itemSelecionado; set { itemSelecionado = value; OnPropertyChanged(); } }
        
        public ObservableCollection<Model.PublicadorDetalhes> ListaPublicadorDetalhes { get => listaPublicadorDetalhes; set { listaPublicadorDetalhes = value; OnPropertyChanged(); } }
        #endregion

        // Construtor
        public BancoDeDadosViewModel(BancoDeDadosView page)
        {
            this.page = page;

            DataGrid = page.FindByName<SfDataGrid>("DataGrid");
            DataGrid.GridViewCreated += DataGrid_GridViewCreated;
            DataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

            DataGrid.FrozenColumnsCount = 3;

            ListaPublicadorDetalhes = Model.PublicadorDetalhes.Lista();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            Type tipo = new Model.PublicadorDetalhes().GetType();
            PropertyInfo prop = tipo.GetProperty(e.Column.MappingName);
            if (Attribute.GetCustomAttribute(prop, typeof(HiddenAttribute)) is HiddenAttribute)
            {
                // Oculta a coluna marcada como Hidden
                e.Column.IsHidden = true;
            }

            // Define o texto do cabeçalho da coluna conforme o atributo de descrição
            if (Attribute.GetCustomAttribute(prop, typeof(DescriptionAttribute)) is DescriptionAttribute description)
            {
                e.Column.HeaderText = description.Description;
            }
        }

        internal void Init()
        {
            
        }

        #region Eventos DataGrid
        private void DataGrid_GridViewCreated(object sender, GridViewCreatedEventArgs e)
        {
            DataGrid.View.LiveDataUpdateMode = LiveDataUpdateMode.Default;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
