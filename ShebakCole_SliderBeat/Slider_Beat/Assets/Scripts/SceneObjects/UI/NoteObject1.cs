using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject1 : MonoBehaviour
{
    private Vector3 notePosition, notePositionGood, notePositionSustained;
    public bool canBePressed, canBeReSustained, canBeReHit;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect, sustainedEffect, sliderKnob;
    private int oneNote;


    // Start is called before the first frame update
    void Start()
    {
        canBePressed = true;
        oneNote = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(oneNote);
            if (canBePressed && !canBeReHit && canBeReSustained && oneNote == 1) 
            {
                    //Debug.Log("Initiated");
                    GameManager.instance.PerfectHit();
                    oneNote = 0;
            notePosition = transform.position + new Vector3(2, 0, 0);
            Instantiate(perfectEffect, notePosition, missEffect.transform.rotation); //Neeeeeeeeeeeed to get rid of this crap while keeping the Effect. Becomes extra notes. It repeats itself over a 2D Collider! Do a createGameObject instead?
        }  
        if (!canBePressed && !canBeReHit && !canBeReSustained && oneNote == 1)
        {
            GameManager.instance.GoodHit();
            notePositionGood = transform.position + new Vector3(2, 0, 0);
            Instantiate(goodEffect, notePositionGood, missEffect.transform.rotation);
            oneNote = 0;
        }
            if (!canBePressed && !canBeReHit && !canBeReSustained) ////ADD SUSTAINED TO GAME MANAGER VOID PUBLIC()
            {
            Debug.Log("sustained");
            GameManager.instance.SustainedHit();
            notePositionSustained = sliderKnob.transform.position;
            Instantiate(sustainedEffect, notePositionSustained, missEffect.transform.rotation);
        }
            if (!canBePressed && canBeReHit && canBeReSustained)
            {
                //Debug.Log("Miss Sustained"); //ADD MISS SUSTAINED TO GAME MANAGER VOID PUBLIC()
            }
        //Debug.Log(noteHP);
       // Debug.Log(canBePressed);
       // Debug.Log(canBeReHit);
        //Debug.Log(canBeReSustained);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Activator") && (canBePressed)) //Waiting first trigger
        {
            canBePressed = true;
            canBeReHit = false;
            canBeReSustained = true;
        }
        if (other.tag == "Blocker") //Missed first trigger
        {
            canBePressed = false;
            canBeReHit = true;
            canBeReSustained = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) //sustained keyhold
    {
        if ((other.tag == "Activator") && (other.tag == "Blocker")) //success
        {
            canBePressed = false;
            canBeReHit = false;
            canBeReSustained = false;
        }
        if (other.tag == "Activator") //success
        {
            canBePressed = false;
            canBeReHit = false;
            canBeReSustained = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator") //backup if Stay version fails. Waiting 1st trigger
        {
            canBePressed = false;
            canBeReHit = true;
            canBeReSustained = true;
        }
      if (gameObject.activeInHierarchy)
        {
            if (other.tag == "Blocker" && gameObject.activeSelf && oneNote == 1)
            {
                canBePressed = false;
                gameObject.SetActive(false);
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position = transform.position + new Vector3(0, 0, 0), missEffect.transform.rotation); //Do I want this?
            }
            else if (other.tag == "Blocker" && gameObject.activeSelf && oneNote == 0)
            {
                canBePressed = false;
                gameObject.SetActive(false);
            }
        }
    }
}
