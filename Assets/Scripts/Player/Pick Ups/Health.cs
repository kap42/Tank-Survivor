using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Health : MonoBehaviour
{
    public int hpAdd = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.gameObject.CompareTag("Player"))
        {
            HandleTank ht = collision.transform.parent.GetComponent<HandleTank>();

            ht.hp += hpAdd;

            ht.hp = Mathf.Max(ht.maxHP, ht.hp);

            ht.ShowHealth();
        }
    }
}
