﻿using System;
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
        Start = 1,
        Break = 2,
        BreakBack = 3,
        End = 4
    }
}
