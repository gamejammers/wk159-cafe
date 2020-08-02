//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class DungeonRewardItem
        : UIListItem
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public DungeonReward reward                             { get { return data as DungeonReward; } }

        [Header("Item UI")]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI minAmountText;
        public TextMeshProUGUI maxAmountText;
        public Image picture;
        public UIList modifiers;

        
        //
        // public methods /////////////////////////////////////////////////////
        //

        public override void Refresh()
        {
            base.Refresh();

            DungeonReward rwd = reward;
            if(rwd == null) return;

            Ingredient ingr = rwd.Ingredient;

            if(ingr == null) return;

            if(picture != null)
            {
                picture.sprite = ingr.Image;
            }

            if(nameText != null)
            {
                nameText.text = ingr.IngredientName;
            }

            if(descriptionText != null)
            {
                descriptionText.text = ingr.Description;
            }

            if(minAmountText != null)
            {
                minAmountText.text = rwd.MinimumAmount.ToString();
            }

            if(maxAmountText != null)
            {
                maxAmountText.text = rwd.MaximumAmount.ToString();
            }

            if(modifiers != null)
            {
                modifiers.SetData(ingr.CharacterModifiers);
            }
        }
    }
}
