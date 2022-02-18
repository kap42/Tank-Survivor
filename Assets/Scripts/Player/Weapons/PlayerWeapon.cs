using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    /// <summary>
    /// What is to be used as a WeaponToLaunch?
    /// </summary>
    [SerializeField] 
    GameObject WeaponToLaunch;

    /// <summary>
    /// How many weapons should be spawned at once?
    /// </summary>
    [SerializeField]
    int numToLaunch = 1;

    /// <summary>
    /// How often should we fire the weapon?
    /// </summary>
    [SerializeField]
    float weaponCoolDownTime = 1f;

    /// <summary>
    /// Start firing and continue forever
    /// </summary>
    /// <returns>Nothing</returns>
    IEnumerator Start()
    {
        // Avoid garbage
        var wfs = new WaitForSeconds(weaponCoolDownTime);

        while (true)
        {
            for (int i = 0; i < numToLaunch; i++)
            {
                Instantiate(
                    WeaponToLaunch,
                    transform.position,
                    transform.rotation);
            }

            yield return wfs;
        }
    }
}
