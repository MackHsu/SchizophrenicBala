using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    int itemId;
    public string description;
    JsonData itemData;

    public void SetItem(int itemId)
    {
        this.itemId = itemId;
        foreach(JsonData item in GameManager.itemsJson)
        {
            if ((int)item["id"] == itemId)
            {
                itemData = item;
                break;
            }
        }
        string imagePath = (string)itemData["imagePath"];
        //gameObject.GetComponent<Image>().sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        GameObject itemImage = gameObject.transform.Find("ItemImage").gameObject;
        itemImage.GetComponent<Image>().sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        itemImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        description = (string)itemData["desc"];
        gameObject.GetComponent<Button>().interactable = true;
    }
}
