using AppRelatorio.View;
using Syncfusion.Data;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            ListaPublicadorDetalhes = Model.PublicadorDetalhes.Lista();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.MappingName == "Atribuicao")
            {

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
