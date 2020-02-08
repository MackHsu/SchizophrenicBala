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
    public static JsonData personalities;
    public static JsonData textJson;
    public static JsonData itemsJson;

    // Start is called before the first frame update
    void Start()
    {
        personalities = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/json/personalitiesTest.json", Encoding.GetEncoding("GB2312")));
        textJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/json/text.json", Encoding.GetEncoding("GB2312")));
        itemsJson = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/json/items.json", Encoding.GetEncoding("GB2312")));
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
