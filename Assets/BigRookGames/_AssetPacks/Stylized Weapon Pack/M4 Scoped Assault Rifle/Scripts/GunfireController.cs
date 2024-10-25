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
        [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
            " the visible rocket attached to the rocket launcher")]
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

            // --- Instantiate projectile ---
            if (projectilePrefab != null)
            {
                Instantiate(
                    projectilePrefab,
                    muzzlePosition.transform.position,
                    muzzlePosition.transform.rotation,
                    null // Set parent to null if you don't want the projectile to be a child of the weapon
                );
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
                // Change pitch to give variation to repeated shots
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
