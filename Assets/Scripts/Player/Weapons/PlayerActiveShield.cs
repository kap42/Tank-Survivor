using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveShield : MonoBehaviour
{
    public GameObject explosion = null;

    public int damage = 1;
    public int maxHP = 1;

    public float downTime = 2;

    public float recoilAmount = .5f;

    int hp;

    private void Start()
    {
        hp = maxHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleBullet(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleBullet(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandleBullet(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleBullet(collision.gameObject);
    }

    private void HandleBullet(GameObject collision)
    {
        if (!collision.CompareTag("Friendly") &&
            !collision.CompareTag("Player"))
        {
            var enemyScore = collision.GetComponent<Stats>();

            if (enemyScore != null)
            {
                Vector2 recoil = collision.transform.position - transform.position;

                recoil = recoil.normalized * recoilAmount;

                (int score, int remain, bool dead) = enemyScore.DoDamage(damage, recoil);

                if (dead)
                {
                    HandleTank.score += score;
                }

                damage -= remain;

                Debug.Log(damage);

                damage = 0;

                if (damage <= 0)
                {
                    gameObject.SetActive(false);

                    Invoke("Reactivate", downTime);
                }
            }
        }
    }

    void Reactivate()
    {
        hp = maxHP;

        gameObject.SetActive(true);
    }
}
