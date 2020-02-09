using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Conversation : MonoBehaviour
{
    int textId;
    int textIndex;
    int index = 0;
    GameObject player;
    public Text characterName;
    public Text content;
    public Text hint;
    public float textSpeed = 0.1f;  //0.1秒一个字逐字播放
    string contentStr;
    JsonData textJson;
    bool displaying = false;    //文字逐渐显现的过程中

    private void Awake()
    {
        player = GameObject.Find("Player");
        textJson = GameManager.textJson;
        for(int i = 0; i < textJson.Count; i++)
        {
            if((int)textJson[i]["id"] == textId)
            {
                textIndex = i;
                break;
            }
        }
    }

    private void Update()
    {
        if (displaying)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //在逐字显现的过程中按下Enter直接将整段展示出来
                StopCoroutine("ShowContent");
                content.text = contentStr;
                displaying = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StopCoroutine("ShowContent");
                index++;
                content.text = "";
                if (index < textJson[textIndex].Count)
                    Show();
                else
                {
                    player.GetComponent<PlayerMovement>().canMove = true;
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetAndShow(int textId)
    {
        this.textId = textId;
        gameObject.SetActive(true);
        Show();
    }

    public void Show()
    {
        characterName.text = (string)textJson[textIndex]["content"][index]["characterName"];
        contentStr = (string)textJson[textIndex]["content"][index]["content"];
        StartCoroutine("ShowContent");
    }
    private IEnumerator ShowContent()
    {
        displaying = true;
        int i = 0;
        while (true)
        {
            if (i >= contentStr.Length)
            {
                displaying = false;
                yield break;
            }
            content.text += contentStr[i];
            i++;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
