using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{

   // public string p1Tag;
     public string p2Tag;
    BS_Marker_Manager dmgCont;
    BS_Main_Health health;
    new Animator animation;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == p2Tag)
        {
            Debug.Log("Attack Triggered");
            animation.SetTrigger("Attack");

        }
    }
}
