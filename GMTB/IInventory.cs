using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    public interface IInventory
    {
        List<IItem> HeldItems { get; }

        Vector2 Add(IEntity item);
        void Remove(IEntity item);
    }
}
