using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeFunction : MonoBehaviour {

    public Rigidbody rb;
    public float dodgeSpeed;
    public Vector3 dodge = new Vector3(5, 5, 5);
    public float buttonCoolDown = 0.5f;
    public float lastTapTime = 0;
    public string dodgeButton;
    private Vector3 velocity;

    void Start()
    {
        GameObject player = GameObject.Find("PlayerController3PF");
        rb = GetComponent<Rigidbody>();
   
        lastTapTime = 0;

    }

    void Update()
    {

        if (Input.GetButtonDown(dodgeButton))
        {
            if ((Time.time - lastTapTime) < buttonCoolDown)
            {
                DodgeRight();
            }

            lastTapTime = Time.time;
        }


    }

    void DodgeRight()
    {
        //rb.AddForce(dodge * dodgeSpeed);

        //.velocity += Vector3.right * dodgeSpeed;

       velocity = Vector3.right * dodgeSpeed;
    }

}