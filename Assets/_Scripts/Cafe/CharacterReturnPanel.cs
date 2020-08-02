//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // This panel is displayed for a character that has returned from an adventure
    // and has goodies to share.
    //

    public class CharacterReturnPanel
        : CharacterDetailSubPanel
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        [Header("Item UI")]
        public CharacterStatsPanel stats;
        public TextMeshProUGUI dungeonName;
        public UIList ingredients;
        
        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Finished()
        {

            character.manager.ingredientStorage.AddIngredients(character.data.ingredients);

            character.state = Character.State.Ready;
            cafe.characterDetails.Hide();
            cafe.characterDetails.Refresh();
            cafe.characterDetails.Show();
        }

        //
        // --------------------------------------------------------------------
        //

        public void Refresh()
        {
            if(!SetVisible(character != null && character.state == Character.State.Returned))
                return;

            stats.Refresh(character);

            Dungeon prevDungeon = character.data.previousDungeon;
            dungeonName.text = prevDungeon != null ? prevDungeon.DungeonName : "Distant Lands";

            ingredients.SetData(character.data.ingredients);
        }
    }
}
