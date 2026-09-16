using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour { 

    private SpriteRenderer theSR;
    public Sprite defaultSprite;
    public Sprite pressedSprite;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedSprite;
        }
        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultSprite;
        }
    }
}
