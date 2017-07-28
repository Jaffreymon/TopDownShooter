using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

    public enum GunType { Semi, Auto };

    // Gun variables
    public GunType gunType;
    [SerializeField]
    protected float rpm = 450f;
    [SerializeField]
    protected AudioClip[] gunSound;
    [SerializeField]
    protected float gunDamage = 2f;
    [SerializeField]
    protected int maxMagAmmo;
    protected int currMagAmmo;

    public float gunID;
    public LayerMask collisionMask;
    protected AudioSource audioSource;
    protected AudioClip shootSound;
    protected AudioClip reloadSound;
    protected bool reloading;

    // Bullet variables
    protected float secondsBetweenShots;
    protected float nextPossibleShotTime;
    protected float shootDist;

    // Components
    public Transform spawn;
    public Transform shellEjectPoint;
    public Rigidbody shell;
    
    protected GUI_HUD gui;
    protected LineRenderer tracer;

    protected void setGunStats(float _gunDamage, int _maxAmmo, float _rpm, GunType _gunType, float _shootDist)
    {
        gunDamage = _gunDamage;
        maxMagAmmo = _maxAmmo;
        rpm = _rpm;
        gunType = _gunType;
        shootDist = _shootDist;

        secondsBetweenShots = 60 / rpm;
        currMagAmmo = maxMagAmmo;
    }


	public virtual void Shoot()
    {
        if (canShoot())
        {
            fireBullet();
        }
    }

    public void ShootAuto()
    {
        if(gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    protected bool canShoot() {
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

    // Shoots a single straight ray 
    public virtual void shootBullet(Vector3 _bulletDir, float shootDist)
    {
        Ray ray = new Ray(spawn.position, _bulletDir);
        RaycastHit hit;

        Debug.Log("In Gun:shootBullet " + shootDist);
        if (Physics.Raycast(ray, out hit, shootDist, collisionMask))
        {
            shootDist = hit.distance;

            if (hit.collider.GetComponent<Entity>())
            {
                hit.collider.GetComponent<Entity>().takeDamage(gunDamage);
            }
        }

        if (tracer)
        {
            StartCoroutine("RenderTracer", ray.direction * shootDist);
        }
    }

    public virtual void fireBullet()
    {
        Debug.Log("In Gun:Firebullet " + shootDist);
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
