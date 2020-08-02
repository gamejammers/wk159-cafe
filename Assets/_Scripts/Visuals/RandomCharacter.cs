//
// 
//

using UnityEngine;

namespace Cafe
{
    public class RandomCharacter
        : MonoBehaviour
    {
        //
        // members ////////////////////////////////////////////////////////////
        //
        
        public CharacterViewData viewData;
        private GameObject sprite;

        private static System.Random rnd                        = new System.Random();


        //
        // unity callbacks ////////////////////////////////////////////////////
        //
        
        protected virtual void Start()
        {
            Randomize();
        }

        [ContextMenu("Randomize")]
        private void Randomize()
        {
            if(viewData != null)
            {
                if(sprite != null)
                {
                    #if UNITY_EDITOR
                        DestroyImmediate(sprite);
                    #else
                        Destroy(sprite);
                    #endif
                }
                sprite = viewData.Create("sprite", rnd);
                sprite.transform.SetParent(transform, false);
            }
        }
    }
}
