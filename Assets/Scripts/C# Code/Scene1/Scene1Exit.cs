using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Exit : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach(Item item in GameManager.save.inventory)
            {
                if(item.id == 0)
                {
                    SceneManager.LoadScene("Scene2");
                    return;
                }
            }
            // TODO: 弹窗提示捡起钢管
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            HintManager.ShowTips(canvas, 11);
        }
    }
}
