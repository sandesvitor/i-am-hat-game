using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }


    /*
     * 
     * PLAYER INVOKING BARMAN EVENTS:
     * 
    */
    public event Action onPubStandTriggerEnter;
    public void PubStandTriggerEnter()
    {
        if (onPubStandTriggerEnter != null)
        {
            onPubStandTriggerEnter();
        }
    }

    public event Action onPubStandTriggerExit;
    public void PubStandTriggerExit()
    {
        if (onPubStandTriggerExit != null)
        {
            onPubStandTriggerExit();
        }
    }

    public event Action<Transform> onLoungeTableTriggerEnter;
    public void LoungeTableTriggerEnter(Transform player)
    {
        if (onLoungeTableTriggerEnter != null)
        {
            onLoungeTableTriggerEnter(player);
        }
    }


    /*
     * 
     * TARGET AND MINIONS EVENTS:
     * 
    */
    public event Action onTargetDangerZoneEnter;
    public void TargetDangerZoneEnter()
    {
        if (onTargetDangerZoneEnter != null)
        {
            onTargetDangerZoneEnter();
        }
    }

    public event Action onSecurityActivation;
    public void SecurityActivation()
    {
        if (onSecurityActivation != null)
        {
            onSecurityActivation();
        }
    }


    /*
     * 
     * GENERIC DIALOGUE EVENTS:
     * 
    */
    public event Action<int> onDialogueActivation;
    public void DialogueActivation(int id)
    {
        if (onDialogueActivation != null)
        {
            onDialogueActivation(id);
        }
    }


    /*
     * 
     * GENERIC CLICK EVENTS
     * 
    */
    public event Action<int> onNPCClick;
    public void NPCClick(int id)
    {
        if (onNPCClick != null)
        {
            onNPCClick(id);
        }
    }


    /*
     * 
     * GENERIC MENU EVENTS
     * 
     */
    public event Action<Transform> onActionMenuActivated;
    public void ActionMenuActivated(Transform target)
    {
        if (onActionMenuActivated != null)
        {
            onActionMenuActivated(target);
        }
    }

}
