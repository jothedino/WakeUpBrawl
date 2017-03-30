using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombatControl : MonoBehaviour {

    GameObject player;
    GameObject Weapon;
    float swingrate = .5f;

    //private MeleeTrigger meleeTrig;

    // Use this for initialization
    void Start()
    {
        // StartCoroutine(MeleeSwing());
        //Weapon.SetActive(false);
    //  meleeTrig =  GetComponent<MeleeTrigger>();

    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1 P1"))
        {
            StartCoroutine(MeleeSwing());
            GetComponent<PlayerHealth>();
            GetComponent<MeleeTrigger>();

        }
    }

    // Update is called once per frame
   IEnumerator MeleeSwing()
    {

           // if (Input.GetButtonUp("Fire1 P1"))
           // {
                //animation trigger
                //player.animation.CrossFade("");

                //Weapon.SetActive(true);

                //animation waiting
                //yield return new WaitForSeconds(player.animation[""].length);
                
                yield return new WaitForSeconds(swingrate);
                Debug.Log("I Hit!");
                //Weapon.SetActive(false);
           // }
           // else
           // {
             //   yield return 0;
               

           // }
        
    }
}
