using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderDongleLeft : MonoBehaviour
{

    public Slider slider;    


    private float angleSliderNumber, scaleSliderNumber;


    void update()
    {
        angleSliderNumber = slider.value * 10f;
        this.transform.rotation = Quaternion.Euler(0, angleSliderNumber, 0);

        scaleSliderNumber = slider.value;
        Vector3 scale = new Vector3(scaleSliderNumber, scaleSliderNumber, scaleSliderNumber);
        this.transform.localScale = scale;
    }
}