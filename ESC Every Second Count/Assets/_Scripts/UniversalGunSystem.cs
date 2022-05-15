using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UniversalGunSystem : MonoBehaviour
{
    [Header("Gun Stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
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
    public LayerMask whatIsEnemy;
    public Rigidbody player;

    [Header("Effects")]
    public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public GameObject muzzleFlash, bulletHoleGraphics, bulletHoleFleshGraphics;
    public TextMeshProUGUI bulletText;
    public float bulletHolesLoadAmount = 3f;
    public TextMeshProUGUI reloadPromptText;
    [Space()]
    public AudioClip ShootAudio;
    public AudioClip ReloadAudio;
    public AudioClip OutOfAmmoAudio;
    public AudioSource audioSource;
    //Recoil
    private GunRecoil recoilScript;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        audioSource = GetComponent<AudioSource>();
        recoilScript = transform.GetComponent<GunRecoil>();
    }

    private void Update()
    {
        MyInput();

        //Set Text
        bulletText.SetText(bulletsLeft + "/" + magazineSize);

        reloadPromptText.enabled = false;
        if(bulletsLeft == 0 && Input.GetKey(KeyCode.Mouse0))
        {
            reloadPromptText.enabled = true;
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
        //-set
        float x = Random.Range(-usedSpread, usedSpread);
        float y = Random.Range(-usedSpread, usedSpread);
        float z = Random.Range(-usedSpread, usedSpread);
        //-calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(-x, -y, -z);

        //Raycast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range))
        {
            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
        }

        //Shake camera
        camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        if (rayHit.normal != Vector3.zero && !rayHit.collider.CompareTag("Enemy") && !rayHit.collider.CompareTag("Player")) 
        {
            GameObject bh = Instantiate(bulletHoleGraphics, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            Destroy(bh, bulletHolesLoadAmount);
        }
        if (rayHit.normal != Vector3.zero && rayHit.collider.CompareTag("Enemy") && !rayHit.collider.CompareTag("Player"))
        {
            GameObject bhf = Instantiate(bulletHoleFleshGraphics, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            Destroy(bhf, 1);
        }
        GameObject mf = Instantiate(muzzleFlash, attackPoint);
        mf.transform.parent = attackPoint;
        Destroy(mf, .5f);

        bulletsLeft--;
        bulletShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShots);

        //Audio
        audioSource.PlayOneShot(ShootAudio);

        //Recoil
        recoilScript.RecoilFire();
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
    audioSource.PlayOneShot(ReloadAudio);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        reloadPromptText.enabled = false;
    }
}
