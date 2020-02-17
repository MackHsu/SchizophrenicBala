using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class Scene1Manager : MonoBehaviour
{
    public GameObject canvas;

    GameObject player;
    GameObject popUp;
    bool tipsShowed = false;
    List<bool> flags;
    bool itemExits = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemExits)
        {
            foreach (Item item in GameManager.save.inventory)
            {
                if (item.id == 0)
                {
                    //如果玩家inventory中存在0号物品（钢管），则把场景中的可拾取钢管删除
                    itemExits = true;
                    break;
                }
            }
            if (!itemExits)
            {
                GameObject steelPipe = new GameObject("SteelPipe");
                steelPipe.transform.position = new Vector3(-1, -2.72f, 0);
                steelPipe.AddComponent<SpriteRenderer>().sprite = Resources.Load("Images/Items/SteelPipe", typeof(Sprite)) as Sprite;
                steelPipe.AddComponent<BoxCollider2D>().isTrigger = true;
                steelPipe.AddComponent<HintTrigger>().canvas = canvas;
                steelPipe.AddComponent<SteelPipe>();
                Pickable steelPipePick = steelPipe.AddComponent<Pickable>();
                steelPipePick.itemId = 0;
                steelPipePick.number = 1;
                itemExits = true;
            }
        }
        if (GameManager.focusStack.Count == 0)
        {
            if (GameManager.save.flags["S1F1"] == false)
            {
                //显示对话0
                popUp = HintManager.ShowDialogue(canvas, 0);
                GameManager.save.flags["S1F1"] = true;
            }
            else if (GameManager.save.flags["S1F2"] == false && popUp == null)
            {
                //对话0结束后显示提示1
                popUp = HintManager.ShowTips(canvas, 1);
                GameManager.save.flags["S1F2"] = true;
            }
            else if (GameManager.save.flags["S1F2"] == true)
            {

            }
        }
    }
}
