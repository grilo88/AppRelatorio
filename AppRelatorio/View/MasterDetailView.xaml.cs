using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRelatorio.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        public MasterDetailView()
        {
            InitializeComponent();
        }

        private void Pagina1_Tapped(object sender, EventArgs e)
        {
            Detail.Navigation.PushAsync(new BancoDeDadosView());
            IsPresented = false;
        }

        private void Pagina3_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private void Pagina2_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }
    }
}