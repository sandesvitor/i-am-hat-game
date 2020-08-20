using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerActionMenu : MonoBehaviour
{
    [SerializeField] private Button _talkBtn;
    [SerializeField] private Button _passHatBtn;

    void Start()
    {
        this.gameObject.SetActive(false);        

        GameEvents.current.onActionMenuActivated += PopUpActionMenuOnTarget;
    }


    private void PopUpActionMenuOnTarget(Transform target)
    {

        SetActionMenuAsChildOfTarget(target);

        AddEventListenersToActionMenuButtons(target.gameObject);
    
    }

    private void SetActionMenuAsChildOfTarget(Transform target)
    {
        float actionMenuYPosition = target.localScale.y + 1.5f;
        this.transform.SetParent(target);
        this.transform.position = new Vector3(target.position.x, target.position.y + actionMenuYPosition, target.position.z);

        this.gameObject.SetActive(true);
    }

    private void AddEventListenersToActionMenuButtons(GameObject target)
    {
        int targetId = target.GetComponent<ControllerNPC>().sceneID;

        // CLEAN MEMORY FROM OTHER LISTENERS METHODS:
        _talkBtn.onClick.RemoveAllListeners();
        _passHatBtn.onClick.RemoveAllListeners();

        // APLY NEW LISTENERS:
        _talkBtn.onClick.AddListener(() => TalkToNPC(target, targetId));
        _passHatBtn.onClick.AddListener(() => TransferHatToNPC(target, targetId));
    }

    private void TalkToNPC(GameObject target, int id)
    {
        Debug.Log($"TALKING TO {target.name} - ID: {id}");
        this.gameObject.SetActive(false);
        GameEvents.current.DialogueActivation(id);
    }

    private void TransferHatToNPC(GameObject target, int id)
    {
        Debug.Log($"TRANFERING HAT TO {target.name} - ID: {id}");
        target.GetComponent<PlayerMovement>().enabled = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().enabled = false;
        player.tag = "NPC";
        target.tag = "Player";

        Hat_Player hatPlayer = GameObject.FindGameObjectWithTag("HatPlayer").GetComponent<Hat_Player>();
        hatPlayer.HatTransfer(target.transform);

    }

    private void OnDestroy()
    {
        GameEvents.current.onActionMenuActivated -= PopUpActionMenuOnTarget;
    }
}
