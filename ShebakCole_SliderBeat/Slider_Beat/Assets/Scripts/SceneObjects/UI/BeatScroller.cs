using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BeatScroller : MonoBehaviour
{ 
    public float beatTempo;

    public bool hasStarted;

    private float songDelay;
    private Transform targetA, targetS, targetD, targetF, targetMO, targetML;
    private Rigidbody2D a, s, d, f, mO, mL;

void Start()
{
        beatTempo = beatTempo / 60f;
        targetA = GameObject.FindGameObjectWithTag("NoteA").transform;
        targetS = GameObject.FindGameObjectWithTag("NoteS").transform;
        targetD = GameObject.FindGameObjectWithTag("NoteD").transform;
        targetF = GameObject.FindGameObjectWithTag("NoteF").transform;
        targetMO = GameObject.FindGameObjectWithTag("NoteMouse 0").transform;
        targetML = GameObject.FindGameObjectWithTag("NoteMouse 1").transform;
        a = GetComponent<Rigidbody2D>();
        s = GetComponent<Rigidbody2D>();
        d = GetComponent<Rigidbody2D>();
        f = GetComponent<Rigidbody2D>();
        mO = GetComponent<Rigidbody2D>();
        mL = GetComponent<Rigidbody2D>();
    }

void FixedUpdate () {
        songDelay += Time.deltaTime;
        if (!hasStarted)
        {
            if (Input.anyKeyDown && songDelay >=3f)
            {
                hasStarted = true;
            }
        }
        else
        {
            if (targetA)
            {
                Vector2 direction = (Vector2)targetA.position - a.position;
                direction.Normalize();
            }
            if (targetS)
            {
                Vector2 direction = (Vector2)targetS.position - s.position;
                direction.Normalize();
            }
            if (targetD)
            {
                Vector2 direction = (Vector2)targetD.position - d.position;
                direction.Normalize();
            }
            if (targetF)
            {
                Vector2 direction = (Vector2)targetF.position - f.position;
                direction.Normalize();
            }
            if (targetMO)
            {
                Vector2 direction = (Vector2)targetMO.position - mO.position;
                direction.Normalize();
            }
            if (targetML)
            {
                Vector2 direction = (Vector2)targetML.position - mL.position;
                direction.Normalize();
            }

            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
      }
    }

