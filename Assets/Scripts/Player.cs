using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private CharacterController _controller;
    private Hat_Player _hat;
    private Target _target;
    private UIManager _uiManager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _hat = GameObject.Find("Hat_Player").GetComponent<Hat_Player>();
        _target = GameObject.Find("TARGET").GetComponent<Target>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();


        if (_controller == null) {
            Debug.LogError("Character Controller Component is NULL.");
        }

        // NULL EXCEPTION WHEN HAT CHANGES PARENTING BUT PLAYER
        // STAYS IN THE FIRST GAME OBJECT!
        if (_hat == null) {
            Debug.LogError("Hat component is NULL.");
        }
        
        if(_target == null){
            Debug.LogError("Target component is NULL.");
        }   

    }

    void Update()
    {
        CalculatingMovement();

        if (Input.GetKeyDown(KeyCode.Space)) {
            _target.DestroyOwnHat();
        }
    }

    void CalculatingMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * _speed;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void TransferHat(GameObject newPlayer) {

        newPlayer.GetComponent<Player>().enabled = true;
        newPlayer.tag = "Player";

        this.GetComponent<Player>().enabled = false;
        this.transform.tag = "NPC";

        _hat.HatTransfer(newPlayer.transform);

    }


    private void OnTriggerEnter(Collider other) {

        


        if (this.transform.tag == "Player" && other.tag == "NPC") {
            
            if (other.transform.Find("Hat_NPC") == null) {

                TransferHat(other.gameObject);

            } else {

                Debug.Log("DUDE ALREAD HAVE A HAT...");

            }        

        }
    }
}
