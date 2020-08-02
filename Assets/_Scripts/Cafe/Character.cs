//
//
//

using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    //
    // This class is responsible for maintaining all that data for a character both
    // in the cafe, as well as how it is passed to and handled by the simulation.  This
    // includes things like the CharacterData (stats), the current state of the character,
    // and visual elements for the character (both in game and in the hud).
    //
    // This class is also (currently) responsible for creating new characters
    // although this should probably change.
    //

    [System.Serializable]
    public class Character
    {
        //
        // types //////////////////////////////////////////////////////////////
        //

        public enum State
        {
            Ready,          // character is chilling in the cafe, ready for the next adventure
            Adventuring,    // character is out and about adventuring
            Returned,       // character has returned to the cafe with rewards to unload
            Dead,           // the character has passed away, clean it up and create a new one!
        }
        
        //
        // members ////////////////////////////////////////////////////////////
        //

        [Header("State")]
        public State state                                      = State.Ready;
        public CharacterData data                               = null;
        public CharacterViewData view                           = null;
        public int seed                                         = 0;

        [Header("Visuals")]
        public Sprite headSprite                                = null;
        public Sprite bodySprite                                = null;
        public Sprite feetSprite                                = null;
        public Sprite handsSprite                               = null;

        [Header("Cafe GameObjects")]
        public CafeManager manager                              = null;
        public CafeCharacterView cafeView                       = null; // the sprite showing the character in the cafe
        public List<DungeonSimulater.SimulationStepResult> dungeonResults = new List<DungeonSimulater.SimulationStepResult>();

        //
        // initialize /////////////////////////////////////////////////////////
        //
        
        public static Character Create(CafeManager cafeManager, CharacterViewData view, List<CharacterIngredients> inventory, System.Random rnd, WandererSimulation wandererSim)
        {
            // pick our starting stats
            ///hsmith $TODO: This probably doesn't belong here.  we should have a way
            // of spawning character data based on classes mabye?  Barbarians,
            // rangers, etc.

            int maxHealth = 8 + rnd.Next(4);
            int initialArmor = 5 + rnd.Next(7);
            int initialEnergy = 10 + rnd.Next(3);

            // pick our visuals
            int variationCount = view.GetVariationCount();
            int headIdx = rnd.Next(variationCount);
            int bodyIdx = rnd.Next(variationCount);
            int feetIdx = rnd.Next(variationCount);
            int handsIdx = rnd.Next(variationCount);

            //
            // create the character!
            //
            Character newCharacter = new Character() {

                //
                // state
                //

                state = State.Returned,
                data = new CharacterData(
                    maxHealth, 
                    initialArmor, 
                    initialEnergy, 
                    possibleNames[rnd.Next(possibleNames.Length)]
                ),
                view = view,
                seed = rnd.Next(),

                //
                // visuals
                //

                headSprite = view.CreateSprite(CharacterViewData.Part.Head, headIdx),
                bodySprite = view.CreateSprite(CharacterViewData.Part.Body, bodyIdx),
                feetSprite = view.CreateSprite(CharacterViewData.Part.Feet, feetIdx),
                handsSprite = view.CreateSprite(CharacterViewData.Part.Hands, handsIdx),

                //
                // cafe view 
                // We fill this in after we have the rest of the character created.

                cafeView = null,
                manager = cafeManager,
            };

            //
            // add some random mods
            //

            int numMods = rnd.Next(8);
            for(int i = 0; i < numMods; ++i)
            {
                CharacterModificationEnum mod = EnumUtil.Random<CharacterModificationEnum>(rnd);
                int delta = rnd.Next(1,5);
                newCharacter.data.UpdateStat(mod, delta);
            }

            //
            // add the ingredients
            //

            int numIngredients = rnd.Next(5);
            for(int i = 0; i < numIngredients; ++i)
            {
                CharacterIngredients ingredient = inventory.Random(rnd);
                newCharacter.data.ingredients.Add(ingredient);
            }

            //
            // cafe sprite!
            //

            newCharacter.cafeView = CafeCharacterView.Create(newCharacter, wandererSim);

            //
            // return the finished character
            //

            return newCharacter;

        }

        //
        // constants //////////////////////////////////////////////////////////
        //

        //
        ///hsmith $TODO this probably doesn't belong here, we should have a data file
        // we load to get names?
        //
        public static string[] possibleNames                           = new string[] {
           "Lydan",
           "Syrin",
           "Ptorik",
           "Joz",
           "Varog",
           "Gethrod",
           "Hezra",
           "Feron",
           "Ophni",
           "Colborn",
           "Fintis",
           "Gatlin",
           "Jinto",
           "Hagalbar",
           "Krinn",
           "Lenox",
           "Revvyn",
           "Hodus",
           "Dimian",
           "Paskel",
           "Kontas",
           "Weston",
           "Azamarr",
           "Jather",
           "Tekren",
           "Jareth",
           "Adon",
           "Zaden",
           "Eune",
           "Graff",
           "Tez",
           "Jessop",
           "Gunnar",
           "Pike",
           "Domnhar",
           "Baske",
           "Jerrick",
           "Mavrek",
           "Riordan",
           "Wulfe",
           "Straus",
           "Tyvrik",
           "Henndar",
           "Favroe",
           "Whit",
           "Jaris",
           "Renham",
           "Kagran",
           "Lassrin",
           "Vadim",
           "Arlo",
           "Quintis",
           "Vale",
           "Caelan",
           "Yorjan",
           "Khron",
           "Ishmael",
           "Jakrin",
           "Fangar",
           "Roux",
           "Baxar",
           "Hawke",
           "Gatlen",
           "Barak",
           "Nazim",
           "Kadric",
           "Paquin",
        };

    }
}
