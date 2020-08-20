using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoungeTableAreaEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameEvents.current.LoungeTableTriggerEnter(other.transform);
        }
    }
}
