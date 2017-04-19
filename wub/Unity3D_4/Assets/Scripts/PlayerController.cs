using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform target;
    public string horizontalAxis;
    public string verticalAxis;
    public float moveSpeed = 12f;
    Animator anim;
    private CharacterController cc;
    // private float ySpeed;
    private float f;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //perfect rotation & movement
        Vector3 NextDir = new Vector3(Input.GetAxisRaw(horizontalAxis), 0, Input.GetAxisRaw(verticalAxis));
        if (NextDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(NextDir.normalized);
        cc.Move(NextDir / 8);
        f = 0;
        if (Input.GetAxis(verticalAxis) != 0 || Input.GetAxis(horizontalAxis) != 0)
        {
            if( Mathf.Abs(Input.GetAxis(verticalAxis)) > .5f || Mathf.Abs(Input.GetAxis(horizontalAxis)) >.5f)
            {
                f = 1;
            }
            else
            {
                f = .5f;
            }
            
        }
        anim.SetFloat("forward", f);
      //  anim.SetFloat("turn", t);

     //   float f = Input.GetAxis(horizontalAxis);
        //  float t = Input.GetAxis(verticalAxis);




    }
}
