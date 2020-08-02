//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // This is a reusable panel that shows character status and stats in detail
    //

    public class CharacterStatsPanel
        : UIElement
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        [Header("Item UI")]
        public Image head;
        public Image body;
        public Image hands;
        public Image feet;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI stateText;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI energyText;

        public TextMeshProUGUI armor;

        public TextMeshProUGUI thirst;
        public TextMeshProUGUI hunger;

        public TextMeshProUGUI heatRes;
        public TextMeshProUGUI coldRes;
        public TextMeshProUGUI poisonRes;
        public TextMeshProUGUI shockRes;
        public TextMeshProUGUI impactRes;
        public TextMeshProUGUI pierceRes;

        //
        // public methods /////////////////////////////////////////////////////
        //
        
        public void Refresh(Character character)
        {
            if(!SetVisible(character != null))
                return;

            head.sprite = character.headSprite;
            body.sprite = character.bodySprite;
            hands.sprite = character.handsSprite;
            feet.sprite = character.feetSprite;

            nameText.text = character.data.characterName;
            stateText.text = character.state.ToString();

            healthText.text = System.String.Format("{0}/{1}", character.data.currentHealth, character.data.maxHealth);
            energyText.text = System.String.Format("{0}/{1}", character.data.currentEnergy, character.data.maxEnergy);

            armor.text = character.data.armor.ToString();

            thirst.text = character.data.thirst.ToString();
            hunger.text = character.data.hunger.ToString();

            heatRes.text = character.data.heatResistance.ToString();
            coldRes.text = character.data.coldResistance.ToString();
            poisonRes.text = character.data.poisonResistance.ToString();
            shockRes.text = character.data.shockResistance.ToString();
            impactRes.text = character.data.impactResistance.ToString();
            pierceRes.text = character.data.piercingResistance.ToString();
        }
    }
}
