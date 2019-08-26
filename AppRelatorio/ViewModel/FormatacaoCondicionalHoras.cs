using AppRelatorio.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppRelatorio.ViewModel
{
    public class FormatacaoCondicionalHorasFreightTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Baixo { get; set; }

        public DataTemplate Medio { get; set; }

        public DataTemplate Alto { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            int value = (item as PublicadorDetalhes).Horas;
            if (value > 30)
                return Alto;
            else if (value > 10)
                return Medio;
            else
                return Baixo;
        }
    }
}
