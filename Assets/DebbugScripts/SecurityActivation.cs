using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecurityActivation : MonoBehaviour
{
    [SerializeField] private float _securityResponseTime = 0.5f;
    [SerializeField] private float _tackleImpact = 10f;
    [SerializeField] private GameObject _speachBubble;
    private Vector3 _initPosition;

    void Start()
    {
        _speachBubble.SetActive(false);
        _initPosition = new Vector3(1.47f, 3.39f, 9.17f);


        // EVENT SUBSCRIPTION:
        GameEvents.current.onSecurityActivation += ProtectTarget;
    }

    private void ProtectTarget()
    {
        StartCoroutine(SecurityShout());

        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LeanTween.move(this.gameObject, player.position, _securityResponseTime).setEaseOutQuad();

        LeanTween.move(
                player.gameObject,
                new Vector3(player.position.x, player.position.y, player.position.z - _tackleImpact),
                _securityResponseTime
            ).setEaseOutQuad();

        StartCoroutine(ReturnToInitPosition());


    }


    // TIRAR DO HARD CODE E IMPLEMENTAR UM EVENTO PARA DIÁLOGOS!
    private IEnumerator SecurityShout()
    {
        _speachBubble.SetActive(true);
        TMP_Text dialogBox = _speachBubble.GetComponentInChildren<TMP_Text>();
        dialogBox.text = "BACK AWAY FROM THE V.I.P.";

        yield return new WaitForSeconds(1.5f);
        dialogBox.text = "";
        _speachBubble.SetActive(false);


    }

    private IEnumerator ReturnToInitPosition()
    {
        yield return new WaitForSeconds(0.8f);
        LeanTween.move(this.gameObject, _initPosition, 1.8f).setEaseOutQuad();
    }

    private void OnDestroy()
    {
        GameEvents.current.onSecurityActivation -= ProtectTarget;
    }

}
