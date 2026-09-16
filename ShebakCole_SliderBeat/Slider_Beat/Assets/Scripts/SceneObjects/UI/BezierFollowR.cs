using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollowR : MonoBehaviour
{
    public Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;

    public float curveSpeed = 0.5f;
    //public float speed = 0f;
    private int _direction = 1;
    private float updated;
    private bool _isObjectSelected;
    private Vector3 _mouseLastPosition;
    private Vector3 _mouseStartPosition;
    private float _journeyLength;
    private Vector3 _offsetPos;

    private float _currentTime = 0;

    private void Start()
    {
        _journeyLength = Vector3.Distance(startPoint.position,
                                            endPoint.position);

        UpdateJourney(0);
    }

    private void WhileDragging()
    {
        if (!_isObjectSelected)
        {
            return;
        }
        var currentPosition = Input.mousePosition;


        // You might want to use the curveSpeed here as well
        // like kind of sensitivity
        var distCovered = ((Input.GetAxis("Mouse X")) * curveSpeed / _journeyLength) + updated; //Problem: +updated to offsetPos.x. mainly offsetPosX being added profusely SOLVED
        updated = distCovered;
        if (updated >= 1f)
        {
            updated = 1f;
        }
        if (updated < 0f)
        {
            updated = 0;
        }
        UpdateJourney(updated);
    }

    private void UpdateJourney(float time)
    {
        if (time < 0)
            time = 0;
        else if (time > 1)
            time = 1;

        _currentTime = time;

        transform.position =
            QuadraticCurve(startPoint.position,
                            middlePoint.position,
                            endPoint.position,
                            _currentTime);
    }

    private void Update()
    {
        if (_isObjectSelected)
        {
            // use this to detect mouse up instead
            if (Input.GetMouseButtonUp(1))
            {
                _isObjectSelected = false;
            }
        }
        if ((!_isObjectSelected) && Input.GetMouseButtonDown(1))
        {
            _isObjectSelected = true;
            _mouseStartPosition = Input.mousePosition;
        }
        // call it here instead of using OnMouseDrag
        WhileDragging();
    }

    private static Vector3 Lerp(Vector3 start, Vector3 end, float time)
    {
        return start + (end - start) * time;
    }

    private static Vector3 QuadraticCurve(Vector3 start, Vector3 middle, Vector3 end, float time)
    {
        Vector3 point0 = Lerp(start, middle, time);
        Vector3 point1 = Lerp(middle, end, time);
        return Lerp(point0, point1, time);
    }

    private static float QuadraticCurve(float start, float middle, float end, float time)
    {
        float point0 = Mathf.Lerp(start, middle, time);
        float point1 = Mathf.Lerp(middle, end, time);
        return Mathf.Lerp(point0, point1, time);
    }
}
