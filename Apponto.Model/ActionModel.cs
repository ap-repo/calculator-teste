using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class ActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum ActionEnum
    {
        Inicio = 1,
        Pause = 2,
        VoltaPausa = 3,
        Saida = 4
    }
}
