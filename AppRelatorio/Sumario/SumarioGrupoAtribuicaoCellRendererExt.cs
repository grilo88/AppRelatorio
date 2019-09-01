using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppRelatorio.Sumario
{
    public class SumarioGrupoAtribuicaoCellRendererExt : GridGroupSummaryCellRenderer
    {
        public SumarioGrupoAtribuicaoCellRendererExt()
        {
        }

        public override void OnInitializeDisplayView(DataColumnBase dataColumn, SfLabel view)
        {
            base.OnInitializeDisplayView(dataColumn, view);
            base.OnInitializeDisplayView(dataColumn, view);
            view.HorizontalTextAlignment = TextAlignment.Center;
            view.BackgroundColor = Color.Gray;
            view.FontAttributes = FontAttributes.Italic;
            view.FontSize = 20;
            view.TextColor = Color.White;
        }
    }
}
