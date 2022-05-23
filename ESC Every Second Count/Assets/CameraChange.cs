using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam;
    public GameObject FirstCam;
    public int CamMode;

    
    void Update()
    {
       if(Input.GetButtonDown("Camera"))
        {
            if(CamMode==1)
            {
                CamMode=0;
            }
            else
            {
                CamMode+=1;
            }
            StartCoroutine(CamChange());
        }
    }
    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if(CamMode==0)
        {
            FirstCam.SetActive(true);
            ThirdCam.SetActive(false);
            
        }
        if(CamMode==1)
        {
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
        } 
           
    }
}
