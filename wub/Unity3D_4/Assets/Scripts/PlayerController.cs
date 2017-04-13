using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform target;
    public string horizontalAxis;
    public string verticalAxis;
    public float moveSpeed = 12f;
   // public float rotationSpeed = 15.0f;
   // public float gravity = -9.8f;
    private CharacterController cc;
   // private float ySpeed;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //perfect rotation & movement
        Vector3 NextDir = new Vector3(Input.GetAxisRaw(horizontalAxis), 0, Input.GetAxisRaw(verticalAxis));
        if (NextDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(NextDir.normalized);
        cc.Move(NextDir / 8);

        /*base for all below
        // Movement is based on the current axis position times speed.
        float deltaX = Input.GetAxisRaw(horizontalAxis); // * moveSpeed;
        float deltaZ = Input.GetAxisRaw(verticalAxis); // * moveSpeed;
        
        //should cause rotation according to direction character is going
        //snaps roation back to zero
        Vector3 movementRotation = new Vector3(deltaX, 0f, deltaZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementRotation.normalized), 0.15F);

        transform.Translate(movementRotation * moveSpeed * Time.deltaTime, Space.World);
        */
        /*
       //back to base player movement
       Vector3 movement = new Vector3(deltaX, 0, deltaZ);
       movement = Vector3.ClampMagnitude(movement, moveSpeed);

       Quaternion tmp = target.rotation;
       target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
       transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
       movement.y = ySpeed;                               
       movement *= Time.deltaTime;
       movement = target.TransformDirection(movement);
       target.rotation = tmp;
       cc.Move(movement);
        */

    }
}
