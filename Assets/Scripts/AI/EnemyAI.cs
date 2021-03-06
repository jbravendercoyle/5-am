﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    GameObject player;
    public bool patrol = true, guard = false, clockwise = false;
    public bool moving = true;
    public bool pursuingPlayer = false, goingToLastLoc = false;
    Vector3 target;
    Rigidbody2D rid;
    public Vector3 playerLastPos;
    RaycastHit2D hit;
    float speed = 3.0f;
    int layerMask = 1 << 8;

    private SimplePF2D.Path path;
    private Vector3 nextPoint;
    public bool isStationary = true;

    public Animator EnemyAnimCon;
    public Animator EnemyLegsAnimCon;

    public GameObject[] navPoints;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLastPos = this.transform.position;

        rid = this.GetComponent<Rigidbody2D>();
        layerMask = ~layerMask;

        //Create our new paths using the simplepathfinding 2d object attached to a grid gameobject
        path = new SimplePF2D.Path(GameObject.Find("Grid").GetComponent<SimplePathFinding2D>());
        nextPoint = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        movement();
        EnemyAnimCon.SetBool("Stationary", isStationary);
        EnemyLegsAnimCon.SetBool("Stationary", isStationary);


        Patrol();
        //playerDetect();
    }

    void movement()
    {
            if (path.IsGenerated())
            {
                if (isStationary)
                {
                    //Get the next point in the path as world position. returns true when successful
                    if(path.GetNextPoint(ref nextPoint))
                    {
                        //face direction
                        Vector3 difference = nextPoint - transform.position;
                        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
                        
                        //Move
                        rid.velocity = nextPoint - transform.position;
                        rid.velocity = rid.velocity.normalized;
                        rid.velocity *= speed;
                        isStationary = false;
                    }
                    else //When GetNextPoint fails it means we have reached the end of the path
                    {
                        rid.velocity = Vector3.zero;
                        isStationary = true;
                    }
                }
                else
                {                
                Vector3 delta = nextPoint - transform.position;
                    if (delta.magnitude <= 0.2f)
                    {
                        rid.velocity = Vector3.zero;
                        isStationary = true;
                    }
                }
            }
            else
            {     
            rid.velocity = Vector3.zero;
            isStationary = true;
            }
        }

    public void Patrol()
    {
        if (patrol == true)
        {
            //for (int i = 0; i < navPoints.Length; i++) {
                path.CreatePath(this.transform.position, navPoints[0].transform.position);
                    //}
        }
    }

    public void playerDetect()
    {
        Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
        //vision arc
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        Vector3 dir = player.transform.position - transform.position;
        hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y), dist, layerMask);
        Debug.DrawRay(transform.position, dir, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player" && pos.x > 1.2f && Vector3.Distance(this.transform.position, player.transform.position) < 9)
            {
                Debug.Log("found player");
                path.CreatePath(this.transform.position, player.transform.position);
                //playerLastPos = player.transform.position;
                patrol = false;
                pursuingPlayer = true;

            }
            else
            {
                if (pursuingPlayer == true)
                {
                    goingToLastLoc = true;
                    pursuingPlayer = false;
                }
                //pursuingPlayer = false;
            }
        }
    }

    //void movement()
    //{
    //    float dist = Vector3.Distance(player.transform.position, this.transform.position);
    //    Vector3 dir = player.transform.position - transform.position;
    //    hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y), dist, layerMask);
    //    Debug.DrawRay(transform.position, dir, Color.red);

    //    Vector3 fwt = this.transform.TransformDirection(Vector3.right);

    //    RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 1.0f, layerMask);

    //     Debug.DrawRay(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), Color.cyan);

    //     if (moving == true)
    //    {
    //        transform.Translate(Vector3.right * speed * Time.deltaTime);

    //    }

    //    if (patrol == true)
    //    {
    //Debug.Log("Patrolling normally");
    //        speed = 2.0f;
    //
    //        if (hit2.collider != null)
    //        {
    //Debug.LogError(hit.Collider.tag);
    //           if (hit2.collider.gameObject.tag == "Wall")
    //          {
    //Quaternion rot = this.transform.rotation;

    //            if (clockwise == false)
    //             {
    //                 transform.Rotate(0, 0, 90);
    //             }
    //             else
    //            {
    //               transform.Rotate(0, 0, -90);
    //            }
    //       }
    //     }

    // }


    //if (pursuingPlayer == true)
    // {
    //transform.Translate(Vector3.right * speed * Time.deltaTime);
    //Debug.Log("Pursuing Player");
    //    speed = 3.5f;
    //    rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);

    //    if (hit.collider.gameObject.tag == "Player")
    //    {
    //        playerLastPos = player.transform.position;
    //    }
    // }

    // if (goingToLastLoc == true)
    //  {
    //     Debug.Log("Checking last known player location");
    //    speed = 3.0f;
    //transform.Translate(Vector3.right * 4 * Time.deltaTime);
    //   rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
    //   if (Vector3.Distance(this.transform.position, playerLastPos) < 1.5f) {
    //didn't find player, return to patrol;
    //       patrol = true;
    //     goingToLastLoc = false;
    //   }
    // }
    // }



}

