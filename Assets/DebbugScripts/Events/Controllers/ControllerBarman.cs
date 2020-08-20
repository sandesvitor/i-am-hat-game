using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ControllerBarman : MonoBehaviour
{
    private Vector3 _initialPosition;
    private NavMeshAgent _navMeshAgent;
    private CharacterController _controller;

    [SerializeField] private float _movingTime = 5f;    
    [SerializeField] private GameObject _speachBubble;
    [SerializeField] private bool _triggerActive = true;


    void Start()
    {
        //BARMAN INITIALIZATION
        _initialPosition = new Vector3(7.62f, 2.67f, 5.42f);
        this.transform.position = _initialPosition;
        _speachBubble.SetActive(false);
        _controller = GetComponent<CharacterController>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();


        // EVENTS SUBSCRIPTION:       
        GameEvents.current.onPubStandTriggerEnter += MoveToPlayerZPosition;
        GameEvents.current.onPubStandTriggerExit += ReturnToInitPostion;
        GameEvents.current.onLoungeTableTriggerEnter += MoveToPlayerTable;


    }


    private void MoveToPlayerZPosition()
    {
        if (_triggerActive)
        {
            StartCoroutine(TalkToPlayer("What the hell do'ya want, goddamnit!?"));

            Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            float playerZPositionCache = player.position.z;

            LeanTween.moveLocalZ(this.gameObject, playerZPositionCache, _movingTime).setEaseOutQuad();
        }
        else
        {
            Debug.Log("Event PubTable alread been caled; this event is canceled.");
        }

    }

    private void ReturnToInitPostion()
    {
        if (_triggerActive)
        {
            StartCoroutine(TalkToPlayer("$%*@, @#$$&*!"));
            LeanTween.moveLocalZ(this.gameObject, _initialPosition.z, _movingTime).setEaseOutQuad();
        }
        else
        {
            Debug.Log("Event PubTable alread been caled; this event is canceled.");
        }
        
    }

    private IEnumerator TalkToPlayer(string dialog)
    {
        _speachBubble.SetActive(true);
        TMP_Text dialogBox = _speachBubble.GetComponentInChildren<TMP_Text>();
        dialogBox.text = dialog;

        yield return new WaitForSeconds(2f);
        dialogBox.text = "";
        _speachBubble.SetActive(false);
    }

    private void MoveToPlayerTable(Transform player)
    {
        //if this method is called by the PubTable event, deny acess
        //to PubStand event:
        _triggerActive = false;

        //move barman to player position:
        _navMeshAgent.SetDestination(player.position);
    }


    // UNSUBSCRIBBE METHODS FROM EVENT LOOP
    private void OnDestroy()
    {
        GameEvents.current.onPubStandTriggerEnter -= MoveToPlayerZPosition;
        GameEvents.current.onPubStandTriggerExit -= ReturnToInitPostion;
        GameEvents.current.onLoungeTableTriggerEnter -= MoveToPlayerTable;
    }
}
