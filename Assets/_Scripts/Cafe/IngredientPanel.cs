//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cafe
{
    public class IngredientPanel
        : UIElement
        , IDropHandler
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Ingredient ingredient                            { get; private set; }
        public CraftingPanel parent                             { get; private set; }

        [Header("Item UI")]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI amountText;
        public Image picture;
        public UIList modifiers;

        public Sprite emptyIngredientSprite                     = null;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void SetParent(CraftingPanel parent)
        {
            this.parent = parent;
        }

        //
        // --------------------------------------------------------------------
        //

        public void OnDrop(PointerEventData ev)
        {
            GameObject drop = ev.pointerDrag;
            IngredientItemUI item = drop.GetComponent<IngredientItemUI>();

            if(item != null && item.ingredient!= null && item.ingredient.RemoveIngredients(1))
            {
                SetIngredient(item.ingredient.ingredient);
                parent.UpdateIngredients();
            }
        }

        //
        // --------------------------------------------------------------------
        //


        public void SetIngredient(Ingredient ingredient)
        {
            this.ingredient = ingredient;
            Refresh();
        }

        //
        // --------------------------------------------------------------------
        //
        
        public virtual void Refresh()
        {
            if(ingredient == null) // just cleans the sprite if ingredient null
            {
                if(picture != null)
                {
                    picture.sprite = emptyIngredientSprite;
                }
            }
            else
            {
                if(nameText != null)
                {
                    nameText.text = ingredient.IngredientName;
                }

                if(descriptionText != null)
                {
                    descriptionText.text = ingredient.Description;
                }

                if(picture != null)
                {
                    picture.sprite = ingredient.Image;
                }

                if(modifiers != null)
                {
                    modifiers.SetData(ingredient.CharacterModifiers);
                }
            }
        }
    }
}
