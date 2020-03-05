using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class PersonalityBox : MonoBehaviour
{
    public List<GameObject> slots;
    public Text description;
    public Button confirm;
    public Button cancel;
    JsonData personalityJson;
    Color unselectedColor;
    int selectedIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(ChangePersonality);
        cancel.onClick.AddListener(() => { Destroy(gameObject); });
        GameManager.focusStack.Add(gameObject);
        personalityJson = GameManager.personalities;
        unselectedColor = slots[0].GetComponent<Image>().color;
        for(int i = 0; i < personalityJson.Count && i < 8; i++)
        {
            JsonData personality = personalityJson[i];
            slots[i].GetComponent<PersonalitySlot>().SetPersonality(i, personality);
        }
        foreach(GameObject slot in slots)
        {
            slot.GetComponent<Button>().onClick.AddListener(()=> { SlotClick(slot); });
        }
    }

    private void Update()
    {
        if (GameManager.focusStack[GameManager.focusStack.Count - 1] == gameObject && Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.focusStack.Remove(gameObject);
        if (GameManager.focusStack.Count == 0)
            GameObject.Find("Canvas/Mask").SetActive(false);
    }

    void SlotClick(GameObject slot)
    {
        selectedIndex = slot.GetComponent<PersonalitySlot>().slotIndex;
        foreach(GameObject s in slots)
        {
            s.GetComponent<Image>().color = unselectedColor;
        }
        slots[selectedIndex].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        description.text = (string)slots[selectedIndex].GetComponent<PersonalitySlot>().personality["description"];
    }

    void ChangePersonality()
    {
        if (selectedIndex == -1)
        {
            description.text = "请选择一个人格。";
        }
        JsonData personality = slots[selectedIndex].GetComponent<PersonalitySlot>().personality;
        GameManager.ChangePersonality((int)personality["pid"]);
        Destroy(gameObject);
    }
}
