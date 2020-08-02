//
//
//

using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class CheapAssWander
    : MonoBehaviour
{
    //
    // members ////////////////////////////////////////////////////////////////
    //

    [Header("Settings")]
    public float wanderRange = 2f;
    public float moveSpeed = 2f;
    public float delay = 1f;

    private Vector3 start = Vector3.zero;
    
    //
    // unity callbacks ////////////////////////////////////////////////////////
    //

    protected virtual void Awake()
    {
        start = transform.position;
    }

    //
    // ------------------------------------------------------------------------
    //

    protected virtual void Start()
    {
        StartCoroutine(Wander());
    }

    //
    // ------------------------------------------------------------------------
    //

    private IEnumerator Wander()
    {
        var wait = new WaitForSeconds(delay);

        while(true)
        {
            Vector3 dst = new Vector3(
                Cafe.btRandom.Range(-wanderRange, wanderRange),
                0f,
                Cafe.btRandom.Range(-wanderRange, wanderRange)
            ) + start;

            float dist2 = 10f;
            while(dist2 > 1f)
            {
                Vector3 diff = dst - transform.position;
                dist2 = diff.sqrMagnitude;

                transform.position += diff.normalized * moveSpeed * Time.deltaTime;
                yield return null;
            }

            yield return wait;
        }
    }
}


