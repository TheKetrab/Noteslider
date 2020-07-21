﻿using Noteslider.Model.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noteslider.Code.Render
{
    public interface IAssetRenderer
    {
        void Render(Asset a, params object[] p);
    }
}
