using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public Button newGameButton;
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(Scene1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Scene1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
