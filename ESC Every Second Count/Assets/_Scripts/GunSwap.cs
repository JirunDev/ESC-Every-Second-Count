using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSwap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;
    public TextMeshProUGUI reloadPromptText;
    public TextMeshProUGUI bulletAmountText;
    public TextMeshProUGUI magazineAmountText;

    [Header("Triggers")]
    [SerializeField] private int[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;
    private static int triggerNumber;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null) keys = new int[weapons.Length];
    }

    private void FixedUpdate()
    {
        if (triggerNumber != 0)
        {
            reloadPromptText.enabled = true;
            bulletAmountText.enabled = true;
            magazineAmountText.enabled = true;
        }
        else
        {
            reloadPromptText.enabled = false;
            bulletAmountText.enabled = false;
            magazineAmountText.enabled = false;
        }

        int previousSelectedWeapon = selectedWeapon;

        if (GunTimer.Duration > 0)
        {
            triggerNumber = GunItems.triggerNumber;
        }
        else
        {
            triggerNumber = 0;
        }

        if(timeSinceLastSwitch >= switchTime)
        {
            selectedWeapon = triggerNumber;
        }

        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        timeSinceLastSwitch = 0f;
    }
}
