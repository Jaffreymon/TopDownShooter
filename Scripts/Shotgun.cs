using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    private float shotgunRPM = 70f;
    private float shotgunDamage = 143f;
    private int shotgunMaxAmmo = 7;
    private float shotgunShootDist = 6f;
    private GunType shotgunGunType = GunType.Semi;

    private float defaultEndSpread = 3f;
    private float skillEndSpread = 5f;


    private void Start()
    {
        setGunStats(shotgunDamage, shotgunMaxAmmo, shotgunRPM, shotgunGunType, shotgunShootDist);

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

    public override void Shoot()
    {
        if (canShoot())
        {
            fireBullet();
        }
    }

    // Shoots out 3 rays instead of 1
    public override void shootBullet(Vector3 _bulletDir, float shootDist)
    {
        float am = 12f;

        // Straight pellet
        Ray straightRay = new Ray(spawn.position, _bulletDir * shootDist);
        // Right Angled pellet
        Ray rightRay = new Ray(spawn.position, Quaternion.Euler(0, am, 0) * straightRay.direction);
        // Left Angled pellet
        Ray leftRay = new Ray(spawn.position, Quaternion.Euler(0, -am, 0) * straightRay.direction);

        if (checkSkillUsed())
        {
            Ray skillRightRay = new Ray(spawn.position, Quaternion.Euler(0, 2 * am, 0) * straightRay.direction);
            Ray skillLeftRay = new Ray(spawn.position, Quaternion.Euler(0, -2 * am, 0) * straightRay.direction);

            checkRayCollision(skillRightRay, shootDist);
            checkRayCollision(skillLeftRay, shootDist);
        }

        checkRayCollision(straightRay, shootDist);
        checkRayCollision(rightRay, shootDist);
        checkRayCollision(leftRay, shootDist);

        if (tracer)
        {
            toggleTracerSpread();
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

    private void toggleTracerSpread()
    {
        tracer.endWidth = (checkSkillUsed() == true) ? skillEndSpread : defaultEndSpread;
    }
}
