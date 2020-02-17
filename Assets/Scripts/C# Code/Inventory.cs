using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button cancel;
    public Text desc;

    List<Item> inventory;
    List<Button> slots;
    int page;

    // Start is called before the first frame update
    void Start()
    {
        cancel.onClick.AddListener(Cancel);
        //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
        inventory = GameManager.save.inventory;
        page = 1;
        for (int i = 0; i < inventory.Count && i < 18 * page; i++)
        {
            int itemId = inventory[i].id;
            GameObject slot = gameObject.transform.Find("Box").Find("Slots").Find("Slot" + i).gameObject;
            slot.GetComponent<ItemSlot>().SetItem(itemId);
        }
        slots = new List<Button>(transform.Find("Box/Slots").gameObject.GetComponent<GridLayoutGroup>().GetComponentsInChildren<Button>());
        foreach(Button slot in slots)
        {
            slot.onClick.AddListener(() => { ItemClick(slot.gameObject); });
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        GameManager.focusStack.Remove(gameObject);
        if (GameManager.focusStack.Count == 0)
            GameObject.Find("Canvas/Mask").SetActive(false);
    }

    void Cancel()
    {
        Destroy(gameObject);
    }

    void ItemClick(GameObject slot)
    {
        slot.GetComponent<Image>().color = new Color(50, 50, 50);
        desc.text = slot.GetComponent<ItemSlot>().description;
    }
}
