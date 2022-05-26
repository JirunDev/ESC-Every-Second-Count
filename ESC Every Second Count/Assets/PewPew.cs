using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    public GameObject bullet; 
    public float speed = 100f;
    private bool PlayerInZone;
    

    private void Start()
    {
        PlayerInZone = false;                   //player not in zone       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")     //if player in zone
        {
            
            PlayerInZone = true;
        }
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F))           //if in zone and press F key
        {
            GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            
            instBulletRigidbody.AddForce(Vector3.forward * speed);
        }
    }

    private void OnTriggerExit(Collider other)     //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            
        }
    }
}
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "button")
        {
            GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();

            instBulletRigidbody.AddForce(Vector3.forward * speed);
        }
    }*/

/*public GameObject bullet;
    public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject)
        {
            GameObject instBullet=Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instBulletRigidbody = instBulletRigidbody.GetComponent<Rigidbody>();
            instBulletRigidbody.AddForce(Vector3.forward * speed * Time.deltaTime);
        }
    }*/