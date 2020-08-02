//
//
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    public class Recipe
    {
        //
        // members ////////////////////////////////////////////////////////////
        //
        
        public Ingredient first                                 = null;
        public Ingredient second                                = null;
        public Ingredient third                                 = null;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Clear()
        {
            first = null;
            second = null;
            third = null;
        }

        //
        // --------------------------------------------------------------------
        //
        
        public List<CharacterModifier> GetModifiers()
        {
            var result = new List<CharacterModifier>();
            if(first != null)
                result.AddRange(first.CharacterModifiers);

            if(second != null)
                result.AddRange(second.CharacterModifiers);

            if(third != null)
                result.AddRange(third.CharacterModifiers);

            return result;
        }

        //
        // --------------------------------------------------------------------
        //

        public void Apply(Character character)
        {
            List<CharacterModifier> mods = GetModifiers();
            foreach(CharacterModifier mod in mods)
            {
                character.data.UpdateStat(mod.modificationType, mod.modificationAmmount);
            }
            Clean();
        }
        void Clean() // cleans the recipe
        {
            first = null;
            second = null;
            third = null;
        }
    }
}
