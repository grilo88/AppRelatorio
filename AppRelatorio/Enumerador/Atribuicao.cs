using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AppRelatorio.Enumerador
{
    public static class Atribuicao
    {
        public static ObservableCollection<string> Lista()
        {
            List<string> lista = new List<string>();

            lista.Add("PU - Publicador Batizado");
            lista.Add("PU - Publicador Não Batizado");
            lista.Add("AC - Ancião");
            lista.Add("SM - Servo Ministerial");
            lista.Add("PA30 - Pioneiro Auxiliar 30 Horas");
            lista.Add("PA50 - Pioneiro Auxiliar 50 Horas");
            lista.Add("PR - Pioneiro Regular");
            lista.Sort();

            return new ObservableCollection<string>(lista);
        }
    }
}
