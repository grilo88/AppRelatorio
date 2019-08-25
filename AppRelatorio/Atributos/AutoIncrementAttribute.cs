using System;
using System.Collections.Generic;
using System.Text;

namespace AppRelatorio.Atributos
{
    public class AutoIncrementAttribute : Attribute
    {
        public bool Computed { get; set; }
    }
}
