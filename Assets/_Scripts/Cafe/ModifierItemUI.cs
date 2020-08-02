//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class ModifierItemUI
        : UIListItem
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public CharacterModifier mod                            
        { 
            get 
            { 
                Dbg.Assert(data is CharacterModifier, "Data is not character modifier!");
                return (CharacterModifier)data;
            } 
        }

        [Header("Item UI")]
        public TextMeshProUGUI modType;
        public TextMeshProUGUI modAmount;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public override void Refresh()
        {
            base.Refresh();

            CharacterModifier m = mod;
            modType.text = m.modificationType.ToString();
            modAmount.text = m.modificationAmmount.ToString();
        }
    }
}
