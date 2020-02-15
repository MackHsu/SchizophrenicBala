using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadSlots : MonoBehaviour
{
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    int selectedId = 0;

    public Button confirm;
    public Button cancel;
    // Start is called before the first frame update
    void Start()
    {
        cancel.onClick.AddListener(Cancel);
        LoadDatas(slot1, 1);
        LoadDatas(slot2, 2);
        LoadDatas(slot3, 3);
        confirm.onClick.AddListener(Load);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedId < 1 || selectedId > 3 || !File.Exists("Save/save" + selectedId + ".json")) confirm.interactable = false;
        else confirm.interactable = true;
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

    void Load()
    {
        GameManager.save = JsonMapper.ToObject<Save>(File.ReadAllText("Save/save" + selectedId + ".json"));
        int scene = GameManager.save.scene;
        SceneManager.LoadScene("Scene" + scene);
    }

    public void Click(int slotId)
    {
        selectedId = slotId;
        slot1.GetComponent<Image>().color = Color.white;
        slot2.GetComponent<Image>().color = Color.white;
        slot3.GetComponent<Image>().color = Color.white;
        switch (selectedId)
        {
            case 1:
                slot1.GetComponent<Image>().color = Color.gray;
                break;
            case 2:
                slot2.GetComponent<Image>().color = Color.gray;
                break;
            case 3:
                slot3.GetComponent<Image>().color = Color.gray;
                break;
            default:
                break;
        }
    }
}
