using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    public class Inventory : IInventory
    {
        #region Data Members
        private List<IItem> AllItems;
        private Vector2 IconDisplayStart;
        private int IconOffset;
        #endregion

        #region Accessors
        public List<IItem> HeldItems
        {
            get { return AllItems; }
        }
        #endregion

        #region Constructor
        public Inventory()
        {
            AllItems = new List<IItem>();
            IconDisplayStart = new Vector2(25, 25);
            IconOffset = 25;
        }
        #endregion

        #region Methods        
        // Adds an item to the list, returns the position of the item within the item bar to the item so it can continue to display itself
        public Vector2 Add(IEntity item)
        {
            // Create a Vector 2 to hold the display postions
            Vector2 DisplayPos;
            // If first Item, display at default cooridinates
            if (AllItems.Count == 0)
                DisplayPos = new Vector2(IconDisplayStart.X, IconDisplayStart.Y);
            // If not first Item, add the offset multiplied by the number of icons
            // IE, Item 1 = x50y50, Item 2 = x75y50, Item 3 = x100y50 etc
            // ---- COULD RESULT IN OVERLAP IF EARLIER ITEM REMOVED BEFORE LATER ITEM ---- //
            // ---- NEED TO WORK ON FIX ---- //
            // --- Maybe array of locations? marked as free or not --- //
            else
                DisplayPos = new Vector2(IconDisplayStart.X + (IconOffset * AllItems.Count), IconDisplayStart.Y);
            AllItems.Add(item as IItem);
            return DisplayPos;
        }

        public void Remove(IEntity item)
        {
            AllItems.Remove(item as IItem);
        }
        #endregion
    }
}
