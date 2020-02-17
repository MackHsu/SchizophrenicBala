﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class Menu : MonoBehaviour
{
    public Button saveBtn;
    public Button exitBtn;
    public Button cancelBtn;
    GameObject player;
    GameObject saveSlots;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //player.GetComponent<PlayerMovement>().canMove = false;

        saveBtn.onClick.AddListener(Save);
        cancelBtn.onClick.AddListener(Cancel);
        exitBtn.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        if (saveSlots == null && Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void OnDestroy()
    {
        //player.GetComponent<PlayerMovement>().canMove = true;
        GameManager.focusStack.Remove(gameObject);
        if (GameManager.focusStack.Count == 0)
            GameObject.Find("Canvas/Mask").SetActive(false);
    }

    void Save()
    {
        GameObject saveSlotsPrefab = Resources.Load("Prefabs/SaveSlots") as GameObject;
        GameObject canvas = GameObject.Find("Canvas");
        saveSlots = Instantiate(saveSlotsPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
        GameManager.focusStack.Add(saveSlots);
        //if (!File.Exists("save.json")) File.Create("save.json").Close();
        //using (StreamWriter sw = new StreamWriter(new FileStream("save.json", FileMode.Truncate)))
        //{
        //    Save save = new Save();
        //    sw.Write(JsonMapper.ToJson(save));
        //    sw.Close();
        //}
    }

    void Cancel()
    {
        Destroy(gameObject);
    }

    void Exit()
    {
        Application.Quit();
    }

}
