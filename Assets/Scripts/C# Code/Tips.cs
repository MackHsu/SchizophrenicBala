using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.focusStack.Remove(gameObject);
        if (GameManager.focusStack.Count == 0)
            GameObject.Find("Canvas/Mask").SetActive(false);
    }

    private void Update()
    {
        if (GameManager.focusStack[GameManager.focusStack.Count - 1] == gameObject && Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(gameObject);
        }
    }
}
