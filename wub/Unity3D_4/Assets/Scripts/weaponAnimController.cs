using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnimController : MonoBehaviour
{
    public string dodgeButton;
    public string attackButton;
    new  Animator animation;

    private void Start()
    {
        
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown(attackButton))
        {
            //Debug.Log("ATTACK!!");
            animation.SetTrigger("Attack");

        }
        if (Input.GetButtonDown(dodgeButton))
        {
            // Debug.Log("DODGED");
            animation.SetTrigger("Dodge");
        }

    }
}
    
    
