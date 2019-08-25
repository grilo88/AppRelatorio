using AppRelatorio.Banco;
using AppRelatorio.UWP.Dependency;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly:Dependency(typeof(Caminho))]
namespace AppRelatorio.UWP.Dependency
{
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string arquivo)
        {
            string path = ApplicationData.Current.LocalFolder.Path;
            string path_db = Path.Combine(path, arquivo);
            return path_db;
        }
    }
}
