using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItems : MonoBehaviour
{
    public static int triggerNumber = 0;
    public static int countdownValue = 0;
    private bool checkStartTimer = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (transform.name == "M1911Tactical(Clone)")
                triggerNumber = 1;
            if (transform.name == "G36C(Clone)")
                triggerNumber = 2;
            if (transform.name == "Mossberg500(Clone)")
                triggerNumber = 3;
            if (transform.name == "MP5(Clone)")
                triggerNumber = 4;
            if (transform.name == "CheytacM200(Clone)")
                triggerNumber = 5;
            if (transform.name == "Reset")
                triggerNumber = 0;

            if (triggerNumber != 0 && !checkStartTimer)
            {
                checkStartTimer = true;
                countdownValue = Random.Range(25,76);
            }
        }
    }

    private void FixedUpdate()
    {
        if (GunTimer.Duration != 0)
        {
            checkStartTimer = false;
            countdownValue = 0;
        }
    }
}
