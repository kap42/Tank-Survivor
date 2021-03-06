using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveBullet : MonoBehaviour
{
    public GameObject explosion = null;

    public int maxDamage =1;
    public int hp = 1;

    public float recoilAmount = .5f;

    public int damage = 1;

    public float lifeTime = 5f;
    public void Reset()
    {
        damage = maxDamage;
        Invoke("TimeToDie",lifeTime);
    }

    void TimeToDie()
    {
        PlayerBullets.bulletPool.Release(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleBullet(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleBullet(other.gameObject);
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

                damage = remain;

                if (damage <= 0)
                {
                    if (explosion != null)
                    {
                        Instantiate(
                            explosion,
                            transform.position,
                            transform.rotation);
                    }

                    PlayerBullets.bulletPool.Release(gameObject);
                }
            }
        }
    }
}
