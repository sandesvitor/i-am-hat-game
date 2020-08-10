using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float _gravity = 9.8f;
    private CharacterController _controller;

    void Start() {
        _controller = GetComponent<CharacterController>();

        if (_controller == null) {
            Debug.LogError("Character Controller Component is NULL.");
        }
    }

    void Update() {
        CalculatingGravity();
    }

    void CalculatingGravity() {

        Vector3 velocity = new Vector3(0, 0, 0);
        velocity.y -= _gravity;
        _controller.Move(velocity * Time.deltaTime);
    }
}
