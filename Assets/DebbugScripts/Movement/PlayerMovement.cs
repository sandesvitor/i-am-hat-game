using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    private CharacterController _controller;


    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("Character Controller Component is NULL.");
        }

    }

    void Update()
    {

        CalculatingMovement();
        //CalculatingMovementWithRaycast();

    }

    private void CalculatingMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * _speed;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    //private void CalculatingMovementWithRaycast()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hitInfo;

    //        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
    //        {
    //            GetComponent<NavMeshAgent>().SetDestination(hitInfo.transform.position);
    //        }
    //    }
    //}
}
