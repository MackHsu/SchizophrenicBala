using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject canvas;

    GameObject player;
    GameObject tips;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        HintManager.ShowDialogue(canvas, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
