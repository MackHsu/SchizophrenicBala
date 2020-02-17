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
        gameObject.GetComponent<Image>().sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        description = (string)itemData["desc"];
        gameObject.SetActive(true);
    }
}
