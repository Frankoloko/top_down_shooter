using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    // Update is called once per frame
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
        }
    }
}
