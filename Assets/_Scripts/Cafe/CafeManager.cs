//
//
//
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    //
    // This class is responsible for coordinating all the logic while in the cafe.
    //

    public class CafeManager
        : MonoBehaviour
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        [Header("Configuration")]
        public CharacterViewData viewData                       = null;
        public WandererSimulation wandererSimulation = null;
        public DungeonSimulater dungeonSim                      = null;
        public int maxCharacters                                = 5;
        public int seed                                         = 0;
        public List<CharacterIngredients> starterIngredientPool = null;

        [Header("Interface")]
        public Canvas screenCanvas                              = null;
        public UIElement characterPanelParent                   = null;
        public CafeCharacterDetailPanel characterDetails        = null;

        [Header("Run Time")]
        public List<Character> characters                       = null;
        private System.Random rnd                               = null;
        public IngredientStorage ingredientStorage              = new IngredientStorage();

        public static CafeManager instance                      { get; private set; }

        //
        // public methods /////////////////////////////////////////////////////
        //

        public void Embark(Character character, Dungeon dungeon)
        {
            if(dungeon != null)
            {
                if(ingredientStorage.meal != null) ingredientStorage.meal.Apply(character);
                if(ingredientStorage.drink != null) ingredientStorage.drink.Apply(character);

                ingredientStorage.meal.Clear();
                ingredientStorage.drink.Clear();
                StartCoroutine(RaidDungeon(character, dungeon));
            }
        }
        
        //
        // private methods ////////////////////////////////////////////////////
        //
        
        private Vector3 GetSpawnPoint()
        {
            return new Vector3(
                rnd.NextFloat(-6f, 6f),
                0f,
                rnd.NextFloat(-3.5f, 3.5f)
            ) + new Vector3(-3.5f, 0f, -7f);
        }

        //
        // --------------------------------------------------------------------
        //

        private IEnumerator RaidDungeon(Character character, Dungeon dungeon)
        {
            var wait = new WaitForSeconds(dungeon.ReactionTime / 2f);

            character.state = Character.State.Adventuring;
            character.dungeonResults.Clear();

            DungeonSimulater.CharacterSimulationData simdata = 
                dungeonSim.StartDungeonSimulation(character.data, dungeon);

            DungeonSimulater.SimulationStepResult step = new DungeonSimulater.SimulationStepResult() {
                action = DungeonSimulater.Action.Nothing,
                message = System.String.Empty
            };

            while(step.action != DungeonSimulater.Action.Complete)
            {
                ///###hsmith $TODO store this in a log somewhere for displaying
                step = dungeonSim.SimulateDungeon(simdata);

                if(step.action == DungeonSimulater.Action.Death)
                {
                    ///###hsmith $TODO DEATH DETH DEAD
                    CleanDeadCharacter(character);
                    step.action = DungeonSimulater.Action.Complete;
                }
                character.dungeonResults.Add(step);

                yield return wait;
            }

            dungeonSim.EndDungeonSimulation(simdata);
            character.state = Character.State.Returned;
        }
        void CleanDeadCharacter(Character character)
        {
            character.cafeView.Destroy();
            characters.Remove(character);
        }

        //
        // unity callbacks ////////////////////////////////////////////////////
        //

        protected virtual void Awake()
        {
            Dbg.Assert(instance == null, "Multiple instances of CafeManager!");
            instance = this;
        }

        //
        // --------------------------------------------------------------------
        protected virtual void Start()
        {
            // create a list to hold our currently active characters
            characters = new List<Character>();

            // seed our random number generator for determinism
            if(seed == 0)
            {
                rnd = new System.Random();
            }
            else
            {
                rnd = new System.Random(seed);
            }

            // reset our UI
            characterPanelParent.Show();
            characterDetails.Hide();
        }

        //
        // --------------------------------------------------------------------
        //
        
        protected virtual void Update()
        {
            // if we are missing characters add them now.
            while(characters.Count < maxCharacters)
            {
                Character newChar = Character.Create(
                    this, 
                    viewData, 
                    starterIngredientPool, 
                    rnd, 
                    wandererSimulation
                );

                newChar.cafeView.Initialize(this, GetSpawnPoint());
                characters.Add(newChar);
            }
        }
    }
}

