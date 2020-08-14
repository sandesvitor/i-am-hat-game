using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject itemPrefab;
    public Text moneyDisplay;
    private string[] _currentInventory;

    private bool _isInventoryOpen = false;

    void Start() {
        inventory.SetActive(false);
        moneyDisplay.gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) && _isInventoryOpen == false) {
            ExpandInventory(true);
            _isInventoryOpen = true;
        } else if (Input.GetKeyDown(KeyCode.Tab) && _isInventoryOpen == true) {
            ExpandInventory(false);
            _isInventoryOpen = false;
        }
    }

    private void ExpandInventory(bool expand) {
        inventory.SetActive(expand);
        moneyDisplay.gameObject.SetActive(expand);
    }

    public void SetCurrentPlayerItems() {

        Inventory currentPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _currentInventory = currentPlayer.GetInventoryItems();

        foreach (Transform child in inventory.transform) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _currentInventory.Length; i++) {

            GameObject newItem = Instantiate(itemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newItem.transform.SetParent(inventory.transform, true);
            //newItem.transform.parent = inventory.transform;

            newItem.GetComponentInChildren<Text>().text = _currentInventory[i];

        }
    }
}
