﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class ValveActivatable : Activatable
{
    public abstract void Activate(float rotation);

}