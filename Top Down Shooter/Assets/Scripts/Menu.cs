using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    Sound sound;

    void Start()
    {
        sound = GameObject.Find("Sound").GetComponent<Sound>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Game");
            BalancingSettings.ResetStatics();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            // print(EventSystem.currentSelectedGameObject);
            print(EventSystem.current.currentSelectedGameObject.name == "o_Shoot");
            sound.PlaySelected();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) ^ Input.GetKeyDown(KeyCode.RightArrow) ^ Input.GetKeyDown(KeyCode.UpArrow) ^ Input.GetKeyDown(KeyCode.DownArrow)) {
            sound.PlaySelect();
        }
    }
}
