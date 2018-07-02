using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitProcessorCountSlider : MonoBehaviour {

    private Slider slider;
    // Use this for initialization
    void Awake () {
        slider = GetComponentInChildren<Slider>();
        slider.minValue = 1;
        slider.maxValue = Environment.ProcessorCount;
        slider.wholeNumbers = true;
	}
    
    private void Start()
    { 
        transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void OnSlideChangeInputFieldText()
    {
        InputField field = GetComponentInChildren<InputField>();
        field.text = slider.value.ToString();
    }

}
