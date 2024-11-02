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
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    private bool hasSpottedPlayer = false;  // Flag to track player spotting
    
    public int health = 100;  // Add a health variable

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if player is within follow range
        if (distanceToPlayer <= followRange)
        {
            agent.SetDestination(player.position);

            // Check if player is within attack range and set spotted flag
            if (distanceToPlayer <= attackRange)
            {
                hasSpottedPlayer = true;
            }
        }
        else
        {
            agent.ResetPath();   // Stop following if out of follow range
            hasSpottedPlayer = false;  // Reset spotted flag if out of follow range
        }

        // Fire only if spotted and time is ready
        if (hasSpottedPlayer && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectileInstance.GetComponent<Projectile>().Initialize(firePoint);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);  // Destroy minion if health drops to 0 or below
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))  // Check if the colliding object is a projectile
        {
            TakeDamage(40);  // Subtract 40 health if hit by a projectile
            Destroy(other.gameObject);  // Optionally destroy the projectile on impact
        }
    }
}
