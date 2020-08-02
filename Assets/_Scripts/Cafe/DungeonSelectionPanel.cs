//
//
//

using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class DungeonSelectionPanel
        : UIElement
    {
        //
        // members ////////////////////////////////////////////////////////////
        //
        
        public CharacterReadyPanel parent                       { get; private set; }
        public UIList availableDungeons;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Refresh(CharacterReadyPanel parent)
        {
            this.parent = parent;

            if(!SetVisible(parent.selectedDungeon == null))
                return;

            availableDungeons.SetData(parent.cafe.dungeonSim.GetAvailableDungeons());
        }

        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected virtual void Start()
        {
            availableDungeons.OnSelection += OnDungeonSelected;
        }

        
        //
        // --------------------------------------------------------------------
        //

        private void OnDungeonSelected(int idx, System.Object data)
        {
            Dungeon selected = data as Dungeon;
            Dbg.Assert(selected != null, "How did we get a null dungeon here?");
            parent.SelectDungeon(selected);
        }
        
        
    }
}
