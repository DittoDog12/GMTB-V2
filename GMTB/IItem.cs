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
    public interface IItem
    {
        bool Collected { get; }
        string ItemID { get; }
        void setVars(string name);
    }
}
