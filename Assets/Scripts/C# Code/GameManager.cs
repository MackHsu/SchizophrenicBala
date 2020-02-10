using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;


public class GameManager : MonoBehaviour
{
    public static JsonData personalities;
    public static JsonData textJson;
    public static JsonData itemsJson;

    public static Dictionary<string, bool> flags;

    // Start is called before the first frame update
    void Start()
    {
        personalities = JsonMapper.ToObject(Resources.Load<TextAsset>("json/personalitiesTest").text);
        textJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/text").text);
        itemsJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/items").text);
        

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
