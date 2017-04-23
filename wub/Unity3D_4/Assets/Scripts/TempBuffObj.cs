using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBuffObj : MonoBehaviour {
    
   public string playerTag;

    BS_Marker_Manager dmgCont;
    new Animator animation;
    public BS_Main_Health healthScript;


	// Use this for initialization
	void Start () {

        animation = GetComponent<Animator>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
           // Debug.Log("P1 Power Up Triggered");
            animation.SetTrigger("PowerUp");
            healthScript = other.gameObject.GetComponent<BS_Main_Health>();
            StartCoroutine(StopDamage());

        //    GetComponent<CapsuleCollider>().enabled = false;

        }

    }

    IEnumerator StopDamage()
    {
        healthScript = gameObject.GetComponent<BS_Main_Health>();
        healthScript.enabled = false;
       // Debug.Log("P1 Health Disabled" + healthScript._health);
        yield return new WaitForSeconds(5);
      //  Debug.Log("P1 Health Enabled" + healthScript._health);

        healthScript.enabled = true;
    }

}
