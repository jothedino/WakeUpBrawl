using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeController : MonoBehaviour {

    public string dodgeButton;
    new Animator animation;

	// Use this for initialization
	void Start () {

        animation = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown(dodgeButton))
        {
            Debug.Log("DODGED");
            animation.SetTrigger("Dodge");
        }
		
	}
}
