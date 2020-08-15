using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarmanMove : MonoBehaviour
{
    [SerializeField] private Transform _barman;
    [SerializeField] private DialogueManager _dialogueManager;

    private void Start() {
        _dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }


    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player") {

            VIDE_Assign barmanDialogue = _barman.GetComponent<VIDE_Assign>();

            if (Input.GetKeyDown(KeyCode.E)) {

                if (barmanDialogue) {
                    _dialogueManager.Interact(barmanDialogue);
                }

            }          
       

        }
    }

    public void BartenderMove() {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _barman.position = new Vector3(_barman.position.x, _barman.position.y, player.position.z);
        //_dialogueManager.CallNext();
    }
}
