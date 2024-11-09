// credit to Dave / Game Development YouTube
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //Audio
    public AudioSource gunShotSound;
    public AudioSource reloadSound;
    public AudioSource emptyMagSound;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    //public GameObject muzzleFlash;
    public GameObject bulletHoleGraphic;
    public TextMeshProUGUI text;

     // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        //SetText
        text.SetText("Holy M4: " + bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }

        else if (shooting && !reloading && bulletsLeft <= 0)
        {
            // Play empty magazine sound
            emptyMagSound.Play();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;
        // Play gunshot sound
        gunShotSound.Play();

        if (muzzlePrefab != null && muzzlePosition != null)
        {
            Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, muzzlePosition.transform);
        }

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        //GameObject flash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity, attackPoint);
        //Destroy(flash, 0.1f);
        // --- Spawn muzzle flash ---
        

        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log($"Hit: {rayHit.collider.name} with Tag: {rayHit.collider.tag}");

            if (rayHit.collider.CompareTag("Kairo"))
            {
                rayHit.collider.GetComponent<MinionAI>().TakeDamage(damage);
            }
            else if (rayHit.collider.CompareTag("KairoBoss"))
            {
                rayHit.collider.GetComponent<BossAI>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("Hit an object that is not the target Kairo or Kairo Boss");
                //Instantiate(bulletHoleGraphic, rayHit.point , Quaternion.FromToRotation(Vector3.forward, rayHit.normal));
            }
        }



        //Graphics
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
         // Play reload sound
        reloadSound.Play();
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
