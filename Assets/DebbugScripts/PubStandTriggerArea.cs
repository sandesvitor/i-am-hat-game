using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubStandTriggerArea : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //CLUMKSY MOVEMENT WHEN IMPLEMENTING INPUT!
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameEvents.current.PubStandTriggerStay();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.current.PubStandTriggerExit();
        }
    }
}
