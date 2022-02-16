using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    /// <summary>
    /// The object to spawn upon death
    /// </summary>
    public GameObject explosion;

    /// <summary>
    /// HP for the object
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Damage given
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Armour that is deducted from damage
    /// </summary>
    public int armour = 0;

    /// <summary>
    /// Larger objects should often have less recoil
    /// </summary>
    public float recoilModifier = 1;

    /// <summary>
    /// Score for killing object
    /// </summary>
    public int score = 1;

    /// <summary>
    /// Cache animator, used to flash on damage
    /// </summary>
    Animator flash;

    /// <summary>
    /// Cache the RigidBody2D
    /// </summary>
    Rigidbody2D rb;

    /// <summary>
    /// Instantiate the cached stuff
    /// </summary>
    void Start()
    {
        flash = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Used when a weapon damages an enemy
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    /// <param name="recoil">Magnitude and direction of recoil</param>
    /// <returns>Socre, Remaining Damage, Dead or Not</returns>
    public (int, int, bool) DoDamage(int damage, Vector2 recoil)
    {
        // Calculate the actual damage
        // Do we pierce the armour?
        int actualDamage = Mathf.Max(0, damage - armour);

        hp -= actualDamage;

        // If there was any damage, do a recoil
        // and flash the object
        if (actualDamage > 0)
        {
            rb.MovePosition(
                (Vector2)transform.position + recoil * recoilModifier);

            flash?.SetTrigger("Flash");
        }

        // Was the object killed?
        if (hp <= 0)
        {
            // Any remaining damage could be used on another enemy
            int remainingDamage = -hp;

            // Start an explosion (or something)
            Instantiate(
                explosion,
                transform.position,
                transform.rotation);

            // Debug/stats
            SpawnTimed.spawned--;

            // We should return the object to an object pool instead
            Destroy(gameObject);

            return (score, remainingDamage, true);
        }

        // It didn't die, so no score, no remaining damage.
        return (0, 0, false);
    }
}
