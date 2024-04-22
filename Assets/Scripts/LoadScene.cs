using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    void Start()
    {
    }

    void Update()
    {
    }

    public void LoadGame(string sceneName) {
//         Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(sceneName);
    }
}
