using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _sensitivityX = 2f;

    private CharacterController _controller;
    private Hat_Player _hat;
    private Target _target;
    private UIManager _uiManager;
    private DialogueManager _dialogueManager;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _hat = GameObject.Find("Hat_Player").GetComponent<Hat_Player>();
        _target = GameObject.Find("TARGET").GetComponent<Target>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _dialogueManager = GameObject.Find("Dialogue_Manager").GetComponent<DialogueManager>();


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
        if(_uiManager == null) {
            Debug.LogError("UI Manager component is NULL.");
        }
        if(_dialogueManager == null) {
            Debug.LogError("Dialogue Manager component is NULL.");
        }

    }

    void Update()
    {
        CalculatingMovement();
        //LookX();

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

    void LookX() {
        Vector3 newRotation = transform.localEulerAngles;
        float mouseX = Input.GetAxis("Mouse X");

        newRotation.y += mouseX * _sensitivityX;
        transform.localEulerAngles = newRotation;
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


            switch (other.name) {
                case "BARMAN":
                    other.transform.GetComponent<DialogueTrigger>().TriggerDialog();
                    break;
                case "DRUNK_GUY":
                    other.transform.GetComponent<DialogueTrigger>().TriggerDialog();
                    break;
                case "SHADY_GUY":
                    other.transform.GetComponent<DialogueTrigger>().TriggerDialog();
                    break;
                case "TARGET":
                    other.transform.GetComponent<DialogueTrigger>().TriggerDialog();
                    break;
                case "TARGET_SECURITY":
                    other.transform.GetComponent<DialogueTrigger>().TriggerDialog();
                    break;
                default:
                    Debug.Log(other.name);
                    break;
            }


            //    if (other.transform.Find("Hat_NPC") == null) {

            //        TransferHat(other.gameObject);

            //    } else {

            //        Debug.Log("DUDE ALREAD HAVE A HAT...");

            //    }        

        }
    }

    private void OnTriggerExit(Collider other) {

        if (this.transform.tag == "Player" && other.tag == "NPC") {  
            _dialogueManager.EnableDialogBox(false);
        }
    }
}
