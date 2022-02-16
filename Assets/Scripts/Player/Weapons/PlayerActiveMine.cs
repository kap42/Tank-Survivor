using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveMine : MonoBehaviour
{
    public GameObject explosion = null;

    public LayerMask thingsToMove;
    public LayerMask possibleMines;

    public float damageArea = 2;

    public int damage = 1;
    public int hp = 1;
    
    public bool active = true;

    public float recoilAmount = .5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleBullet(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleBullet(other.gameObject);
    }

    public void HandleBullet(GameObject collision)
    {
        if (!collision.CompareTag("Friendly") &&
            !collision.CompareTag("Player"))
        {
            Collider2D[] possibleTargets = new Collider2D[500];

            active = false;

            int targetsFound = Physics2D.OverlapCircleNonAlloc(
                transform.position, damageArea,
                possibleTargets, thingsToMove);

            for (int i = 0; i < targetsFound; i++)
            {
                var target = possibleTargets[i];

                var enemyStats = target.GetComponent<Stats>();

                if (enemyStats != null)
                {
                    Vector2 recoil = target.transform.position - transform.position;

                    recoil = recoil.normalized * recoilAmount;

                    (int score, int remainingDamage, bool died) = enemyStats.DoDamage(damage, recoil);

                    if (died)
                    {
                        HandleTank.score += score;
                    }
                }
            }

            gameObject.tag = "Lethal";

            targetsFound = Physics2D.OverlapCircleNonAlloc(
                transform.position, damageArea,
                possibleTargets, possibleMines);

            for (int i = 0; i < targetsFound; i++)
            {
                var target = possibleTargets[i];

                var enemyStats = target.GetComponent<PlayerActiveMine>();

                if (enemyStats != null && enemyStats.active)
                {
                    enemyStats.active = false;
                    
                    enemyStats.HandleBullet(gameObject);
                }
            }

            if (explosion != null)
            {
                var go = Instantiate(
                    explosion,
                    transform.position,
                    transform.rotation);

                // go.transform.localScale = Vector2.one * (damageArea / 2.0f);
            }

            Destroy(gameObject);
        }
    }
}
