//
//
//

using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cafe
{
    //
    // This panel is displayed for a character that is actively adventuring
    //

    public class CharacterAdventurePanel
        : CharacterDetailSubPanel
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        [Header("Item UI")]
        public CharacterStatsPanel stats                        = null;
        public TextMeshProUGUI dungeonName                      = null;
        public UIList messageList                               = null;
        public Scrollbar progress                               = null;
        public Image faceImage                                  = null;
        
        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Refresh()
        {
            if(!SetVisible(character != null && character.state == Character.State.Adventuring))
                return;

            stats.Refresh(character);

            dungeonName.text = character.data.currentDungeon.DungeonName;

            if(character.dungeonResults.Count > 0)
            {
                messageList.SetData(character.dungeonResults.Map<DungeonSimulater.SimulationStepResult, string>(a=>a.message));
                progress.value = character.dungeonResults[character.dungeonResults.Count-1].progress;
            }
            else
            {
                progress.value = 0f;
            }

            faceImage.sprite = character.headSprite;

            if(progress.value >= 1f)
            {
                character.state = Character.State.Returned;
                parent.Hide();
                parent.Refresh();
                parent.Show();
            }
        }

        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected virtual void Start()
        {
            StartCoroutine(RefreshCoroutine());
        }

        //
        // ////////////////////////////////////////////////////////////////////
        //

        private IEnumerator RefreshCoroutine()
        {
            var wait = new WaitForSeconds(0.1f);
            while(true)
            {
                if(isVisible)
                {
                    Refresh();
                }
                yield return wait;
            }
        }
    }
}
