using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 10f;
    public float scrollSpeed = 1000f;
    public float rotatespeed = 1f;
    public Transform cameraTransform;

    private Vector3 _cameraStartPosition;
    private Vector3 _targetPosition;
    private Plane _plane;
    void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update()
    {
        float _horizontal = Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime;
        float _vertical = Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;

        transform.Translate(_horizontal, 0, _vertical);

        float mousescrollWhile = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
        if (mousescrollWhile >= 10)
        {
            cameraTransform.Translate(transform.forward * 10f * -mousescrollWhile);
        }
        if(mousescrollWhile >= -10)
        {
            cameraTransform.Translate(transform.forward * 10f * mousescrollWhile);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * 10f * -rotatespeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * 10f * rotatespeed * Time.deltaTime);
        }
    }
}
