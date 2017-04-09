using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnimController : MonoBehaviour
{

    public string attackButton;
    new  Animator animation;
  //  public bool animation_bool;

    private void Start()
    {
        
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonUp(attackButton))
        {
         //   animation_bool = true;
            //animation.Play("testAttack");
            animation.SetTrigger("Attack");

            //animation.SetBool();
        }

       // if (animation_bool == true)
      //  {
          //  animation.Play("testAttack");
          //  animation_bool = false;
      //  }
        
    }
}
    
    
