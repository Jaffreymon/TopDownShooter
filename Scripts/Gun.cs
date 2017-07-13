using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

    public enum GunType { Semi, Auto };

    // Gun variables
    public GunType gunType;
    [SerializeField]
    private float rpm = 450f;
    [SerializeField]
    private AudioClip[] gunSound;
    [SerializeField]
    private float gunDamage = 2f;
    [SerializeField]
    private int maxMagAmmo;
    [SerializeField]
    private int currMagAmmo;

    public float gunID;
    public LayerMask collisionMask;
    private AudioSource audioSource;
    private AudioClip shootSound;
    private AudioClip reloadSound;
    private bool reloading;

    // Bullet variables
    private float secondsBetweenShots;
    private float nextPossibleShotTime;

    // Components
    public Transform spawn;
    public Transform shellEjectPoint;
    public Rigidbody shell;
    
    private GUI_HUD gui;
    private LineRenderer tracer;

    private void Start()
    {
        secondsBetweenShots = 60 / rpm;
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


	public void Shoot()
    {
        if (canShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shootDist = 20f;

            if (Physics.Raycast(ray, out hit, shootDist, collisionMask))
            {
                shootDist = hit.distance;

                if(hit.collider.GetComponent<Entity>())
                {
                    hit.collider.GetComponent<Entity>().takeDamage(gunDamage);
                }
            }

            nextPossibleShotTime = Time.time + secondsBetweenShots;
            currMagAmmo--;

            if(gui)
            {
                gui.SetAmmoCount(currMagAmmo, maxMagAmmo);
            }

            //Play gun shoot sound
            audioSource.clip = shootSound;
            audioSource.Play();

            if(tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shootDist);
            }

            Rigidbody newShell = Instantiate(shell, shellEjectPoint.position, Quaternion.identity) as Rigidbody;
            newShell.AddForce(shellEjectPoint.forward * Random.Range(100f, 150f) + spawn.forward * Random.Range(-5f,5f));
        }
    }

    public void ShootAuto()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool canShoot() {
        bool canShoot = true;
        if(Time.time < nextPossibleShotTime || (0 >= currMagAmmo) || (reloading))
        {
            canShoot = false;
        }
        return canShoot;
    }

    public bool isReloading()
    {
        return reloading;
    }
    
    public bool Reload()
    {
        if (currMagAmmo != maxMagAmmo)
        {
            reloading = true;
            return true;
        }
        return false;
    }

    public void finishReload()
    {
        StartCoroutine(ReloadTime());
    }
    
    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);

        yield return null;
        tracer.enabled = false;
    }
    IEnumerator ReloadTime()
    {
        // Play gun reload sound
        audioSource.clip = reloadSound;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);

        reloading = false;
        currMagAmmo = maxMagAmmo;
        if (gui)
        {
            gui.SetAmmoCount(currMagAmmo, maxMagAmmo);
        }
    }
}
