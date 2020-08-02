//
//
//

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{

    //
    // This class controls the HUD display for an individual character.
    //

    public class CafeCharacterPanel
        : UIElement
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Character character                              { get; private set; }

        [Header("Item UI")]
        public Image profilePic                                 = null;
        public TextMeshProUGUI nameText                         = null;
        public TextMeshProUGUI statusText                       = null;
        public Image healthMeter                                = null;
        public Image energyMeter                                = null;
        public Image crossSwords                                = null;

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void DisplayDetails()
        {
            Dbg.Assert(cafe != null && character != null, "Must have a cafe and character");
            if(cafe.characterDetails.isVisible)
            {
                cafe.characterDetails.Hide();
            }
            else
            {
                cafe.characterDetails.Show(cafe, character);
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public void SetData(CafeManager cafe, Character character)
        {
            this.cafe = cafe;
            this.character = character;
        }

        //
        // --------------------------------------------------------------------
        //

        public void Refresh()
        {
            if(!SetVisible(character != null))
                return;

            profilePic.sprite = character.headSprite;
            nameText.text = character.data.characterName;
            statusText.text = character.state.ToString();
            if(character.state == Character.State.Adventuring)
            {
                crossSwords.gameObject.SetActive(true);
            }
            else
            {
                crossSwords.gameObject.SetActive(false);
            }

            if(character.data.maxHealth > 0)
            {
                healthMeter.fillAmount = character.data.currentHealth / (float)character.data.maxHealth;
            }

            if(character.data.maxEnergy > 0)
            {
                energyMeter.fillAmount = character.data.currentEnergy / (float)character.data.maxEnergy;
            }
        }
    }
}
