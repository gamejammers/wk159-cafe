//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class DungeonItem
        : UIListItem
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Dungeon dungeon                                  { get { return data as Dungeon; } }
        
        [Header("Item UI")]
        public TextMeshProUGUI dungeonNameText;
        public Image icon;
        public TextMeshProUGUI dungeonLevel;
        public UIList attributes;
        public UIList rewards;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public override void Refresh()
        {
            base.Refresh();

            Dungeon dng = dungeon;
            if(dng == null) return;

            dungeonNameText.text = dng.DungeonName;
            icon.sprite = dng.Icon;
            dungeonLevel.text = dng.DungeonLevel.ToString();
            
            if(attributes != null)
            {
                attributes.SetData(dng.DungeonAttributes.Map(attr=>attr.ToString()));
            }

            if(rewards != null)
            {
                rewards.SetData(dng.Rewards);
            }
        }
    }
}
