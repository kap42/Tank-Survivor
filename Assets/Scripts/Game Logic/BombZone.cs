using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombZone : MonoBehaviour
{
    public GameObject explosion;

    bool done = false;

    private void Start()
    {
        Instantiate(
            explosion,
            transform.position,
            Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        done = true;

        if (!collision.gameObject.CompareTag("Rock"))
        {
            Stats enemyScore = collision.GetComponent<Stats>();

            if (enemyScore != null)
            {
                if (enemyScore.explosion != null)
                {
                    Destroy(Instantiate(
                        enemyScore.explosion,
                        collision.transform.position,
                        Quaternion.identity), 1f);
                }
            }

            Destroy(collision.gameObject);
        }
    }

    private void LateUpdate()
    {
        if(done)
        {
            Destroy(gameObject);
        }
    }
}
