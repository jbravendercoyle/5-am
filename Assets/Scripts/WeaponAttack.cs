﻿using UnityEngine;
using System.Collections;

public class WeaponAttack : MonoBehaviour {
    public GameObject oneHandSpawn, twoHandSpawn, bullet;
    public GameObject curWeapon;
    public GameObject muzzleFlash;
    bool gun = false;
    float timer = 0.1f, timerReset = 0.1f;
    PlayerAnimate pa;
    SpriteContainer sc;

    float weaponChange = 0.5f;
    bool changingWeapon = false;
    bool oneHanded = false;

    //public EnemyAttacked ea;

    // Use this for initialization
    void Start() {
        pa = this.GetComponent<PlayerAnimate>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            attack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            pa.resetCounter();
        }
        if (Input.GetMouseButtonUp(0))
        {
            pa.resetCounter();
        }
        //another way to drop weapon

        if (Input.GetKeyDown(KeyCode.LeftShift) && !changingWeapon)
        {
            dropWeapon();
        }

        if (changingWeapon)
        {
            weaponChange -= Time.deltaTime;
            if (weaponChange <= 0)
            {
                changingWeapon = false;
            }
        }
    }

    public void setWeapon(GameObject cur, string name, float fireRate, bool gun, bool oneHanded)
    {
        changingWeapon = true;
        curWeapon = cur;
        pa.SetNewTorso(sc.getWeaponWalk(name), sc.getWeapon(name));
        timerReset = fireRate;
        timer = timerReset;
        this.gun = gun;
        this.oneHanded = oneHanded;
    }

    public void attack()
    {
        pa.attack();
        if (gun == true)
        {
            pa.attack();
            Bullet bl = bullet.GetComponent<Bullet> ();
            Vector3 dir;
            dir.x = Vector2.right.x;
            dir.y = Vector2.right.y;
            dir.z = 0;
            bl.setVals(dir, "Player");
            if(oneHanded == true)
            {
                Instantiate(bullet, oneHandSpawn.transform.position, this.transform.rotation);
                //Instantiate(muzzleFlash, oneHandSpawn.transform.position, this.transform.rotation);
            }
            else
            {
                Instantiate(bullet, twoHandSpawn.transform.position, this.transform.rotation);
               // Instantiate(muzzleFlash, oneHandSpawn.transform.position, this.transform.rotation);
            }
            timer = timerReset;



        } else
        {
            //melee attack 
            int layerMask = 1<<9;
            layerMask = ~layerMask;
            pa.attack();

            RaycastHit2D ray = Physics2D.Raycast (new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y), 0.5f, layerMask);
            Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(transform.right.x, transform.right.y), Color.green);

            if (curWeapon == null && ray.collider.gameObject.tag == "Enemy")
            {
                
                EnemyAnimate ea = ray.collider.gameObject.GetComponent<EnemyAnimate>();
                ea.knockDownEnemy();

            } else if (ray.collider != null)
            {
                if (ray.collider.gameObject.tag == "Enemy") {
                    EnemyAnimate ea = ray.collider.GetComponent<EnemyAnimate>();
                    ea.killMelee();
                }
            }

        }
    }

    public GameObject getCur()
    {
        return curWeapon;
    }

    public void dropWeapon()
    {
        if (curWeapon == null)
        {

        } else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
            curWeapon.AddComponent<ThrowWeapon>();
            Vector3 dir; 
            dir.x = mousePos.x - this.transform.position.x;
            dir.y = mousePos.y - this.transform.position.y;
            dir.z = 0;
            curWeapon.GetComponent<Rigidbody2D>().isKinematic = false;
            curWeapon.GetComponent<ThrowWeapon>().setDirection(dir);
            curWeapon.transform.position = oneHandSpawn.transform.position;
            curWeapon.transform.eulerAngles = this.transform.eulerAngles;
            curWeapon.SetActive(true);
            setWeapon (null, "", 0.5f, false, false);
            pa.resetSprites ();
        }
}

}

