using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBossOnDestroy : MonoBehaviour
{
    public GameObject bossGameObject;
    public GameObject minion1;
    public GameObject minion2;
    public GameObject minion3;
    public GameObject minion4;

    void Update()
    {
        // Check if all minions are destroyed
        if (minion1 == null && minion2 == null && minion3 == null && minion4 == null)
        {
            bossGameObject.SetActive(true);  // Enable the boss
            Destroy(this);  // Destroy this script so it doesn't keep checking
        }
    }
}
