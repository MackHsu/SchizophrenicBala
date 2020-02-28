using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1QTETrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().Move(0, false, false);
            collision.attachedRigidbody.velocity = new Vector2(0, collision.attachedRigidbody.velocity.y);
            HintManager.ShowDialogue(GameObject.Find("Canvas"), 5);
            GameManager.save.flags["S1F3"] = true;
            Destroy(gameObject);
        }
    }
}
