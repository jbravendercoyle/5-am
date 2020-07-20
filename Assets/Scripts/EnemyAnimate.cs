using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyAnimate : MonoBehaviour {
   // public Sprite knockedDown, stabbed, bulletWound, backUp;
    public GameObject bloodPool, bloodSpurt;
    SpriteRenderer sr;
    public SpriteRenderer legs;
    public bool EnemyKnockedDown;
    float knockDownTimer = 3.0f;
    GameObject player;

    public Animator EnemyAnimCon;

    // Use this for initialization
    void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	if (EnemyKnockedDown == true)
        {
            knockDown();
        }
	} 

    public void knockDownEnemy()
    {
        EnemyKnockedDown = true;
    }

    void knockDown()
    {
        knockDownTimer -= Time.deltaTime;
        //knocked down
        EnemyAnimCon.SetBool("Down", true);
        this.GetComponent<CircleCollider2D>().enabled = false;
        sr.sortingOrder = 1;
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<ShadowCaster2D>().castsShadows = false;
        legs.enabled = false;

        if (knockDownTimer <=0)
        {
            EnemyAnimCon.SetBool("Down", false);
            legs.enabled = true;
            EnemyKnockedDown = false;          
            this.GetComponent<CircleCollider2D>().enabled = true;
            this.GetComponent<EnemyAI>().enabled = true;
            this.GetComponent<ShadowCaster2D>().castsShadows = true;          
            sr.sortingOrder = 5;
            knockDownTimer = 3.0f;
        } 
        //disable ai
    }

    public void killBullet()
    {
        //killed
        EnemyAnimCon.SetBool("Down", true);
        Instantiate(bloodPool, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 1;
        //disable ai 
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<ShadowCaster2D>().castsShadows = false;
        legs.enabled = false;
        this.gameObject.tag = "Dead";
    }

    public void killMelee()
    {
        //stabbed
        EnemyAnimCon.SetBool("Down", true);
        Instantiate(bloodPool, this.transform.position, this.transform.rotation);
        Instantiate(bloodSpurt, this.transform.position, player.transform.rotation);
        sr.sortingOrder = 1;
        //disable ai
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.GetComponent<ShadowCaster2D>().castsShadows = false;
        legs.enabled = false;
        this.gameObject.tag = "Dead";
    }

}
