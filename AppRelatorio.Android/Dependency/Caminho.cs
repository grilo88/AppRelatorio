using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppRelatorio.Banco;
using AppRelatorio.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(Caminho))]
namespace AppRelatorio.Droid.Dependency
{
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string arquivo)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path_db = Path.Combine(path, arquivo);
            return path_db;
        }
    }
}