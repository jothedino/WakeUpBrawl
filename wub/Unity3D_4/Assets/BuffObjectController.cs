using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObjectController : MonoBehaviour {

    public GameObject preFab;
 
    void SpawnBuff()
    {
        Vector3 position = new Vector3(Random.Range(-10.0F, 10.0F), 1, Random.Range(-0F, 0F));
        Instantiate(preFab, position, Quaternion.identity);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player 1")
        {
            SpawnBuff();
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Player 2")
        {
            SpawnBuff();
            Destroy(this.gameObject);
        }
        
    }
}
