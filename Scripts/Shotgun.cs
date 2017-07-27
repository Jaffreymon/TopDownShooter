﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    private float shotgunRPM = 70f;
    private float shotgunDamage = 143f;
    private int shotgunMaxAmmo = 7;
    private float shotgunShootDist = 6f;
    private GunType shotgunGunType = GunType.Semi;

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
            shootBullet(spawn.forward, shotgunShootDist);

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

        checkRayCollision(straightRay, shootDist);
        checkRayCollision(rightRay, shootDist);
        checkRayCollision(leftRay, shootDist);

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
            shootDist = hit.distance;
            if (hit.collider.GetComponent<Entity>())
            {
                hit.collider.GetComponent<Entity>().takeDamage(gunDamage);
            }
        }

    }
}
