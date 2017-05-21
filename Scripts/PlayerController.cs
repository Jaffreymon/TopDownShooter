﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {

    // Player Handling
    [SerializeField]
    private float rotationSpeed = 500f;
    [SerializeField]
    private float walkSpeed = 3.5f;
    [SerializeField]
    private float runSpeed = 6f;
    private float acceleration = 5f;

    // System variables
    private Quaternion targetRotation;
    private Vector3 currVelocityModify;
    private int gunSlotNum = 0;
    private bool flashlightOn= false;

    // Game Components
    public Transform handHold;
    public Gun[] guns;
    private Gun currGun;
    private CharacterController playerController;
    [SerializeField]
    private GameObject flashlight;
    private Camera cam;
    private GUI_HUD gui;

    // Use this for initialization
    void Start () {
        playerController = GetComponent<CharacterController>();
        cam = Camera.main;
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI_HUD>();
        Cursor.visible = false;

        EquipGun(gunSlotNum);
	}
	
	// Update is called once per frame. Player controls go here
	void Update () {
        ControlMouse();

        // Gun input if there exists gun
        if (currGun)
        {
            if (Input.GetButtonDown("Shoot"))
            {
                currGun.Shoot();
            }
            else if (Input.GetButton("Shoot"))
            {
                currGun.ShootAuto();
            }

            if(Input.GetButtonDown("Reload"))
            {
                if(currGun.Reload())
                {
                    currGun.finishReload();
                }
            }
        }

        // Detects gun switch via numpad
        for(int idx = 0; idx < guns.Length; idx++)
        {
            if(Input.GetKeyDown(idx+1 + "") || Input.GetKeyDown("[" + (idx+1) + "]") && !currGun.isReloading())
            {
                gunSlotNum = idx;
                EquipGun(idx);
                break;
            }
        }

        //Detects gun switch via mouse scroll
        float mouseScrollMove = Input.GetAxis("MouseScroll");
        if ( mouseScrollMove != 0 && !currGun.isReloading())
        {
            if(mouseScrollMove > 0 && gunSlotNum != (guns.Length - 1)) {
                gunSlotNum = Mathf.Clamp(gunSlotNum + 1, 0, guns.Length - 1);
                EquipGun(gunSlotNum);
            }
            else if(mouseScrollMove < 0 && gunSlotNum != 0)
            {
                gunSlotNum = Mathf.Clamp(gunSlotNum - 1, 0, guns.Length - 1);
                EquipGun(gunSlotNum);
            }
        }

        //Detects flashlight on
        if(Input.GetButtonDown("Flashlight"))
        {
            flashlightOn = !flashlightOn;
            flashlight.SetActive(flashlightOn);
        }
	}

    void EquipGun(int id)
    {
        if(currGun) { Destroy(currGun.gameObject);  }
        currGun = Instantiate(guns[id], handHold.position, handHold.rotation) as Gun;
        currGun.transform.parent = handHold;
        currGun.gui = gui;

        gui.SetCurrGunName(currGun.name.Replace("(Clone)", "").Trim());
    }

    // Player looks where their mouse is over
    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint( new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currVelocityModify = Vector3.MoveTowards(currVelocityModify, input, acceleration * Time.deltaTime);
        Vector3 motion = currVelocityModify;

        // Corrects player movement speed at diagnols
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1f;
        // Increases player movement based on running or walking
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        playerController.Move(motion * Time.deltaTime);
    } 

    // Player looks where they are moving
    void ControlWASD()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        currVelocityModify = Vector3.MoveTowards(currVelocityModify, input, acceleration * Time.deltaTime);
        Vector3 motion = currVelocityModify;
        // Corrects player movement speed at diagnols
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1f;
        // Increases player movement based on running or walking
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        playerController.Move(motion * Time.deltaTime);
    }
}
