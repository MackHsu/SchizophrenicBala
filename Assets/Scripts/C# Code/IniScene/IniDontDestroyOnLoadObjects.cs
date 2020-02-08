using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniDontDestroyOnLoadObjects : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameManager);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
