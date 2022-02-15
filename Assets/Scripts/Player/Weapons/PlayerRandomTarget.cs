using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomTarget : MonoBehaviour
{
    /// <summary>
    /// Only target objects in these layers
    /// </summary>
    public LayerMask includeMask;

    /// <summary>
    /// Movement speed of the weapon
    /// </summary>
    public float movementSpeed = 10f;

    /// <summary>
    /// Look for enemies with in this range
    /// </summary>
    public float range = 20f;

    /// <summary>
    /// Target a random object and quit the script
    /// </summary>
    /// <returns>Nothing</returns>
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 dir;

        // Find all possible targets within range
        Collider2D[] possibleTargets =
            Physics2D.OverlapCircleAll(
                transform.position,
                range, includeMask);

        // Are there any targets available?
        if (possibleTargets.Length > 0)
        {
            // Pick a random target
            GameObject target = possibleTargets[Random.Range(0, possibleTargets.Length)].gameObject;

            dir = target.transform.position - transform.position;
        }
        else
        {
            // There were no targets, so go in a random direction
            dir = Random.insideUnitCircle;
        }

        // Move towards the target
        rb.velocity = dir.normalized * movementSpeed;

        // Aim the rocket towards the target
        float angle = Vector2.SignedAngle(Vector2.up, dir);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
