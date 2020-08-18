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
     * BARMAN EVENTS:
     * 
    */
    public event Action onPubStandTriggerStay;
    public void PubStandTriggerStay()
    {
        if (onPubStandTriggerStay != null)
        {
            onPubStandTriggerStay();
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

}
