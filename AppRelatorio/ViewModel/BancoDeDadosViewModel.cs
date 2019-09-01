using AppRelatorio.Atributos;
using AppRelatorio.Estilo;
using AppRelatorio.Model;
using AppRelatorio.Sumario;
using AppRelatorio.View;
using Syncfusion.Data;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        #region Filtro de Pesquisa

        #region Campos

        private string filterText = "";
        private string selectedColumn = "Todas as Colunas";
        private string selectedCondition = "Equals";
        internal delegate void FilterChanged();
        internal FilterChanged filterTextChanged;

        #endregion

        #region Propriedades

        public string FilterText { get => filterText;  set { filterText = value; OnFilterTextChanged(); OnPropertyChanged(); } }
        public string SelectedCondition { get => selectedCondition; set { selectedCondition = value; } }
        public string SelectedColumn { get => selectedColumn;  set { selectedColumn = value; } }

        #endregion

        #region Métodos Privados

        private void OnFilterTextChanged() => filterTextChanged();

        private bool MakeStringFilter(PublicadorDetalhes o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            exactValue = exactValue.ToString().ToLower();
            string text = FilterText.ToLower();
            var methods = typeof(string).GetMethods();
            if (methods.Count() != 0)
            {
                if (condition == "Contains")
                {
                    var methodInfo = methods.FirstOrDefault(method => method.Name == condition);
                    bool result1 = (bool)methodInfo.Invoke(exactValue, new object[] { text });
                    return result1;
                }
                else if (exactValue.ToString() == text.ToString())
                {
                    bool result1 = String.Equals(exactValue.ToString(), text.ToString());
                    if (condition == "Equals")
                        return result1;
                    else if (condition == "NotEquals")
                        return false;
                }
                else if (condition == "NotEquals")
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private bool MakeNumericFilter(PublicadorDetalhes o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            double res;
            bool checkNumeric = double.TryParse(exactValue.ToString(), out res);
            if (checkNumeric)
            {
                switch (condition)
                {
                    case "Equals":
                        try
                        {
                            if (exactValue.ToString() == FilterText)
                            {
                                if (Convert.ToDouble(exactValue) == (Convert.ToDouble(FilterText)))
                                    return true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case "NotEquals":
                        try
                        {
                            if (Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                                return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        #endregion

        #region Métodos Públicos

        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as PublicadorDetalhes;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !SelectedColumn.Equals("Todas as Colunas"))
                    {
                        bool result = MakeNumericFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                    else if (SelectedColumn.Equals("Todas as Colunas"))
                    {
                        if (item.Nome.ToLower().Contains(FilterText.ToLower()) ||
                            item.Sobrenome.ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                    else
                    {
                        bool result = MakeStringFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                }
            }
            return false;
        }

        #endregion

        #endregion

        // Construtor
        public BancoDeDadosViewModel(BancoDeDadosView page)
        {
            this.page = page;
            
            DataGrid = page.FindByName<SfDataGrid>("DataGrid");
            DataGrid.ColumnSizer = ColumnSizer.Auto;
            DataGrid.SelectionMode = Syncfusion.SfDataGrid.XForms.SelectionMode.Single;
            DataGrid.AutoGenerateColumns = true;
            DataGrid.AllowResizingColumn = true;
            DataGrid.AllowGroupExpandCollapse = true;

            DataGrid.GridViewCreated += DataGrid_GridViewCreated;
            DataGrid.AutoGeneratingColumn += DataGrid_AutoGeneratingColumn;

            DataGrid.FrozenColumnsCount = 3;

            #region Sumário de Grupo Customizado
            DataGrid.CellRenderers.Remove("GroupSummary");
            DataGrid.CellRenderers.Add("GroupSummary", new SumarioGrupoAtribuicaoCellRendererExt());

            DataGrid.GroupSummaryRows.Add(new GridGroupSummaryRow()
            {
                ShowSummaryInRow = true,
                Title = "Total Horas: {Horas}",
                SummaryColumns = new ObservableCollection<ISummaryColumn>()
                {
                    new GridSummaryColumn()
                    {
                        Name="Horas",
                        MappingName="Horas",
                        SummaryType=SummaryType.DoubleAggregate,
                        Format="{Sum}"
                    },
                    new GridSummaryColumn()
                    {
                        Name="IdPublicador",
                        MappingName="IdPublicador",
                        Format="{Count}",
                        SummaryType=SummaryType.CountAggregate
                    }
                }
            });
            #endregion

            #region Modo Edição
            DataGrid.NavigationMode = NavigationMode.Cell;
            DataGrid.AllowEditing = true;
            DataGrid.SelectionMode = Syncfusion.SfDataGrid.XForms.SelectionMode.Multiple;
            #endregion

            #region Animação do Ícone de Classificação
            DataGrid.AllowSorting = true;
            DataGrid.GridStyle = new DataGridEscuroStyle();
            #endregion

            ListaPublicadorDetalhes = Model.PublicadorDetalhes.Lista();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            Type t = new Model.PublicadorDetalhes().GetType();
            PropertyInfo prop = t.GetProperty(e.Column.MappingName);
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
