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

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == p1Tag)
        {
            Debug.Log("Power Up Triggered");
            animation.Play("PowerUp");

        }

 
    }



}
