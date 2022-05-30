using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersonalBest : MonoBehaviour
{
    public TextMeshProUGUI cityPBT, villagePBT, spacePBT;

    private int pbCity, pbVillage, pbSpace;

    void Start()
    {
        //RecoverSavedValue
        pbCity = PlayerPrefs.GetInt("pbcity", 0);
        pbVillage = PlayerPrefs.GetInt("pbvillage", 0);
        pbSpace = PlayerPrefs.GetInt("pbspace", 0);
    }

    // Update is called once per frame
    void Update()
    {
        cityPBT.text = $"Personal Best : {pbCity}";
        villagePBT.text = $"Personal Best : {pbVillage}";
        spacePBT.text = $"Personal Best : {pbSpace}";
    }
}
