using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SceneManager : MonoBehaviour
{
    public GameObject canvas;

    GameObject player;
    GameObject dialogue;
    bool tipsShowed = false;
    GameObject tips;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (GameManager.save.flags["S1F1"] == false)
            dialogue = HintManager.ShowDialogue(canvas, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.save.flags["S1F1"] == false)
        {
            if (tipsShowed == false && dialogue == null)
            {
                //开场对话已经结束，需要展示拾取提示
                tips = HintManager.ShowTips(canvas, 1);
                tipsShowed = true;
            }
            else if (tipsShowed == true && Input.GetKeyDown(KeyCode.Return))
            {
                //关闭拾取提示，剧情进度S1F1完成，角色可以移动
                GameManager.save.flags["S1F1"] = true;
                Destroy(tips);
                player.GetComponent<PlayerMovement>().canMove = true;
            }

        }
        else
        {
            //已经完成了剧情进度S1F1
        }
    }
}
