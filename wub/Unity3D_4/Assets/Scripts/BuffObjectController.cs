using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObjectController : MonoBehaviour
{

    //Spawn this object
    public GameObject spawnObject;

    void SpawnObject()
    {
            Vector3 position = new Vector3(Random.Range(-5.0F, 5.0F), 1, Random.Range(-5F, 5F));
            Instantiate(spawnObject, position, Quaternion.identity);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player 1" || other.gameObject.tag == "Player 2")
        {
            StartCoroutine(SpawnDelay());
            //Destroy(gameObject);
         //   SpawnObject();
          
        }
    }
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(3f);
        SpawnObject();
    }

}



