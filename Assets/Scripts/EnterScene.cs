using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour {

    public string to_level = "";

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(NextLevel);
    }

    void NextLevel()
    {
        SceneManager.LoadScene(to_level);
    }
}
