using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AppRelatorio.Banco;
using Foundation;
using AppRelatorio.iOS.Dependency;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Caminho))]
namespace AppRelatorio.iOS.Dependency
{
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string arquivo)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path_lib = Path.Combine(path, "..", "Library");
            string path_db = Path.Combine(path_lib, arquivo);
            return path_db;
        }
    }
}