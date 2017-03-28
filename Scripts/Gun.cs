using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Gun : MonoBehaviour {

    public enum GunType { Semi, Auto };

    // Gun variables
    public GunType gunType;
    [SerializeField]
    private float rpm = 450f;
    private AudioSource gunSound;
    public float gunID;
    public LayerMask collisionMask;
    [SerializeField]
    private float gunDamage = 2f;

    // Bullet variables
    private float secondsBetweenShots;
    private float nextPossibleShotTime;

    // Components
    public Transform spawn;
    public Transform shellEjectPoint;
    public Rigidbody shell;
    private LineRenderer tracer;

    private void Start()
    {
        secondsBetweenShots = 60 / rpm;
        gunSound = GetComponent<AudioSource>();
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
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

            gunSound.Play();
            if(tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shootDist);
            }

            Rigidbody newShell = Instantiate(shell, shellEjectPoint.position, Quaternion.identity) as Rigidbody;
            newShell.AddForce(shellEjectPoint.forward * Random.Range(150f, 200f) + spawn.forward * Random.Range(-10f,10f));
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
        if(Time.time < nextPossibleShotTime )
        {
            canShoot = false;
        }

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);

        yield return null;
        tracer.enabled = false;
    }
}
