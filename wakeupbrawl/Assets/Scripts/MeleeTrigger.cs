using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTrigger : MonoBehaviour {

    GameObject player;

    private float damage = 30.0f;

	// Use this for initialization
	void Start () {

        //Physics.IgnoreCollision(collider, player.collider);
        GetComponent<Collider>();
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {

        if (other.GetComponent<PlayerHealth>())
        {
            Debug.Log("tookDamage");
            other.gameObject.SendMessageUpwards("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false); 
        }
	}
}
