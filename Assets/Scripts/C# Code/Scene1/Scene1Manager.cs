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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
        //if (GameManager.save.flags["S1F1"] == false)
        //{
        //    if (tipsShowed == false && GameManager.focusStack.Count == 0)
        //    {
        //        //开场对话已经结束，需要展示拾取提示
        //        tips = HintManager.ShowTips(canvas, 1);
        //        tipsShowed = true;
        //    }
        //    else if (tipsShowed == true && Input.GetKeyDown(KeyCode.Return))
        //    {
        //        //关闭拾取提示，剧情进度S1F1完成，角色可以移动
        //        GameManager.save.flags["S1F1"] = true;
        //        Destroy(tips);
        //        //player.GetComponent<PlayerMovement>().canMove = true;
        //    }

        //}
        //else
        //{
        //    //已经完成了剧情进度S1F1
        //}
    }
}
