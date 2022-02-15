using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject explosion;

    public int hp = 1;

    public int damage = 1;
    public int armour = 0;

    public float recoilModifier = 1;

    public int score = 1;

    Animator flash;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        flash = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    public (int, int, bool) DoDamage(int damage, Vector2 recoil)
    {
        int actualDamage = Mathf.Max(0, damage - armour);

        hp -= actualDamage;

        if (actualDamage > 0)
        {
        //    rb.velocity = Vector2.zero;
            rb.MovePosition((Vector2)transform.position + recoil * recoilModifier);

            flash?.SetTrigger("Flash");
        }

        if (hp <= 0)
        {
            int remainingDamage = -hp;

            Instantiate(
                explosion,
                transform.position,
                transform.rotation);

            Destroy(gameObject);

            return (score, remainingDamage, true);
        }

        return (0, 0, false);
    }
}
