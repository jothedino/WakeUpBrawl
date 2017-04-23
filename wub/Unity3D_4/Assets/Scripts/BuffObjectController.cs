using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObjectController : MonoBehaviour
{
    // Make sure that this GameObject has a Kinematic Rigidbody on it. Otherwise, the collision with the child won't work

    // GameObject that belongs to this GameObject. Has Mesh + Collider
    private GameObject buffChild;

    private void Start()
    {
        buffChild = transform.GetChild(0).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player 1" || other.gameObject.tag == "Player 2")
        {
            StartCoroutine(DisableAndMove());
        }
    }

    IEnumerator DisableAndMove()
    {
        // Disable the child gameobject
        buffChild.SetActive(false);
        //yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(Random.Range(4, 13));

        // Move, don't re-instantiate
        MoveObject();
    }

    void MoveObject()
    {
        // Choose random position
        Vector3 position = new Vector3(Random.Range(-5.0F, 5.0F), transform.position.y, Random.Range(-5F, 5F));
        // Apply the movement
        transform.position = position;
        // Re-enable the child object
        buffChild.SetActive(true);
    }
}



