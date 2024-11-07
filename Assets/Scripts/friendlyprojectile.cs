using UnityEngine;

public class friendlyprojectile : MonoBehaviour
{
    public int damage = 40;  // Damage dealt by the projectile
    private Transform firePoint;


    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Weapon"))
        {
            Debug.Log("destroyed bullet");
            Destroy(this.gameObject);
            return;
        }
        

        // Check if the object has a health component or tag
        if (other.CompareTag("Kairo"))
        {
            MinionAI minion = other.GetComponent<MinionAI>();
            BossAI boss = other.GetComponent<BossAI>();

            // Apply damage if the component exists
            if (minion != null)
            {
                minion.TakeDamage(damage);
                Debug.Log("hit minion");
            }
            else if (boss != null)
            {
                boss.TakeDamage(damage);
                Debug.Log("hit boss");
            }

            // return if it hits
             transform.position = firePoint.position;
             transform.rotation = firePoint.rotation;
        }
        
    }
}
