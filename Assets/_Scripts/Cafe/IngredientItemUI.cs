//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cafe
{
    public class IngredientItemUI
        : UIListItem
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public CharacterIngredients ingredient                  { get { return data as CharacterIngredients; } }

        [Header("Item UI")]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI amountText;
        public Image picture;
        public UIList modifiers;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public override void Refresh()
        {
            base.Refresh();

            CharacterIngredients cingr = ingredient;
            if(cingr == null) return;

            Ingredient ingr = cingr.ingredient;
            if(ingr != null && nameText != null)
            {
                nameText.text = ingr.IngredientName;
            }

            if(ingr != null && descriptionText != null)
            {
                descriptionText.text = ingr.Description;
            }

            if(cingr != null && amountText != null)
            {
                amountText.text = cingr.amount.ToString();
            }

            if(ingr != null && picture != null)
            {
                picture.sprite = ingr.Image;
            }

            if(ingr != null && modifiers != null)
            {
                modifiers.SetData(ingr.CharacterModifiers);
            }
        }

        //
        // protected methods //////////////////////////////////////////////////
        //

        protected override UIElement MakeDragDisplay()
        {
            UIElement res = base.MakeDragDisplay();
            Image img = res.GetComponent<Image>();
            if(img != null)
            {
                img.sprite = picture.sprite;
            }

            return res;
        }
        
    }
}
