using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour {

    private GameObject raidBossSetup;
    private GameObject worldBossSetup;

    private Text buttonText;

    void Awake()
    {
        raidBossSetup = GameObject.Find("Boss Setup");
        worldBossSetup = GameObject.Find("World Boss Setup");
        worldBossSetup.SetActive(false);
        buttonText = GetComponentInChildren<Text>();
    }

    public void SwitchButton()
    {
        if (raidBossSetup.activeSelf)
        {
            raidBossSetup.SetActive(false);
            worldBossSetup.SetActive(true);
            buttonText.text = "Switch to Raid";
        }
        else
        {
            raidBossSetup.SetActive(true);
            worldBossSetup.SetActive(false);
            buttonText.text = "Switch to WB";
        }

    }
}
