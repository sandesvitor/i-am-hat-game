using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarmanController : MonoBehaviour
{
    private Vector3 _initialPosition;
    [SerializeField] private float _movingTime = 5f;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private GameObject _speachBubble;

    void Start()
    {
        _initialPosition = new Vector3(7.62f, 2.67f, 5.42f);
        this.transform.position = _initialPosition;
        _speachBubble.SetActive(false);
        _controller = GetComponent<CharacterController>();


        // EVENTS SUBSCRIPTION:
        GameEvents.current.onPubStandTriggerStay += GoToPlayer;
        GameEvents.current.onPubStandTriggerExit += ReturnToInitPostion;
    }


    private void GoToPlayer()
    {
        StartCoroutine(TalkToPlayer("What the hell do'ya want, goddamnit!?"));

        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        float playerZPositionCache = player.position.z;

        LeanTween.moveLocalZ(this.gameObject, playerZPositionCache, _movingTime).setEaseOutQuad();
    }


    private void ReturnToInitPostion()
    {
        StartCoroutine(TalkToPlayer("$%*@, @#$$&*!"));
        LeanTween.moveLocalZ(this.gameObject, _initialPosition.z, _movingTime).setEaseOutQuad();
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


    // UNSUBSCRIBBE METHODS FROM EVENT LOOP
    private void OnDestroy()
    {
        GameEvents.current.onPubStandTriggerStay -= GoToPlayer;
        GameEvents.current.onPubStandTriggerExit -= ReturnToInitPostion;
    }
}
