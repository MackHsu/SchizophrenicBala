using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class QTEButton : MonoBehaviour
{
    private List<Key> keys;
    int index;
    public float speed;
    public event Action correctEvent;
    public event Action falseEvent;

    Transform outer;
    Transform inner;
    Vector3 deltaScale;
    bool pressed = false;
    bool correct = false;

    public void StartQte()
    {
        keys = new List<Key>
        {
            new Key("W",KeyCode.W),new Key("A",KeyCode.A),new Key("S",KeyCode.S),new Key("D",KeyCode.D)
        };

        deltaScale = new Vector3(speed, speed, 0f);

        GameManager.focusStack.Add(gameObject);
        index = Random.Range(0, keys.Count - 1);
        transform.Find("Text").GetComponent<Text>().text = keys[index].keyName;

        outer = transform.Find("Outer").transform;
        inner = transform.Find("Inner").transform;

        Time.timeScale = 0;
        StartCoroutine(runQte());
    }

    private void OnDestroy()
    {
        GameManager.focusStack.Remove(gameObject);
        Time.timeScale = 1;
    }

    IEnumerator runQte()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        while (true)
        {
            outer.localScale -= deltaScale;
            if (inner.localScale == outer.localScale)
            {
                falseEvent();
                Destroy(gameObject);
                yield break;
            }
            if (Input.GetKeyDown(keys[index].keyCode))
            {
                pressed = true;
                correct = true;
            }
            else if (Input.anyKeyDown)
            {
                pressed = true;
                correct = false;
            }
            if (pressed && correct)
            {
                correctEvent();
                Destroy(gameObject);
                yield break;
            }
            else if (pressed && !correct)
            {
                falseEvent();
                Destroy(gameObject);
                yield break;
            }
            yield return new WaitForSecondsRealtime(0.001F);
        }
    }
}

public class Key
{
    public string keyName;
    public KeyCode keyCode;
    public Key(string name, KeyCode code)
    {
        keyName = name;
        keyCode = code;
    }
}
