using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button cancel;
    public Text desc;

    List<Item> inventory;
    public List<GameObject> slots = new List<GameObject>();
    int page;
    int pageCount;
    Color color;
    public Text pageText;
    public Button previousPage;
    public Button nextPage;

    // Start is called before the first frame update
    void Start()
    {
        color = slots[0].GetComponent<Image>().color;
        cancel.onClick.AddListener(Cancel);
        //GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = false;
        inventory = GameManager.save.inventory;
        page = 1;
        double temp = inventory.Count / 18d;
        pageCount = (int)Math.Ceiling(temp);
        pageText.text = "1/" + pageCount;
        previousPage.onClick.AddListener(() => { ChangePage(-1); });
        nextPage.onClick.AddListener(() => { ChangePage(1); });
        for (int i = 0; i < inventory.Count && i < 18 * page; i++)
        {
            GameObject slot = slots[i];
            slot.GetComponent<ItemSlot>().SetItem(inventory[i].id, inventory[i].count);
        }

        foreach (GameObject slot in slots)
        {
            slot.GetComponent<Button>().onClick.AddListener(() => { ItemClick(slot); });
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
        foreach(GameObject tempSlot in slots)
        {
            tempSlot.GetComponent<Image>().color = color;
        }
        slot.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        desc.text = slot.GetComponent<ItemSlot>().description;
    }

    void ChangePage(int pagePlus)
    {
        if (page + pagePlus > pageCount || page + pagePlus < 0) return;

        page += pagePlus;
        pageText.text = page + "/" + pageCount;
        desc.text = "";

        foreach(GameObject slot in slots)
        {
            slot.GetComponent<Button>().interactable = false;
            slot.GetComponent<Image>().color = color;
            slot.transform.Find("ItemImage").GetComponent<Image>().color = new Color(1, 1, 1, 0);
            slot.transform.Find("Count").gameObject.SetActive(false);
        }

        for (int i = 0; i < inventory.Count && i < 18 * page; i++)
        {
            GameObject slot = slots[i];
            slot.GetComponent<ItemSlot>().SetItem(inventory[i + (page - 1) * 18].id, inventory[i + (page - 1) * 18].count);
        }
    }
}
