using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject[] characters;
    private int _charIndex;
    public int CharIndex
    {
        get { return _charIndex;  }
        set { _charIndex = value; }
    }

    // Start is called before the first frame update

    private void Awake() // singleton pattern
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // gameObject that holding this script
        }
        else
        {
            Destroy(gameObject); // destroy the duplicate

        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name == "Gameplay")
        {
            Instantiate(characters[CharIndex]);
        }
    }
}
