using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                //GameManager.instance.NoteHit();
                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("OK");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position = transform.position + new Vector3(0, 1, 0), missEffect.transform.rotation);
                } else if(Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position = transform.position + new Vector3(0, 1, 0), missEffect.transform.rotation);
                } else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position = transform.position + new Vector3(0, 1, 0), missEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (gameObject.activeInHierarchy)
        {
            if (other.tag == "Activator" && gameObject.activeSelf)
            {
                canBePressed = false;
                gameObject.SetActive(false);
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position = transform.position + new Vector3(0, 1, 0), missEffect.transform.rotation);
            }
        }
    }
}
