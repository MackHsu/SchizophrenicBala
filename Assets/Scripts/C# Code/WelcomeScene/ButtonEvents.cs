using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;

public class ButtonEvents : MonoBehaviour
{
    public Button newGameButton;
    public Button loadButton;
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(Scene1);
        loadButton.onClick.AddListener(Load);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Scene1()
    {
        GameManager.save = JsonMapper.ToObject<Save>(Resources.Load<TextAsset>("json/beginnerSave").text);  //初始存档
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Load()
    {
        GameObject loadSlotsPrefab = Resources.Load("Prefabs/LoadSlots") as GameObject;
        GameObject canvas = GameObject.Find("Canvas");
        GameObject loadSlots = Instantiate(loadSlotsPrefab, canvas.transform.position, Quaternion.identity, canvas.transform);
        GameManager.focusStack.Add(loadSlots);
    }
}
