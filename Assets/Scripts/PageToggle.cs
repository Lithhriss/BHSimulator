using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageToggle : MonoBehaviour {

    public GameObject PageOne;
    public GameObject PageTwo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NextPage()
    {
        if (PageOne.activeSelf)
        {
            PageOne.SetActive(false);
            PageTwo.SetActive(true);
        }
        else
        {
            PageOne.SetActive(true);
            PageTwo.SetActive(false);
        }
    }
}