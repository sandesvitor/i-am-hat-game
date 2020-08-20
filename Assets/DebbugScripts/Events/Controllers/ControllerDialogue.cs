using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using TMPro;

public class ControllerDialogue : MonoBehaviour
{
    [SerializeField] private GameObject _speechBubble;
    [SerializeField] private TMP_Text _dialogueText;

    void Start()
    {
        _speechBubble.SetActive(false);
                
        GameEvents.current.onDialogueActivation += StartsDialogue;               
    }

    private void StartsDialogue(int id)
    {
        
        ControllerNPC controllerNPC = this.GetComponent<ControllerNPC>();

        if (controllerNPC != null)
        {
            int targetId = controllerNPC.sceneID;
            if (targetId == id)
            {
                VIDE_Assign videAssign = this.GetComponent<VIDE_Assign>();
                if (videAssign != null)
                {
                    if (videAssign.assignedDialogue != "")
                    {
                        if (!VD.isActive)
                        {
                            Begin(videAssign);
                        }
                        else
                        {
                            VD.Next();
                        }
                    }
                    else
                    {
                        Debug.Log($"Controller NPC and VIDE_Assign Components are UP, " +
                            $"but there are no dialogue assign to VIDE_Assign in {this.gameObject}.");
                    }
                }
                else
                {
                    Debug.LogError($"Controller NPC is UP but VIDE_Assign is NULL in {this.gameObject}");
                }
            }
        }
        else
        {
            Debug.LogError($"Controller NPC is NULL in {this.gameObject}");
        }
        

    }


    private void Begin(VIDE_Assign dialog)
    {
        _dialogueText.text = "";
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(dialog);
    }

    private void UpdateUI(VD.NodeData data)
    {
        _dialogueText.text = "";
        _speechBubble.SetActive(true);
        _dialogueText.text = data.comments[0];

        string dialogLine;
        for (int i = 0; i < data.comments.Length; i++)
        {
            dialogLine = data.isPlayer ? $"PLAYER: {data.comments[i]}" : $"{this.gameObject.name}: {data.comments[i]}";
            _dialogueText.text = dialogLine;

            Debug.Log(dialogLine);
        }
    }

    private void End(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
        GameEvents.current.onDialogueActivation -= StartsDialogue;
    }

    private void OnDisable()
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    private void OnDestroy()
    {
        GameEvents.current.onDialogueActivation += StartsDialogue;
    }

}
