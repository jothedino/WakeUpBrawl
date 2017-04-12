using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform target;
    public string horizontalAxis;
    public string verticalAxis;
    public float moveSpeed = 12f;
    public float rotSpeed = 15.0f;
    public float gravity = -9.8f;
    private CharacterController cc;
    private float ySpeed;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement is based on the current axis position times speed.
        float deltaX = Input.GetAxis(horizontalAxis) * moveSpeed;
        float deltaZ = Input.GetAxis(verticalAxis) * moveSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);

        Quaternion tmp = target.rotation;
        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotSpeed * Time.deltaTime);
        movement.y = ySpeed;                               
        movement *= Time.deltaTime;
        movement = target.TransformDirection(movement);
        target.rotation = tmp;

        cc.Move(movement);


    }
}
