//
//
//

using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // This panel is displayed for a character that is ready for an adventure
    //

    public class CharacterReadyPanel
        : CharacterDetailSubPanel
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public CharacterStatsPanel stats;
        public DungeonSelectionPanel selection;
        public CraftingPanel crafting;

        public Dungeon selectedDungeon                          { get; private set; }
        
        //
        // public methods /////////////////////////////////////////////////////
        //

        public void GotoMapSelection()
        {
            selectedDungeon = null;
            selection.Show();
            crafting.Hide();
        }

        //
        // --------------------------------------------------------------------
        //

        public void GotoCraft()
        {
            selection.Hide();
            crafting.Show();
        }

        //
        // --------------------------------------------------------------------
        //

        public void SelectDungeon(Dungeon dungeon)
        {
            selectedDungeon = dungeon;
            Refresh();
        }
        
        //
        // --------------------------------------------------------------------
        //
        
        public void Embark()
        {
            if(selectedDungeon != null)
            {
                cafe.Embark(character, selectedDungeon);
                selectedDungeon = null;
                cafe.characterDetails.Hide();
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public void Refresh()
        {
            if(!SetVisible(character != null && character.state == Character.State.Ready))
                return;

            stats.Refresh(character);

            selection.Refresh(this);
            crafting.Refresh(this);
        }

        //
        // --------------------------------------------------------------------
        //
        
        public override void Hide()
        {
            base.Hide();
            selectedDungeon = null;
        }

        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected override void Awake()
        {
            base.Awake();
            selectedDungeon = null;
        }
        
    }
}
