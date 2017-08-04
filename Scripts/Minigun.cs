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
    private AudioClip rotatingBarrel;

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

        // Index 2 reload sound
        rotatingBarrel = gunSound[2];
    }

    private void Update()
    {
        // Track when player stops firing the gun
        if (!Input.GetButton("Shoot"))
        {
            if (nextTimeBarrelIdle != Mathf.Infinity)
            {
                // If window to shoot is past, barrel stops and must be revved up again
                if (Time.time > nextTimeBarrelIdle)
                {
                    
                    nextTimeBarrelIdle = Mathf.Infinity;

                    // Fade spin and sound
                    gunAnim.SetBool("Spinning", false);
                    audioSource.Stop();
                    audioSource.loop = false;
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
            /* Minigun barrel spurrs before shooting */
            if (!isBarrelSpinning)
            {
                StartCoroutine(minigunRevUp());
            }
            //Resets next time to stop shooting
            nextTimeBarrelIdle = Mathf.Infinity;
        }
    }

    public override void Shoot()
    {
        if (canShoot())
        {
            // Bullets only come out if barrel is spinning
            if (checkIsShooting())
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
        audioSource.PlayOneShot(shootSound);

        Rigidbody newShell = Instantiate(shell, shellEjectPoint.position, Quaternion.identity) as Rigidbody;
        newShell.AddForce(shellEjectPoint.forward * Random.Range(100f, 150f) + spawn.forward * Random.Range(-5f, 5f));
    }

    // Shoots a straight line; Skill shoots 2 rays
    public override void shootBullet(Vector3 _bulletDir, float shootDist)
    {
        // Straight pellet
        Ray straightRay = new Ray(spawn.position, _bulletDir * shootDist);

        if (checkSkillUsed())
        {
            float am = 12f;
            Ray skillRightRay = new Ray(spawn.position, Quaternion.Euler(0, am, 0) * straightRay.direction);
            Ray skillLeftRay = new Ray(spawn.position, Quaternion.Euler(0, -am, 0) * straightRay.direction);

            checkRayCollision(skillRightRay, shootDist);
            checkRayCollision(skillLeftRay, shootDist);
        }
        else
        {
            checkRayCollision(straightRay, shootDist);
        }


        if (tracer)
        {
            StartCoroutine("RenderTracer", straightRay.direction * shootDist);
        }
    }

    private void checkRayCollision(Ray _ray, float _shootDist)
    {
        RaycastHit hit;

        //Debug.DrawRay(_ray.origin, _ray.direction * _shootDist, Color.red, 10f);

        if (Physics.Raycast(_ray, out hit, _shootDist, collisionMask))
        {
            if (hit.collider.GetComponent<Entity>())
            {
                hit.collider.GetComponent<Entity>().takeDamage(gunDamage);
            }
        }
    }

    public void toggleBarrelRev(bool _set) { isBarrelSpinning= _set; }

    // Add minigun barrel revving
    IEnumerator minigunRevUp()
    {
        toggleBarrelRev(true);
        //Play barrel rotating sound
        audioSource.clip = rotatingBarrel;
        audioSource.loop = true;
        audioSource.Play();

        gunAnim.SetBool("Spinning",true);
        
        yield return new WaitForSeconds(barrelSpinTime);
        toggleShooting(true);
    }
}
