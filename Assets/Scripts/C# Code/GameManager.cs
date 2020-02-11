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
    public static Save save;
    static GameObject player;
    public static bool menuDisplaying = false;    //是否正在显示菜单

    public static Dictionary<string, bool> flags;

    // Start is called before the first frame update
    void Start()
    {
        personalities = JsonMapper.ToObject(Resources.Load<TextAsset>("json/personalitiesTest").text);
        textJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/text").text);
        itemsJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/items").text);
        player = GameObject.Find("Player");

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuDisplaying && Input.GetKeyDown(KeyCode.Escape))
        {
            //打开菜单
            GameObject canvas = GameObject.Find("Canvas");
            GameObject menuPrefab = Resources.Load("Prefabs/menu") as GameObject;
            GameObject menu = Instantiate(menuPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
            menuDisplaying = true;
        }
    }
}
