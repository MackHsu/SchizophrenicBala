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
    public static List<GameObject> focusStack;  //用一个栈来记录打开的UI（如菜单、物品栏、对话框），栈顶为当前操作的UI，栈空则为对Player进行操作

    public static Dictionary<string, bool> flags;

    // Start is called before the first frame update
    void Start()
    {
        personalities = JsonMapper.ToObject(Resources.Load<TextAsset>("json/personalitiesTest").text);
        textJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/text").text);
        itemsJson = JsonMapper.ToObject(Resources.Load<TextAsset>("json/items").text);
        player = GameObject.Find("Player");
        focusStack = new List<GameObject>();

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (focusStack.Count == 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            //打开菜单
            GameObject canvas = GameObject.Find("Canvas");
            GameObject menuPrefab = Resources.Load("Prefabs/menu") as GameObject;
            GameObject menu = Instantiate(menuPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
            focusStack.Add(menu);
        }
        else if (focusStack.Count == 0 && Input.GetKeyDown(KeyCode.B))
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject inventoryPrefab = Resources.Load("Prefabs/Inventory") as GameObject;
            GameObject inventory = Instantiate(inventoryPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
            focusStack.Add(inventory);
        }
        else if (focusStack.Count != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            //在展示弹出窗口时按下escape，关闭弹窗(出栈并destroy)
            Destroy(focusStack[focusStack.Count - 1]);
        }
    }
}
