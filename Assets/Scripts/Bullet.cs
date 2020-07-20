using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Vector3 direction;
    string creator;
    EnemyAnimate attacked;
    public GameObject bloodImpact, wallImpact;
    // Use this for initialization
    float timer = 10.0f;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * 17 * Time.deltaTime);

        timer -= Time.deltaTime; 
        if(timer<=0)
        {
            Destroy(this.gameObject);
        }
	}

    public void setVals(Vector3 dir, string name)
    {
        direction = dir;
        creator = name;
    } 

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            attacked = col.gameObject.GetComponent<EnemyAnimate>();
            attacked.killBullet();
            Instantiate(bloodImpact, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        } else
        {
            Instantiate(wallImpact, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
