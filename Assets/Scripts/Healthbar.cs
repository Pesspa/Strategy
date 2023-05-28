using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public Transform objectTransform;
    public Transform scaleTransform;
    public Vector3 healthBarPosition;
    private Transform _camera;
    void Start()
    {
        _camera = Camera.main.transform;
    }
    void LateUpdate()
    {
        transform.position = objectTransform.position + healthBarPosition;
        transform.rotation = _camera.transform.rotation;
    }
    public void Setup(Transform target)
    {
        objectTransform = target;
    }
    public void SetHealth(float currenthealth, float maxhealth)
    {
        float size = currenthealth / maxhealth;
        float xSize = Mathf.Clamp01(size);
        scaleTransform.localScale = new Vector3(xSize, 1f, 1f);
    }
}
