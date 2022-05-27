using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UniversalGunSystem : MonoBehaviour
{
    [Header("Gun Stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, accuracy, timeTilMaxSpread;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletShot;

    //hidden values
    bool shooting, readyToShoot, reloading;
    float usedSpread;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public Rigidbody player;

    [Header("Effects")]
    public CamShake camShake;
    public GameObject muzzleFlash, bulletHoleGraphics, bulletHoleFleshGraphics;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI magazineText;
    public float bulletHolesLoadAmount = 3f;
    public GameObject reloadPromptText;
    [Space()]
    public AudioClip ShootAudio;
    public AudioClip ReloadAudio;
    public AudioClip OutOfAmmoAudio;
    public AudioSource audioSource;
    //Recoil
    private GunRecoil recoilScript;
    //CamShake
    private float camShakeMagnitude = 0, camShakeDuration = 0;

    //BulletTrails
    [Header("Effects : Ricocheting")]
    [SerializeField] private LineRenderer trailLine;
    [SerializeField] private float shootDelay = 0.001f;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool bouncingBullets;
    [SerializeField] private float bounceDistance = 10f;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();
        recoilScript = transform.GetComponent<GunRecoil>();
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            MyInput();
        }

        //Set Text
        bulletText.SetText(bulletsLeft.ToString());
        magazineText.SetText(magazineSize.ToString());


        if (bulletsLeft == 0 && Input.GetKey(KeyCode.Mouse0))
        {
            reloadPromptText.SetActive(true);
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletShot = bulletsPerTap;
            Shoot();

            //Audio
            audioSource.PlayOneShot(ShootAudio);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !reloading && bulletsLeft == 0)
        {
            audioSource.PlayOneShot(OutOfAmmoAudio);
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        //-more spread on run
        if (player.velocity.magnitude > 0) usedSpread = spread * 1.5f;
        else usedSpread = spread;

        //-set randomness
        float x = Random.Range(-usedSpread, usedSpread);
        float y = Random.Range(-usedSpread, usedSpread);
        float z = Random.Range(-usedSpread, usedSpread);
        //-calculate direction/rotation/speed with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(-x, -y, -z);

        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Enemy")) rayHit.collider.GetComponent<Enemy>().GetHealthSystem().Damage(damage);
            else if (rayHit.collider.CompareTag("Boss1")) rayHit.collider.GetComponent<Boss1Logic>().GetHealthSystem().Damage(damage);
            else if (rayHit.collider.CompareTag("Shield")) rayHit.collider.GetComponent<PowerPillar>().GetHealthSystem().Damage(damage);

            BulletEffects(rayHit);

            //Graphics (Trails)
            SpawnBulletTrail(rayHit.point);
        }
        else
        {
            SpawnBulletTrail(direction * 1000);
        }

        //Shake camera
        camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics (Muzzle Flash)
        GameObject mf = Instantiate(muzzleFlash, attackPoint);
        mf.transform.parent = attackPoint;
        Destroy(mf, .5f);

        bulletsLeft--;
        bulletShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

        //Recoil
        recoilScript.RecoilFire();
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        audioSource.PlayOneShot(ReloadAudio);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        reloadPromptText.SetActive(false);
    }

    private void BulletEffects(RaycastHit rH)
    {
        if (rH.normal != Vector3.zero && !rH.collider.CompareTag("Enemy") && !rH.collider.CompareTag("Player"))
        {
            GameObject bh = Instantiate(bulletHoleGraphics, rH.point, Quaternion.LookRotation(rH.normal));
            Destroy(bh, bulletHolesLoadAmount);
        }
        if (rH.normal != Vector3.zero && rH.collider.CompareTag("Enemy") && !rH.collider.CompareTag("Player"))
        {
            rH.collider.GetComponent<Enemy>().GetHealthSystem().Damage(damage);

            GameObject bhf = Instantiate(bulletHoleFleshGraphics, rH.point, Quaternion.LookRotation(rH.normal));
            Destroy(bhf, 1);
        }
    }

    private void SpawnBulletTrail(Vector3 hitPoint)
    {
        GameObject trailEffect = Instantiate(trailLine.gameObject, attackPoint.position, Quaternion.identity);

        LineRenderer lineRen = trailEffect.GetComponent<LineRenderer>();

        lineRen.SetPosition(0, attackPoint.position);
        lineRen.SetPosition(1, hitPoint);

        Destroy(trailEffect, 0.5f);
    }
}
