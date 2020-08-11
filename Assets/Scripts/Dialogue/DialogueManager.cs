using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    void Start() {

        _sentences = new Queue<string>();

        EnableDialogBox(false);
    }

    public void EnableDialogBox(bool enable) {
        dialogBox.SetActive(enable);
    }

    public void StartDialogue (Dialogue dialogue) {
        EnableDialogBox(true);

        nameText.text = dialogue.name;

        _sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence() {

        if(_sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        dialogText.text = sentence;

    }

    void EndDialogue() {
        EnableDialogBox(false);
    }
}
