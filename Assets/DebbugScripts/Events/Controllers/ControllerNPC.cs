using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerNPC : MonoBehaviour
{
    private Collider _collider;
    public int sceneID;

    void Start()
    {
        _collider = this.GetComponent<Collider>();

        GameEvents.current.onNPCClick += ShowActionMenu;    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            // DEFINIR DISTÂNCIA PARA CLICAR!
            if (_collider.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                GameEvents.current.NPCClick(this.sceneID);
            }
        }
    }

    private void ShowActionMenu(int id)
    {
        if (this.sceneID == id)
        {
            GameEvents.current.ActionMenuActivated(this.transform);
        }        
    }

    private void ActivateDialogue()
    {
        GameEvents.current.DialogueActivation(this.sceneID);
    }

    private void OnDestroy()
    {
        GameEvents.current.onNPCClick -= ShowActionMenu;
    }
}
