using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class SaveSlots : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;

    public Button confirm;
    public Button cancel;
    // Start is called before the first frame update
    void Start()
    {
        confirm.onClick.AddListener(Confirm);
        cancel.onClick.AddListener(Cancel);
        LoadDatas(slot1, 1);
        LoadDatas(slot2, 2);
        LoadDatas(slot3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Confirm()
    {

    }

    void Cancel()
    {
        Destroy(gameObject);
    }

    void LoadDatas(GameObject slot, int saveId)
    {
        if (File.Exists("Save/save" + saveId + ".json"))
        {
            GameObject texts = slot.transform.Find("Texts").gameObject;
            texts.SetActive(true);
            Save save = JsonMapper.ToObject<Save>(File.ReadAllText("Save/save" + saveId + ".json"));
            texts.transform.Find("Scene").GetComponent<Text>().text = save.scene + "";
            int personalityId = save.personalityId;
            string personality = "";
            for(int i = 0;i < GameManager.personalities.Count; i++)
            {
                if (personalityId == (int)GameManager.personalities[i]["pid"])
                {
                    personality = (string)GameManager.personalities[i]["name"];
                    break;
                }
            }
            texts.transform.Find("Personality").GetComponent<Text>().text = personality;
        }
    }
}
