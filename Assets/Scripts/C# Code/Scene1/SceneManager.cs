using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject player;
    Conversation conversation;

    // Start is called before the first frame update
    void Start()
    {
        GameObject conversationPrefab = Resources.Load("Prefabs/Conversation") as GameObject;
        GameObject con = Instantiate(conversationPrefab, canvas.transform.position,Quaternion.identity,canvas.transform);
        con.SetActive(false);
        conversation = con.GetComponent<Conversation>();
        player.GetComponent<PlayerMovement>().canMove = false;
        conversation.SetAndShow(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
