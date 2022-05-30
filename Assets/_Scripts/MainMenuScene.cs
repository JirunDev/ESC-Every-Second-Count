using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0f, 5f * Time.deltaTime, 0f, Space.Self);

        Vector3 v = startPos;
        v.y += 0.5f * Mathf.Sin(Time.time * 0.5f);
        transform.position = v;
    }
}
