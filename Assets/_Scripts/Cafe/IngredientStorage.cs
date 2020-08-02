//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    public class IngredientStorage
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public IEnumerable ingredients                          { get { return _ingredients; } }
        private List<CharacterIngredients> _ingredients         = new List<CharacterIngredients>();
        public Recipe meal                                      = new Recipe();
        public Recipe drink                                     = new Recipe();

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Clean()
        {
            _ingredients.RemoveAll(i=>i.amount <=0);
        }

        //
        // --------------------------------------------------------------------
        //
        

        public void AddIngredients(IEnumerable<CharacterIngredients> newIngredients)
        {
            foreach(CharacterIngredients ingredient in newIngredients)
            {
                AddIngredients(ingredient);
            }
        }

        //
        // --------------------------------------------------------------------
        //
        
        public void AddIngredients(CharacterIngredients newIngredient)
        {
            CharacterIngredients ing = _ingredients.Find(i=>i.ingredient == newIngredient.ingredient);
            if(ing != null)
            {
                ing.amount += newIngredient.amount;
            }
            else
            {
                _ingredients.Add(newIngredient);
            }
        }
    }
}
