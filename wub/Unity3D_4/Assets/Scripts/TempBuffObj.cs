using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBuffObj : MonoBehaviour {
    
   public string p1Tag;
   // public string p2Tag;
    BS_Marker_Manager dmgCont;
    BS_Main_Health health;
    new Animator animation;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animator>();
	}

    void OnCollisionEnter(Collision other)
    {
        if(gameObject.tag == p1Tag)
        {
            Debug.Log("Power Up Triggered");
            animation.SetTrigger("PowerUp");

        }

 
    }

}
