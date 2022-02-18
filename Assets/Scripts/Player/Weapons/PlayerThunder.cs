using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThunder : MonoBehaviour
{
    /// <summary>
    /// Only target objects in these layers
    /// </summary>
    public LayerMask includeMask;

    /// <summary>
    /// Look for enemies with in this range
    /// </summary>
    public float range = 20f;

    /// <summary>
    /// Look for enemies with in this range
    /// </summary>
    public float shortRange = 4f;

    public GameObject energySphere;

    public float moveDelay = .1f;

    public int numberOfHits = 4;
    public int damage = 10;

    IEnumerator Start()
    {
        var wfs = new WaitForSeconds(moveDelay);

        yield return wfs;

        // Find all possible targets within range
        Collider2D[] possibleTargets =
            Physics2D.OverlapCircleAll(
                transform.position,
                range, includeMask);

        // Are there any targets available?
        if (possibleTargets.Length > 0)
        {
            // Pick a random target
            GameObject target =
                possibleTargets[
                    Random.Range(0, possibleTargets.Length)
                ].gameObject;

            transform.position = target.transform.position;
        }
        else
        {
            numberOfHits = 0;
        }

        for (int i = 1; i < numberOfHits; i++)
        {
            yield return wfs;

            // Find all possible targets within range
            possibleTargets =
                Physics2D.OverlapCircleAll(
                    transform.position,
                    shortRange, includeMask);

            // Are there any targets available?
            // If there is only one object, it's this object itself
            if (possibleTargets.Length > 1)
            {
                // Pick a random target
                GameObject target =
                    possibleTargets[
                        Random.Range(0, possibleTargets.Length)
                    ].gameObject;

                if (target == gameObject)
                {

                }

                transform.position = target.transform.position;

                GameObject go = Instantiate(
                    energySphere,
                    target.transform.position,
                    Quaternion.identity);

                Destroy(go, .4f);
            }
        }

        Destroy(gameObject,1f);
    }
}
