//
//
//

using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // This panel is displayed for a character that is actively adventuring
    //

    public class CharacterDetailSubPanel
        : UIElement
    {
        //
        // members ////////////////////////////////////////////////////////////
        //
        
        public Character character                              { get { return parent != null ? parent.character : null; } }
        public CafeCharacterDetailPanel parent                  { get; private set; }

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void SetData(CafeCharacterDetailPanel parent)
        {
            cafe = parent.cafe;
            this.parent = parent;
        }
    }
}
