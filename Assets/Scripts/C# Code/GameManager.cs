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
        //personalities = JsonMapper.ToObject(UTF82GB2312(Resources.Load<TextAsset>("json/personalitiesTest").text));
        //textJson = JsonMapper.ToObject(UTF82GB2312(Resources.Load<TextAsset>("json/text").text));
        //itemsJson = JsonMapper.ToObject(UTF82GB2312(Resources.Load<TextAsset>("json/items").text));

        personalities = JsonMapper.ToObject(Resources.Load<TextAsset>("json/personalitiesTest").text);
        textJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/text").text);
        itemsJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/items").text);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private string UTF82GB2312(string str1)
    //{
    //    Encoding utf8 = Encoding.GetEncoding(65001);
    //    Encoding gb2312 = Encoding.GetEncoding("GB2312");
    //    byte[] temp1 = utf8.GetBytes(str1);
    //    byte[] temp2 = Encoding.Convert(utf8, gb2312, temp1);
    //    string str2 = gb2312.GetString(temp2);
    //    return str2;
    //}
}
