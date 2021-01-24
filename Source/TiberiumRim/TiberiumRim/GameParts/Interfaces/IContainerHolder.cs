﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiberiumRim
{
    public interface IContainerHolder
    {
        TiberiumContainer Container { get; }
        void Notify_ContainerFull();
    }
}
