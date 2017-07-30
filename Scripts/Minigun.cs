using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Gun {

    private float minigunRPM = 600f;
    private float minigunDamage = 10f;
    private int minigunMaxAmmo = 100;
    private float minigunShootDist = 20f;
    private GunType minigunGunType = GunType.Auto;

    [SerializeField]
    private Animator gunAnim;

    private bool isBarrelSpinning = false;
    private float barrelSpinTime = 1f;
    private float nextTimeBarrelIdle = Mathf.Infinity;

    private void Start()
    {
        setGunStats(minigunDamage, minigunMaxAmmo, minigunRPM, minigunGunType, minigunShootDist);

        audioSource = GetComponent<AudioSource>();
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI_HUD>();
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }

        if (gui)
        {
            gui.SetAmmoCount(currMagAmmo, maxMagAmmo);
        }

        // Index 0 shoot sound
        shootSound = gunSound[0];

        // Index 1 reload sound
        reloadSound = gunSound[1];
    }

    private void Update()
    {
        // Track when player stops firing the gunf
        if (!Input.GetButton("Shoot"))
        {
            if (nextTimeBarrelIdle != Mathf.Infinity)
            {
                // If window to shoot is past, barrel stops and must be revved up again
                if (Time.time > nextTimeBarrelIdle)
                {
                    // Fade spin and sound
                    nextTimeBarrelIdle = Mathf.Infinity;
                    gunAnim.SetBool("Spinning", false);
                    toggleShooting(false);
                    toggleBarrelRev(false);
                }
            }
            // When player stops shooting, start the window period where shooting is still valid
            else if (isBarrelSpinning)
            {
                nextTimeBarrelIdle = Time.time + barrelSpinTime;
            }
        }
        // Resets the window to stop revving when shooting
        else
        {
            nextTimeBarrelIdle = Mathf.Infinity;
        }
    }

    public override void Shoot()
    {
        if (canShoot())
        {
            /* Minigun barrel spurrs before shooting */
            // TODO add realistic spinning to minigun
            if(!isBarrelSpinning)
            {
                StartCoroutine(minigunRevUp());
            }
            // Bullets only come out if barrel is spinning
            else if (checkIsShooting())
            {
                fireBullet();
            }
        }
    }

    public override void fireBullet()
    {
        shootBullet(spawn.forward, shootDist);

        nextPossibleShotTime = Time.time + secondsBetweenShots;
        currMagAmmo--;

        if (gui)
        {
            gui.SetAmmoCount(currMagAmmo, maxMagAmmo);
        }

        //Play gun shoot sound
        audioSource.clip = shootSound;
        audioSource.Play();

        Rigidbody newShell = Instantiate(shell, shellEjectPoint.position, Quaternion.identity) as Rigidbody;
        newShell.AddForce(shellEjectPoint.forward * Random.Range(100f, 150f) + spawn.forward * Random.Range(-5f, 5f));
    }

    public void toggleBarrelRev(bool _set) { isBarrelSpinning= _set; }

    // Add minigun barrel revving
    IEnumerator minigunRevUp()
    {
        toggleBarrelRev(true);
        // Add sounds and barrel spinning animation
        gunAnim.SetBool("Spinning",true);
        
        yield return new WaitForSeconds(barrelSpinTime);
        toggleShooting(true);
    }
}
