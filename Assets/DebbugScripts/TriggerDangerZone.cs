using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDangerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.gameObject.tag == "NPC")
        {
            GameEvents.current.TargetDangerZoneEnter();
        }
    }
}
