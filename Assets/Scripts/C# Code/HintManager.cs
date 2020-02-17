using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class HintManager
{
    public static GameObject ShowHint(GameObject canvas, int textId)
    {
        JsonData textJson = GameManager.textJson;
        GameObject hintPrefab = Resources.Load("Prefabs/Hint") as GameObject;
        GameObject hint = GameObject.Instantiate(hintPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
        GameObject text = hint.transform.Find("Box").Find("Text").gameObject;

        string content = "";
        foreach (JsonData jd in textJson)
        {
            if ((int)jd["id"] == textId)
            {
                content = (string)jd["content"];
                break;
            }
        }
        text.GetComponent<Text>().text = content;
        return hint;
    }

    public static GameObject ShowDialogue(GameObject canvas, int textId)
    {
        canvas.transform.Find("Mask").gameObject.SetActive(true);
        GameObject conversationPrefab = Resources.Load("Prefabs/Conversation") as GameObject;
        GameObject con = GameObject.Instantiate(conversationPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
        GameManager.focusStack.Add(con);
        con.SetActive(false);
        Conversation conversation = con.GetComponent<Conversation>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerMovement>().canMove = false;
        conversation.SetAndShow(textId);
        return con;
    }

    public static GameObject ShowTips(GameObject canvas, int textId)
    {
        canvas.transform.Find("Mask").gameObject.SetActive(true);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //player.GetComponent<PlayerMovement>().canMove = false;
        JsonData textJson = GameManager.textJson;
        GameObject tipsPrefab = Resources.Load("Prefabs/Tips") as GameObject;
        GameObject tips = GameObject.Instantiate(tipsPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
        GameManager.focusStack.Add(tips);
        GameObject tipsText = tips.transform.Find("Box").Find("Text").gameObject;
        string content = "";
        foreach (JsonData jd in textJson)
        {
            if ((int)jd["id"] == textId)
            {
                content = (string)jd["content"];
                break;
            }
        }
        tipsText.GetComponent<Text>().text = content;
        return tips;
    }
}
