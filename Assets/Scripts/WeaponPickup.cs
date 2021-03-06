﻿using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
    public string name;
    public float fireRate;
    WeaponAttack wa;
    public bool gun, onehanded;

	// Use this for initialization
	void Start () {
        wa = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponAttack>();
	}
	
	// Update is called once per frame
	void Update () {
	
	} 

    void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("Collision");
        if (coll.gameObject.tag == "Player" && Input.GetKey(KeyCode.LeftShift)) {

            Debug.Log("Player picked up: " + name);
            if (wa.getCur () !=null)
            {
                wa.dropWeapon();
            }
            wa.setWeapon (this.gameObject,name,fireRate, gun,onehanded);
            //Destroy game object
            this.gameObject.SetActive(false);
        }
    }
}
