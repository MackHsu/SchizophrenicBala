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
    int side;
    string imagePath;
    GameObject player;
    public Text characterName;
    public Text content;
    public Text hint;
    public float textSpeed = 0.1f;  //0.1秒一个字逐字播放
    string contentStr;
    JsonData textJson;
    bool displaying = false;    //文字逐渐显现的过程中

    public Image imageLeft;
    public Image imageRight;

    private void Awake()
    {
        player = GameObject.Find("Player");
        textJson = GameManager.textJson;
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
                if (index < textJson[textIndex]["content"].Count)
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
        for (int i = 0; i < textJson.Count; i++)
        {
            if ((int)textJson[i]["id"] == this.textId)
            {
                textIndex = i;
                break;
            }
        }
        Show();
    }

    public void Show()
    {
        characterName.text = (string)textJson[textIndex]["content"][index]["characterName"];
        contentStr = (string)textJson[textIndex]["content"][index]["content"];
        side = (int)textJson[textIndex]["content"][index]["side"];
        imagePath = (string)textJson[textIndex]["content"][index]["imagePath"];

        Image image = side == 0 ? imageLeft : imageRight;
        Image otherImage = side == 0 ? imageRight : imageLeft;
        Sprite sprite = Resources.Load("Images/" + imagePath, typeof(Sprite)) as Sprite;
        if (!image.gameObject.activeSelf) image.gameObject.SetActive(true);
        image.sprite = sprite;
        image.color = new Color(1, 1, 1, 1);
        if (otherImage.gameObject.activeSelf)
        {
            otherImage.color = new Color(1, 1, 1, 0.5f);
        }

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
