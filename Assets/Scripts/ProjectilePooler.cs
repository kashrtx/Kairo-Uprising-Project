using UnityEngine;
using System.Collections.Generic;

public class ProjectilePooler : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;
    private Queue<GameObject> projectiles;

    private void Start()
    {
        projectiles = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectiles.Enqueue(projectile);
        }
    }

    public GameObject GetProjectile()
    {
        if (projectiles.Count > 0)
        {
            GameObject projectile = projectiles.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(true);
            return projectile;
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectiles.Enqueue(projectile);
    }
}
