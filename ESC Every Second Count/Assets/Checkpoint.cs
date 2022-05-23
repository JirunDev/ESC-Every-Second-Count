using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Damage oof;

    void Start()
    {
        oof = FindObjectOfType<Damage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            oof.SetSpawnPoint(transform.position);
        }

    }
}
