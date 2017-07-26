using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Gun {

    private float minigunRPM = 600f;
    private float minigunDamage = 10f;
    private int minigunMaxAmmo = 100;
    private float minigunShootDist = 20f;
    private GunType minigunGunType = GunType.Auto;

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
}
