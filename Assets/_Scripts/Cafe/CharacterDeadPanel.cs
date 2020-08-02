//
//
//

using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // This panel is displayed for a character that is dead.
    //

    public class CharacterDeadPanel 
        : CharacterDetailSubPanel
    {
        //
        // members ////////////////////////////////////////////////////////////
        //
        
        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Refresh()
        {
            if(!SetVisible(character != null && character.state == Character.State.Dead))
                return;
        }
    }
}
