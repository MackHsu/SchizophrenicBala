using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1QTETrigger : MonoBehaviour
{
    public GameObject police1;
    public GameObject police2;
    public GameObject police3;
    public GameObject player;
    public Scene1Manager Scene1Manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // move the police officers
        StartCoroutine(PoliceMove(police1, player.transform.position.x + 1.5f));
        StartCoroutine(PoliceMove(police2, player.transform.position.x + 2.5f));
        StartCoroutine(PoliceMove(police3, player.transform.position.x + 3.5f));

        // stop the player
        collision.gameObject.GetComponent<CharacterController2D>().Move(0, false, false);
        collision.attachedRigidbody.velocity = new Vector2(0, collision.attachedRigidbody.velocity.y);

        GameObject dialog = HintManager.ShowDialogue(GameObject.Find("Canvas"), 5);
        // GameManager.save.flags["S1F3"] = true;
        dialog.GetComponent<Conversation>().DestroyEvent += Scene1Manager.StartQTE;
        Destroy(gameObject, 1);
    }

    IEnumerator PoliceMove(GameObject police, float x)
    {
        while (true)
        {
            police.transform.position -= new Vector3(0.5f, 0, 0);
            if (police.transform.position.x <= x)
            {
                yield break;
            }
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
