using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide : MonoBehaviour
{
    /// <summary>
    /// Kill object after a set amount of time
    /// </summary>
    /// <param name="n">How long to wait until destruction, defaults to 0</param>
    public void Die(int n = 0)
    {
        Destroy(gameObject, n);
    }
}
