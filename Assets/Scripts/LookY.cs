using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float _sensitivityY= 2f;
    [SerializeField]
    private bool _invertedY = false;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;

        float inverted = _invertedY ? -1f : 1f;
        newRotation.x += inverted * mouseY * _sensitivityY;

        transform.localEulerAngles = newRotation;
    }
}
