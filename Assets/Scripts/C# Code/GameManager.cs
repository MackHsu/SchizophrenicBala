using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;


public class GameManager : MonoBehaviour
{

    int personalityId = 0;
    int textId = 0;
    private JsonData personalities;
    JsonData textJson;

    // Start is called before the first frame update
    void Start()
    {
        personalities = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/json/personalitiesTest.json", Encoding.GetEncoding("GB2312")));
        textJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/json/textTest.json", Encoding.GetEncoding("GB2312")));
        string pName = "bǎlà";
        for(int i = 0; i < personalities.Count; i++)
        {
            if((int)personalities[i]["pid"] == personalityId)
            {
                pName = (string)personalities[i]["name"];
                break;
            }
        }
        int textType = (int)textJson[textId]["textType"];
        string text = "";
        //对话
        if (textType == 0)
        {
            text = (string)textJson[textId]["content"][0]["content"];
        }
        //剧情文本或提示文本
        else
        {
            text = (string)textJson[textId]["content"];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
