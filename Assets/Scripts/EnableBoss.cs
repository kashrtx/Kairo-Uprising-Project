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

    public PlayEnemyMusic script;

    private bool BossIsSpawned = false;

    void Update()
    {
        // Check if all minions are destroyed
        if (!BossIsSpawned && minion1 == null && minion2 == null && minion3 == null && minion4 == null) 
        {
            bossGameObject.SetActive(true);  // Enable the boss

            if (script != null)
            {
                script.PlayBossMusic();
            }

            //Destroy(this);  // Destroy this script so it doesn't keep checking
            BossIsSpawned=true;
        }

        if(bossGameObject == null)
        {
            script.StopMusic();

            

        }
    }
}
