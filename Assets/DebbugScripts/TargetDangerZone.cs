﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDangerZone : MonoBehaviour
{
    void Start()
    {
        GameEvents.current.onTargetDangerZoneEnter += ActivateSecurity;      
    }

    private void ActivateSecurity()
    {
        GameEvents.current.SecurityActivation();
    }

    private void OnDestroy()
    {
        GameEvents.current.onTargetDangerZoneEnter -= ActivateSecurity;
    }
}
