using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGun : MonoBehaviour
{
    public GameObject[] gun;
    private int objCount1;
    private int objCount2;
    private int objCount3;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;

    void Start()
    {
        objCount1 = Random.Range(0, 4);
        objCount2 = Random.Range(0, 4);
        objCount3 = Random.Range(0, 4);

        while (objCount1==objCount2 || objCount1 == objCount3 || objCount3 == objCount2)
        {
            objCount1 = Random.Range(0, 4);
            objCount2 = Random.Range(0, 4);
            objCount3 = Random.Range(0, 4);
        }

        Instantiate(gun[objCount1], pos1.position, Quaternion.identity);
        Instantiate(gun[objCount2], pos2.position, Quaternion.identity);
        Instantiate(gun[objCount3], pos3.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
