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
    List<bool> flags;
    bool qteTriggered = false;
    bool itemExits = false;
    int qteCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().moveSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemExits)
        {
            //如果库存中没有0号物品（钢管），则创建一个可拾取的钢管
            foreach (Item item in GameManager.save.inventory)
            {
                if (item.id == 0)
                {
                    GameObject.Find("QTERoot").transform.Find("QTETrigger").gameObject.SetActive(true);
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
            else if (GameManager.save.flags["S1F3"] == true && !qteTriggered)
            {
                player.GetComponent<PlayerMovement>().moveSwitch = false;
                qteTriggered = true;
                GameObject qtePrefab = Resources.Load("Prefabs/QTEButton") as GameObject;
                GameObject qte = Instantiate(qtePrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
                QTEButton qtebutton = qte.GetComponent<QTEButton>();
                qtebutton.correctEvent += qteCorrect;
                qtebutton.falseEvent += qteFail;
                qtebutton.StartQte();
            }
        }
    }

    private void qteCorrect()
    {
        qteCount++;
        Debug.Log("QTE " + qteCount + " Succeeded!");
        if (qteCount == 3)
        {
            StartCoroutine(playerMoveSwitch());
            return;
        }
        GameObject qtePrefab = Resources.Load("Prefabs/QTEButton") as GameObject;
        GameObject qte = Instantiate(qtePrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
        QTEButton qtebutton = qte.GetComponent<QTEButton>();
        qtebutton.correctEvent += qteCorrect;
        qtebutton.falseEvent += qteFail;
        qtebutton.StartQte();
    }

    private void qteFail()
    {
        qteCount++;
        StartCoroutine(playerMoveSwitch());
        Debug.Log("QTE " + qteCount + " Failed!");
    }

    IEnumerator playerMoveSwitch()
    {
        //防止qte结束后方向键未松开导致意外的移动
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerMovement>().moveSwitch = true;
    }
}
