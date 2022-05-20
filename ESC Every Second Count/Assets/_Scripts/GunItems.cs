using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItems : MonoBehaviour
{
    public static int triggerNumber = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (transform.name == "M1911Tactical")
                triggerNumber = 1;
            if (transform.name == "G36C")
                triggerNumber = 2;
            if (transform.name == "Mossberg500")
                triggerNumber = 3;
            if (transform.name == "MP5")
                triggerNumber = 4;
            if (transform.name == "CheytacM200")
                triggerNumber = 5;
            if (transform.name == "Reset")
                triggerNumber = 0;
        }
    }
}
