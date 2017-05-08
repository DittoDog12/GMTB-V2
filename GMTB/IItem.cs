using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTB
{
    /// <summary>
    /// Collectable Item specific interface
    /// </summary>
    interface IItem
    {
        bool Collected { get; }
    }
}
