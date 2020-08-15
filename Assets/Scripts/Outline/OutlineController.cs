using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour {

    private MeshRenderer _renderer;

    [SerializeField] private float _maxOutlineWidth = 0.2f;
    [SerializeField] private Color _outlineColor = Color.yellow;

    private void Start() {
        _renderer = GetComponent<MeshRenderer>();
        
        foreach (var mat in _renderer.materials) {
            mat.SetColor("_OutlineColor", _outlineColor);
            mat.SetFloat("_Outline", 0f);
        }

        foreach(Transform child in this.transform){
            child.GetComponent<MeshRenderer>().material.SetFloat("_Outline", 0f);
        }

    }


    private void ShowOutline() {

        foreach (var mat in _renderer.materials) {
            mat.SetFloat("_Outline", _maxOutlineWidth);
            mat.SetColor("_OutlineColor", _outlineColor);
        }

        foreach (Transform child in this.transform) {
            child.GetComponent<MeshRenderer>().material.SetFloat("_Outline", _maxOutlineWidth);
            child.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", _outlineColor);
        }
    }

    private void HideOutline() {

        foreach (var mat in _renderer.materials) {
            mat.SetFloat("_Outline", 0f);
        }

        foreach (Transform child in this.transform) {
            child.GetComponent<MeshRenderer>().material.SetFloat("_Outline", 0f);
        }
    }

    private void OnMouseEnter() {
        ShowOutline();
    }
    private void OnMouseExit() {
        HideOutline();
    }

}
