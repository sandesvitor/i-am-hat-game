using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat_NPC : MonoBehaviour
{
    private GameObject _parentObject;
    
    void Start()
    {
        _parentObject = this.transform.parent.gameObject;
        if(_parentObject != null) {
            Debug.Log($"HAT_NPC in GameObject {_parentObject.name}");
        } else {
            Debug.LogError("Parent of HAT_NPC is NULL.");
        }

        SetPosition();

    }

    void SetPosition() {
        this.transform.position = this.transform.parent.position
            + new Vector3(0, this.transform.parent.localScale.y - 0.10f, 0);
    }
}
