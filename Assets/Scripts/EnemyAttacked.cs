using UnityEngine;
using System.Collections;

public class EnemyAttacked : MonoBehaviour {
    public Sprite knockedDown, stabbed, bulletWound, backUp;
    public GameObject bloodPool, bloodSpurt;
    SpriteRenderer sr;
    public bool EnemyKnockedDown;
    float knockDownTimer = 3.0f;
    GameObject player; 
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
        sr.sprite = knockedDown;
        this.GetComponent<CircleCollider2D>().enabled = false;
        sr.sortingOrder = 1;
        this.GetComponent<EnemyAI>().enabled = false;

        if (knockDownTimer <=0)
        {
            EnemyKnockedDown = false;
            sr.sprite = backUp;
            this.GetComponent<CircleCollider2D>().enabled = true;
            this.GetComponent<EnemyAI>().enabled = false;
            sr.sortingOrder = 5;
            knockDownTimer = 3.0f;
        } 
        //disable ai
    }

    public void killBullet()
    {
        sr.sprite = bulletWound;
        Instantiate(bloodPool, this.transform.position, this.transform.rotation);
        sr.sortingOrder = 1;
        //disable ai 
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.tag = "Dead";
    }

    public void killMelee()
    {
        sr.sprite = stabbed;
        Instantiate(bloodPool, this.transform.position, this.transform.rotation);
        Instantiate(bloodSpurt, this.transform.position, player.transform.rotation);
        sr.sortingOrder = 1;
        //disable ai
        this.GetComponent<EnemyAI>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        this.gameObject.tag = "Dead";
    }

}
