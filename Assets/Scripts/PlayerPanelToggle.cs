using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPanelToggle : MonoBehaviour {

    public Toggle[] toggleGroup;
    public GameObject[] playerPanels;
    private int playerNumber = 5;
	// Use this for initialization
	void Awake () {
        playerNumber = 5;
    }
    void Start()
    {
        playerNumber = 5;
        Debug.Log(playerNumber);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void OnToggleChangePanelActiveState()
    {
        
        for (int i = 0; i < toggleGroup.Length; i++)
        {
            if (toggleGroup[i].isOn)
            {
                ActivatePanels(i);
                playerNumber = i + 1;
                break;
            }
        }
    }
    private void ActivatePanels(int index)
    {
        for (int i  = 0; i < playerPanels.Length; i++)
        {
            if (i <= index)
            {
                playerPanels[i].SetActive(true);
            }
            else
            {
                playerPanels[i].SetActive(false);
            }
        }
    }

    public int GetPlayerNumber()
    {
        Debug.Log("returning value " + playerNumber.ToString());
        return playerNumber;
    }
}
