//
//
//

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cafe
{
    public class CheapAssLevelLoader
        : MonoBehaviour
    {
        //
        // members ////////////////////////////////////////////////////////////
        //

        public int nextSceneId                                  = 1;
        
        //
        // public methods /////////////////////////////////////////////////////
        //

        public void NextScene()
        {
            SceneManager.LoadScene(nextSceneId, LoadSceneMode.Single);
        }
        
    }
}
