using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oof : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    void OnTriggerEnter(Collider other)
    {
        //player.transform.position = respawnPoint.transform.position;
        player.GetComponent<Damage>().Respawn();
    }
}
