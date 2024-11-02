using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 10;
    private Transform firePoint;


    public void Initialize(Transform firePoint)
    {
        this.firePoint = firePoint;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a health component
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            ResetPosition(); // Teleport back to the original position
        }
        else if (!(other.CompareTag("Player")))
        {
            ResetPosition(); // Reset position if it hits any other object, like walls or ground
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
