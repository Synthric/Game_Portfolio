using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public float scale, rotation, rotationMath;
    public Slider slider;


    void Start()
    {
        //Cache the Slider variables
        slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    void OnEnable()
    {
        //Subscribe to the Slider Click event
        slider.onValueChanged.AddListener(delegate { sliderCallBack(slider.value); });
    }

    //Will be called when Slider changes
    public void sliderCallBack(float value)
    {
        Debug.Log("Slider Changed: " + value);
        scale = slider.value;
        rotation = slider.value;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        rotationMath = (rotation * 8)+2;
        transform.localRotation = new Quaternion(0, 0, rotationMath, 90);
    }

}
