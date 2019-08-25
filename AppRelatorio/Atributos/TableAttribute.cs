using System;
using System.Collections.Generic;
using System.Text;

namespace AppRelatorio.Atributos
{
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }

        public TableAttribute(string Name)
        {
            this.Name = Name;
        }
        
    }
}
