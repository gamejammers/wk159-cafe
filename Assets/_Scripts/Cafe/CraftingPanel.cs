//
//
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class CraftingPanel
        : UIElement
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public CharacterReadyPanel parent                       { get; private set; }
        public Character character                              { get { return parent.character; } }

        public DungeonItem selectedDungeon;
        public UIList ingredients;

        public IngredientPanel drink1;
        public IngredientPanel drink2;
        public IngredientPanel drink3;
        public UIList drinkMods;

        public IngredientPanel meal1;
        public IngredientPanel meal2;
        public IngredientPanel meal3;
        public UIList mealMods;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Refresh(CharacterReadyPanel parent)
        {
            if(!SetVisible(parent != null && parent.selectedDungeon != null))
                return;

            this.parent = parent;

            selectedDungeon.Refresh(parent.selectedDungeon, 0);

            Recipe drinkRecipe = character.manager.ingredientStorage.drink;
            drink1.SetIngredient(drinkRecipe.first);
            drink2.SetIngredient(drinkRecipe.second);
            drink3.SetIngredient(drinkRecipe.third);

            Recipe mealRecipe = character.manager.ingredientStorage.meal;
            meal1.SetIngredient(mealRecipe.first);
            meal2.SetIngredient(mealRecipe.second);
            meal3.SetIngredient(mealRecipe.third);

            ingredients.SetData(character.manager.ingredientStorage.ingredients);

            var drinkm = new List<CharacterModifier>();
            TryGetMods(drinkm, drink1);
            TryGetMods(drinkm, drink2);
            TryGetMods(drinkm, drink3);
            drinkMods.SetData(drinkm);

            var mealm = new List<CharacterModifier>();
            TryGetMods(mealm, meal1);
            TryGetMods(mealm, meal2);
            TryGetMods(mealm, meal3);
            mealMods.SetData(mealm);
        }

        //
        // --------------------------------------------------------------------
        //

        public void UpdateIngredients()
        {
            character.manager.ingredientStorage.drink.first = drink1.ingredient;
            character.manager.ingredientStorage.drink.second = drink2.ingredient;
            character.manager.ingredientStorage.drink.third = drink3.ingredient;

            character.manager.ingredientStorage.meal.first = meal1.ingredient;
            character.manager.ingredientStorage.meal.second = meal2.ingredient;
            character.manager.ingredientStorage.meal.third = meal3.ingredient;

            // remove empties
            character.manager.ingredientStorage.Clean();

            Refresh(parent);
        }
        
        //
        // --------------------------------------------------------------------
        //

        public void SelectMap()
        {
            parent.SelectDungeon(null);
            parent.GotoMapSelection();
        }

        //
        // --------------------------------------------------------------------
        //
        
        public void Embark()
        {
            parent.Embark();
        }

        //
        // private methods ////////////////////////////////////////////////////
        //

        void TryGetMods(List<CharacterModifier> mods, IngredientPanel panel)
        {
            if(mods != null && panel != null && panel.ingredient != null)
            {
                mods.AddRange(panel.ingredient.CharacterModifiers);
            }
        }

        //
        // unity callbacks ////////////////////////////////////////////////////
        //
        
        protected override void Awake()
        {
            base.Awake();
            drink1.SetParent(this);
            drink2.SetParent(this);
            drink3.SetParent(this);

            meal1.SetParent(this);
            meal2.SetParent(this);
            meal3.SetParent(this);
        }
    }
}
