using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatered : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(100f, 0f, 0f) * Time.deltaTime);
    }
}
