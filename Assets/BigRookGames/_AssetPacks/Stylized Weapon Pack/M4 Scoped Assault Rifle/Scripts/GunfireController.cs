using UnityEngine;

namespace BigRookGames.Weapons
{
    public class GunfireController : MonoBehaviour
    {
        // --- Audio ---
        public AudioClip GunShotClip;
        public AudioSource source;
        public Vector2 audioPitch = new Vector2(.9f, 1.1f);

        // --- Muzzle ---
        public GameObject muzzlePrefab;
        public GameObject muzzlePosition;

        // --- Config ---
        public bool autoFire;
        public float shotDelay = .5f;
        public bool rotate = true;
        public float rotationSpeed = .25f;

        // --- Options ---
        public GameObject scope;
        public bool scopeActive = true;
        private bool lastScopeState;

        // --- Projectile ---
        [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
        public GameObject projectilePrefab;
        public float projectileSpeed = 50f;  // Speed of the projectile
        public GameObject projectileToDisableOnFire;

        // --- Timing ---
        [SerializeField] private float timeLastFired;

        private void Start()
        {
            if (source != null) source.clip = GunShotClip;
            timeLastFired = 0;
            lastScopeState = scopeActive;
        }

        private void Update()
        {
            // --- Rotate the weapon if enabled ---
            if (rotate)
            {
                transform.localEulerAngles = new Vector3(
                    transform.localEulerAngles.x,
                    transform.localEulerAngles.y + rotationSpeed,
                    transform.localEulerAngles.z
                );
            }

            // --- Check if enough time has passed since last shot ---
            bool canFire = (timeLastFired + shotDelay) <= Time.time;

            // --- Fire weapon based on input ---
            if (canFire)
            {
                if (autoFire)
                {
                    FireWeapon();
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    FireWeapon();
                }
            }

            // --- Toggle scope based on public variable value ---
            if (scope && lastScopeState != scopeActive)
            {
                lastScopeState = scopeActive;
                scope.SetActive(scopeActive);
            }
        }

        /// <summary>
        /// Creates an instance of the muzzle flash.
        /// Also handles audio and projectile instantiation.
        /// </summary>
        public void FireWeapon()
        {
            // --- Update the time when the weapon was last fired ---
            timeLastFired = Time.time;

            // --- Spawn muzzle flash ---
            if (muzzlePrefab != null && muzzlePosition != null)
            {
                Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, muzzlePosition.transform);
            }

            // --- Instantiate and launch projectile ---
            if (projectilePrefab != null)
            {
                GameObject projectileInstance = Instantiate(
                    projectilePrefab,
                    muzzlePosition.transform.position,
                    Quaternion.LookRotation(Camera.main.transform.forward) // Ensures projectile goes straight to the center of the screen
                );

                Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Camera.main.transform.forward * projectileSpeed;  // Sets projectile speed
                }
            }

            // --- Disable any gameobjects, if needed ---
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(false);
                Invoke(nameof(ReEnableDisabledProjectile), 3f);
            }

            // --- Play gunshot sound ---
            if (source != null && GunShotClip != null)
            {
                source.pitch = Random.Range(audioPitch.x, audioPitch.y);
                source.PlayOneShot(GunShotClip);
            }
        }

        private void ReEnableDisabledProjectile()
        {
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(true);
            }
        }
    }
}
