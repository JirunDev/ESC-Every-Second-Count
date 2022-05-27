using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public GameObject checkpoint;
    Vector3 spawnPoint;

   
    void Start()
    {
        spawnPoint = gameObject.transform.position; 
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Checkpoint")
        {
            spawnPoint = checkpoint.transform.position;
            
        }
    }
}
