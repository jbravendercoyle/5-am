﻿using UnityEngine;
using System.Collections;

public class WeaponAttack : MonoBehaviour {
    GameObject bullet, curWeapon;
    bool gun = false;
    float timer = 0.1f, timerReset = 0.1f;
    PlayerAnimate pa;
    SpriteContainer sc;

    float weaponChange = 0.0f;
    bool changingWeapon = false;
	
    // Use this for initialization
	void Start () {
        pa = this.GetComponent<PlayerAnimate>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            attack();
        }

        if (Input.GetMouseButton(0))
        {
            pa.resetCounter();
        }
        if (Input.GetMouseButtonUp(0))
        {
            pa.resetCounter();
        }
        //another way to drop weapon

        if (Input.GetMouseButtonDown(3) && changingWeapon == false)
        {
            dropWeapon();
        }

        if (changingWeapon == true)
        {
            weaponChange -= Time.deltaTime;
            if (weaponChange <= 0)
            {
                changingWeapon = false;
            }
        }
    }

    public void setWeapon(GameObject cur, string name, float fireRate,bool gun)
    {
        changingWeapon = true;
        curWeapon = cur;
        pa.SetNewTorso(sc.getWeaponWalk(name), sc.getWeapon(name));
        this.gun = gun;
        timerReset = fireRate;
        timer = timerReset;
    }

    public void attack()
    {
        pa.attack();
    }

    public GameObject getCur()
    {
        return curWeapon;
    }

    public void dropWeapon()
    {


        curWeapon.transform.position = this.transform.position;
        curWeapon.SetActive(true);
        setWeapon(null, "", 0.5f, false);
    }
}

