using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _sensitivityX = 2f;

    private CharacterController _controller;
    private Hat_Player _hat;
    private Target _target;

    private InventoryManager _invetoryManager;
    private DialogueManager _dialogueManager;
    private AudioManager _audioManager;


    // VIDE Dialogue Manager:
    public VIDE_Assign inTrigger;
    public List<string> items = new List<string>();
    public List<string> itemsInventory = new List<string>();

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _hat = GameObject.Find("Hat_Player").GetComponent<Hat_Player>();
        _target = GameObject.Find("TARGET").GetComponent<Target>();
        _invetoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        _dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();


        if (_controller == null) {
            Debug.LogError("Character Controller Component is NULL.");
        }
        if (_hat == null) {
            Debug.LogError("Hat Component is NULL.");
        }        
        if(_target == null){
            Debug.LogError("Target Component is NULL.");
        }        
        if(_invetoryManager == null) {
            Debug.LogError("Inventory Manager Component is NULL.");
        }
        if(_dialogueManager == null) {
            Debug.LogError("Dialogue Manager Component is NULL.");
        }
        if(_audioManager == null) {
            Debug.LogError("Audio Manager Component is NULL.");
        }

        _invetoryManager.SetCurrentPlayerItems();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        if (!VD.isActive) {
            CalculatingMovement();
            //LookX();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            TryInteract();
        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(_target.name);
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

    void TryInteract() {

        if (inTrigger) {
            _dialogueManager.Interact(inTrigger);
            return;
        }


        // Only if the Player is looking forward:
        //
        //RaycastHit rHit;
        //if (Physics.Raycast(transform.position, transform.forward, out rHit, 2)) {

        //    VIDE_Assign assigned;

        //    if (rHit.collider.GetComponent<VIDE_Assign>() != null)
        //        assigned = rHit.collider.GetComponent<VIDE_Assign>();
        //    else return;

        //}
    }

    void TransferHat(GameObject newPlayer) {

        newPlayer.GetComponent<Player>().enabled = true;
        newPlayer.tag = "Player";

        this.GetComponent<Player>().enabled = false;
        this.transform.tag = "NPC";

        _hat.HatTransfer(newPlayer.transform);
        _invetoryManager.SetCurrentPlayerItems();

        // chamar a função SetCurrentPlayerItems() para atualizar o inventório!

    }


    private void OnTriggerEnter(Collider other) {

        if (other.GetComponent<VIDE_Assign>() != null) {
            inTrigger = other.GetComponent<VIDE_Assign>();
        }

        if(other.tag == "MusicBox") {
            _audioManager.EnableJukeboxPanel(true);
        }

        //if (this.transform.tag == "Player" && other.tag == "NPC") {


        //    if (other.transform.Find("Hat_NPC") == null) {

        //        TransferHat(other.gameObject);

        //    } else {

        //        Debug.Log("DUDE ALREAD HAVE A HAT...");

        //    }

        //}
    }

    private void OnTriggerExit(Collider other) {
        inTrigger = null;

        if(other.tag == "MusicBox"){
            _audioManager.EnableJukeboxPanel(false);
        }

    }
}
