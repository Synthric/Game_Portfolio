using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorByTwo : MonoBehaviour
{
    public Vector3 thisNote;
    public float multiplierY = 1f;

    // Start is called before the first frame update
    void Start()
    {
        thisNote = transform.position;
        thisNote.y *= multiplierY;
        transform.position = thisNote;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
