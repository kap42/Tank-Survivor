using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveShield : MonoBehaviour
{
    public GameObject explosion = null;

    public int damage = 1;
    public int maxDamage = 1;

    public float downTime = 2;

    public float recoilAmount = .5f;

    Collider2D col;

    SpriteRenderer sr;

    Color spriteColor;

    bool triggered = false;

    float lerp = 1;

    int counter = 0;

    private void Start()
    {
        damage = maxDamage;

        col = GetComponent<Collider2D>();

        sr = GetComponent<SpriteRenderer>();

        spriteColor = sr.color;
    }

    private void Update()
    {
        int finalDamage = Mathf.Max(1, maxDamage - damage + 1);

        if (counter++ % finalDamage == 0)
        {
            spriteColor.r =
            spriteColor.g =
            spriteColor.b =
            spriteColor.a = 1;
        }
        else
        {
            spriteColor.r =
            spriteColor.g =
            spriteColor.b =
            spriteColor.a = lerp;
        }

        sr.color = spriteColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleShield(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleShield(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandleShield(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleShield(collision.gameObject);
    }

    private void HandleShield(GameObject collision)
    {
        if (!collision.CompareTag("Friendly") &&
            !collision.CompareTag("Player"))
        {
            var enemyStats = collision.GetComponent<Stats>();

            if (enemyStats != null)
            {
                Vector2 recoil = collision.transform.position - transform.position;

                recoil = recoil.normalized * recoilAmount;

                (int score, int remain, bool dead) = enemyStats.DoDamage(damage, recoil);

                if (dead)
                {
                    HandleTank.score += score;
                }

                damage = remain;

                lerp = Mathf.Lerp(0, 1, (float)damage / (float)maxDamage);

                if (damage <= 0)
                {
                    if (!triggered)
                    {
                        triggered = true;

                        col.enabled = false;

                        gameObject.SetActive(false);

                        Invoke("Reactivate", downTime);
                    }
                }
            }
        }
    }

    void Reactivate()
    {
        lerp = 1;

        col.enabled = true;

        triggered = false;

        damage = maxDamage;

        gameObject.SetActive(true);
    }
}
