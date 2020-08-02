//
//
//

using UnityEngine;

[ExecuteInEditMode]
public class CheapAssBillboard
    : MonoBehaviour
{
    //
    // members ////////////////////////////////////////////////////////////////
    //
    [SerializeField, Range(1, 20)]
    private int frameUpdateInterval = 1;
    private Transform cam;
    private int frameCount = 0;

    //
    // unity callbacks ////////////////////////////////////////////////////////
    //

    private void OnEnable()
    {
        cam = Camera.main.transform;
    }

    protected virtual void LateUpdate()
    {
        frameCount++;
        if(cam != null && transform.forward != cam.forward && frameCount % frameUpdateInterval == 0)
        {
            transform.forward = cam.forward;
        }
    }
    
}