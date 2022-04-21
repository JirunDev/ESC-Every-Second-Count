using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotategreen : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }
}
