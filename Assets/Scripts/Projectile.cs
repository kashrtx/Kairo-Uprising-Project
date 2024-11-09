using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 10;
    private Transform firePoint;
    private Vector3 direction;

    public void Initialize(Transform firePoint, Vector3 targetDirection)
    {
        this.firePoint = firePoint;
        this.direction = targetDirection.normalized;
        
        // Add slight random spread for less perfect accuracy
        float spread = 0.03f; // Adjust this value to control spread amount
        direction += new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            Random.Range(-spread, spread)
        );
        direction.Normalize();
        
        // Rotate the projectile to face the direction it's moving
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        // Move in the direction we calculated
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            ResetPosition();
        }
        else if (!(other.CompareTag("Player")))
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        if (firePoint != null)
        {
            transform.position = firePoint.position;
            transform.rotation = firePoint.rotation;
        }
    }
}
