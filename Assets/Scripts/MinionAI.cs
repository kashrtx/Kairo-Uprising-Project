using UnityEngine;
using UnityEngine.AI;

public class MinionAI : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float followRange = 15f;
    public float attackRange = 10f;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    private bool hasSpottedPlayer = false;
    public int health = 100;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= followRange)
        {
            agent.SetDestination(player.position);
            if (distanceToPlayer <= attackRange)
            {
                hasSpottedPlayer = true;
                // Make the minion look at the player
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(directionToPlayer);
            }
        }
        else
        {
            agent.ResetPath();
            hasSpottedPlayer = false;
        }

        if (hasSpottedPlayer && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        // Calculate direction to player
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
        
        // Create the projectile
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectileInstance.GetComponent<Projectile>().Initialize(firePoint, directionToPlayer);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(40);
            Destroy(other.gameObject);
        }
    }
}