//
//
//

using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // this class is responsible for managing all the different detail panels
    // for a specific character
    //

    public class CafeCharacterDetailPanel
        : UIElement
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Character character                              { get; private set; }

        [Header("Subpanels")]
        public CharacterAdventurePanel adventure                = null;
        public CharacterDeadPanel dead                          = null;
        public CharacterReadyPanel ready                        = null;
        public CharacterReturnPanel finished                    = null;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Show(CafeManager cafe, Character character)
        {
            this.cafe = cafe;
            this.character = character;

            adventure.SetData(this);
            dead.SetData(this);
            ready.SetData(this);
            finished.SetData(this);

            Refresh();
            Show();
        }

        //
        // --------------------------------------------------------------------
        //
        
        public void Refresh()
        {
            //
            // visibility of this panel is externally controlled
            //

            adventure.Refresh();
            dead.Refresh();
            ready.Refresh();
            finished.Refresh();
        }
        
    }
}
