using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppRelatorio.Enumerador
{
        public enum AtribuicaoEnum : byte
        {
            [Description("Publicador Batizado")]
            PublicadorBatizado = 1,

            [Description("Publicador Não Batizado")]
            PublicadorNaoBatizado = 2,

            [Description("Ancião")]
            Anciao = 3,

            [Description("Servo Ministerial")]
            ServoMinisterial = 4,

            [Description("Pioneiro Auxiliar 30 Horas")]
            PioneiroAuxiliar30Horas = 5,

            [Description("Pioneiro Auxiliar 50 Horas")]
            PioneiroAuxiliar50Horas = 6,

            [Description("Pioneiro Regular")]
            PioneiroRegular = 7,
    }
}
