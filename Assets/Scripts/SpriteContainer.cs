using UnityEngine;
using System.Collections;

public class SpriteContainer : MonoBehaviour {
    public Sprite[] pLegs, pUnarmedWalk,pPunch,pWaltherWalk, pWaltherAttack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public Sprite[] getPlayerLegs()
    {
        return pLegs;
    } 
    public Sprite[] getPlayerUnarmedWalk()
    {
        return pUnarmedWalk;
    }
    public Sprite[] getPlayerPunch()
    {
        return pPunch;
    }

    public Sprite[] getWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Walther":
                return pWaltherAttack;
                break;
            default:
                return getPlayerPunch();
                break;
        }
    }

    public Sprite[] getWeaponWalk(string weapon)
    {
        switch (weapon)
        {
            case "Walther":
                return pWaltherWalk;
                break;
            default:
                return getPlayerUnarmedWalk();
                break;
        }
    }

}
