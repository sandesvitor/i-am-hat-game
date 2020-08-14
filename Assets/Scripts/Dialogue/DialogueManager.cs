using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    
    public GameObject dialogueContainer;
    public GameObject container_NPC;
    public GameObject container_Player;
    public GameObject playerChoicePrefab;
    public Text text_NPC;

    bool dialoguePaused = false; 
    bool animatingText = false; 

    private List<Text> currentChoices = new List<Text>();

    IEnumerator NPC_TextAnimator;


    void Start() {
        dialogueContainer.SetActive(false);
    }

    //void Awake() {
    //    VD.LoadState("Bar", true);
    //}

    public void Interact(VIDE_Assign dialog) {


        if (!VD.isActive) {
            Begin(dialog);
        } else {
            CallNext();
        }
    }

    void Begin(VIDE_Assign dialog) {

        text_NPC.text = "";

        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(dialog);

        dialogueContainer.SetActive(true);
    }

    public void CallNext() {
        if (animatingText) { CutTextAnim(); return; }

        if (!dialoguePaused){
            VD.Next();
        } 
    }

    void Update() {
        var data = VD.nodeData;

        if (VD.isActive) {
            if (!data.pausedAction && data.isPlayer) {
                if (Input.GetKeyDown(KeyCode.S)) {
                    if (data.commentIndex < currentChoices.Count - 1)
                        data.commentIndex++;
                }
                
                if (Input.GetKeyDown(KeyCode.W)) {
                    if (data.commentIndex > 0)
                        data.commentIndex--;
                }

                for (int i = 0; i < currentChoices.Count; i++) {
                    currentChoices[i].color = Color.white;
                    if (i == data.commentIndex) currentChoices[i].color = Color.yellow;
                }

            }
        }
    }

    void UpdateUI(VD.NodeData data) {

        foreach(Text op in currentChoices) {
            Destroy(op.gameObject);
        }
        currentChoices = new List<UnityEngine.UI.Text>();
        text_NPC.text = "";
        container_NPC.SetActive(false);
        container_Player.SetActive(false);

        if (data.isPlayer) {

            SetOptions(data.comments);            
            container_Player.SetActive(true);

        } else {

            NPC_TextAnimator = DrawText(data.comments[data.commentIndex], 0.02f);
            StartCoroutine(NPC_TextAnimator);

            container_NPC.SetActive(true);
        }
    }


    public void SetOptions(string[] choices) {
        for (int i = 0; i < choices.Length; i++) {
            GameObject newOp = Instantiate(playerChoicePrefab.gameObject, playerChoicePrefab.transform.position, Quaternion.identity) as GameObject;
            newOp.transform.SetParent(playerChoicePrefab.transform.parent, true);
            newOp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60 - (45 * i));
            newOp.GetComponent<RectTransform>().sizeDelta = new Vector2(860, 74);
            newOp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newOp.GetComponent<UnityEngine.UI.Text>().text = choices[i];
            newOp.SetActive(true);

            currentChoices.Add(newOp.GetComponent<UnityEngine.UI.Text>());
        }
    }

    void End(VD.NodeData data) {
        //dialogContainer.SetActive(false);
        container_NPC.SetActive(false);
        container_Player.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        dialogueContainer.SetActive(false);
        VD.EndDialogue();

        //VD.SaveState("Bar", true);
    }

    void OnDisable() {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        if (dialogueContainer != null) {
            dialogueContainer.SetActive(false);
        }
        VD.EndDialogue();
    }

    public void SetPlayerChoice(int choice) {
        VD.nodeData.commentIndex = choice;
        if (Input.GetMouseButtonDown(0)) {
            VD.Next();
        }
    }

    void OnLoadedAction() {
        Debug.Log("Finished loading all dialogues");
        VD.OnLoaded -= OnLoadedAction;
    }

    IEnumerator DrawText(string text, float time) {
        animatingText = true;

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++) {
            string word = words[i];
            if (i != words.Length - 1) word += " ";

            string previousText = text_NPC.text;

            float lastHeight = text_NPC.preferredHeight;
            text_NPC.text += word;
            if (text_NPC.preferredHeight > lastHeight) {
                previousText += System.Environment.NewLine;
            }

            for (int j = 0; j < word.Length; j++) {
                text_NPC.text = previousText + word.Substring(0, j + 1);
                yield return new WaitForSeconds(time);
            }
        }
        text_NPC.text = text;
        animatingText = false;
    }

    void CutTextAnim() {
        StopCoroutine(NPC_TextAnimator);
        text_NPC.text = VD.nodeData.comments[VD.nodeData.commentIndex]; 		
        animatingText = false;
    }
}
