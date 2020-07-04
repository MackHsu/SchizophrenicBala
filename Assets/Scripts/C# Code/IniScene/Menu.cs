using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button saveBtn;
    public Button exitBtn;
    public Button cancelBtn;
    public Button backToWelcomeBtn;
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
        backToWelcomeBtn.onClick.AddListener(BackToWelcomeScene);
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
    }

    void Cancel()
    {
        Destroy(gameObject);
    }

    void Exit()
    {
        Application.Quit();
    }

    void BackToWelcomeScene()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

}
