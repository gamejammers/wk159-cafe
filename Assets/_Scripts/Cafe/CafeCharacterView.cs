//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Cafe
{
    //
    // this represents all the visual information for a character, including
    // the sprite in the cafe, the HUD in the cafe, and the display for adventures.
    //

    public class CafeCharacterView
        : MonoBehaviour
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public Character character                              { get; private set; }
        public CafeManager cafe                                 { get; private set; }

        [Header("Config")]
        public GameObject spriteRootPrefab                      = null;
        public CafeCharacterPanel panelPrefab                   = null;

        private GameObject spriteRoot                           = null;
        private System.Random rnd                               = null;
        private Vector3 startPos                                = Vector3.zero;

        private CafeCharacterPanel panel                        = null;
        Transform cameraTransform = null;
        //
        // initialize /////////////////////////////////////////////////////////
        //

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        public static CafeCharacterView Create(Character character, WandererSimulation wandererSim)
        {
            if(character == null) 
            {
                Debug.LogError("Attempting to create a view for a null character.");
                return null;
            }

            if(character.view == null)
            {
                Debug.LogError("Character does not have a view!");
                return null;
            }

            if(character.view.cafeViewPrefab == null)
            {
                Debug.LogError("Character does not have a cafe view prefab");
                return null;
            }

            CafeCharacterView result = GameObject.Instantiate(character.view.cafeViewPrefab) as CafeCharacterView;
            if(result == null)
            {
                Debug.LogError("Failed to create cafe view prefab!");
                return null;
            }

            Wanderer wandering = result.GetComponent<Wanderer>();
            wandererSim.AddWanderingPoints(wandering);
            wandererSim.AddCharacter(wandering);
            result.gameObject.name = character.data.characterName;
            result.character = character;
            return result;
        }
        
        public void Destroy()
        {
            Destroy(panel.gameObject);
            Destroy(gameObject);
            cafe.wandererSimulation.RemoveCharacter( GetComponent<Wanderer>() );
        }
        //
        // --------------------------------------------------------------------
        //
        
        public void Initialize(CafeManager cafe, Vector3 startPos)
        {
            this.cafe = cafe;
            // Danny's Change -- sRoot == SpriteRoot
            spriteRoot = GameObject.Instantiate(spriteRootPrefab.gameObject);
            PositionConstraint positionConstraint = spriteRoot.GetComponent<PositionConstraint>();

            ConstraintSource positionConstraintSrc = new ConstraintSource();
            positionConstraintSrc.sourceTransform = transform;
            positionConstraintSrc.weight = 1;
            positionConstraint.AddSource(positionConstraintSrc);
            positionConstraint.constraintActive = true;
            // End

            character.view.Create(spriteRoot.transform, character);
            rnd = new System.Random(character.seed);

            panel = Instantiate(panelPrefab) as CafeCharacterPanel;
            panel.SetData(cafe, character);
            panel.transform.SetParent(cafe.characterPanelParent.transform);

            this.startPos = startPos;
            transform.position = startPos;
        }
        
        private IEnumerator UpdatePanel()
        {
            var wait = new WaitForSeconds(0.1f);
            while(true)
            {
                if(panel != null) 
                    panel.Refresh();
                yield return wait;

                if(spriteRoot != null)
                {
                    spriteRoot.gameObject.SetActive(character.state != Character.State.Adventuring);
                }
            }
        }

        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected virtual void Start()
        {
            StartCoroutine(UpdatePanel());
        }
    }
}
