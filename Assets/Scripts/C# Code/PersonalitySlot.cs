using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class PersonalitySlot : MonoBehaviour
{
    public int slotIndex;
    public JsonData personality;
    
    public void SetPersonality(int slotIndex, JsonData personality)
    {
        this.slotIndex = slotIndex;
        this.personality = personality;
        Image image = transform.Find("Image").GetComponent<Image>();
        image.sprite = Resources.Load("Images/" + (string)personality["imageFolder"] + "/avatar", typeof(Sprite)) as Sprite;
        image.color = new Color(1, 1, 1, 1);
        transform.Find("Name").GetComponent<Text>().text = (string)personality["name"];
        GetComponent<Button>().interactable = true;
    }
}
