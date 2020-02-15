using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button cancel;

    List<Item> inventory;
    int page;

    // Start is called before the first frame update
    void Start()
    {
        cancel.onClick.AddListener(Cancel);
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
        inventory = GameManager.save.inventory;
        page = 1;
        for (int i = 0; i < inventory.Count && i < 18 * page; i++)
        {
            int itemId = inventory[i].id;
            GameObject slot = gameObject.transform.Find("Box").Find("Slots").Find("Slot" + i).gameObject;
            slot.GetComponent<ItemSlot>().SetItem(itemId);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
    }

    void Cancel()
    {
        GameManager.inventoryDisplaying = false;
        Destroy(gameObject);
    }
}
