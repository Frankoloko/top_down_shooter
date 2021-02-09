using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    Sound sound;

    void Start()
    {
        sound = GameObject.Find("Sound").GetComponent<Sound>();

        HideAllImages();
        LoadUnlockedImages();
    }

    void HideAllImages()
    {
        // CONTROLS

        GameObject.Find("?_ARROWS").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_ARROWS").GetComponent<Image>().enabled = false;

        GameObject.Find("?_WASD").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_WASD").GetComponent<Image>().enabled = false;

        GameObject.Find("?_Q").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_Q").GetComponent<Image>().enabled = false;

        GameObject.Find("?_E").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_E").GetComponent<Image>().enabled = false;

        // OPTIONS

        GameObject.Find("o_1").GetComponent<Button>().interactable = false;
        GameObject.Find("?_1").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_1").GetComponent<Image>().enabled = false;
        
        GameObject.Find("o_2").GetComponent<Button>().interactable = false;
        GameObject.Find("?_2").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_2").GetComponent<Image>().enabled = false;

        GameObject.Find("o_3").GetComponent<Button>().interactable = false;
        GameObject.Find("?_3").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_3").GetComponent<Image>().enabled = false;

        GameObject.Find("o_4").GetComponent<Button>().interactable = false;
        GameObject.Find("?_4").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_4").GetComponent<Image>().enabled = false;

        GameObject.Find("o_5").GetComponent<Button>().interactable = false;
        GameObject.Find("?_5").GetComponent<Text>().enabled = true;
        GameObject.Find("Image_5").GetComponent<Image>().enabled = false;
    }

    void LoadUnlockedImages()
    {
        if (Settings.progress.q_Sprite) {
            GameObject.Find("?_Q").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_Q").GetComponent<Image>().enabled = true;
            GameObject.Find("Image_Q").GetComponent<Image>().sprite = Settings.progress.q_Sprite;
        }
        if (Settings.progress.e_Sprite) {
            GameObject.Find("?_E").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_E").GetComponent<Image>().enabled = true;
            GameObject.Find("Image_E").GetComponent<Image>().sprite = Settings.progress.e_Sprite;
        }
        if (Settings.progress.e_Shoot_FirstKill) {
            GameObject.Find("?_WASD").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_WASD").GetComponent<Image>().enabled = true;

            // GameObject.Find("o_1").GetComponent<Button>().interactable = true; // The player can't select Shoot as an ability
            GameObject.Find("?_1").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_1").GetComponent<Image>().enabled = true;
        }
        if (Settings.progress.e_Movement_FirstKill) {
            GameObject.Find("?_ARROWS").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_ARROWS").GetComponent<Image>().enabled = true;

            // GameObject.Find("o_2").GetComponent<Button>().interactable = true; // The player can't select Movement as an ability
            GameObject.Find("?_2").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_2").GetComponent<Image>().enabled = true;
        }
        if (Settings.progress.e_Divide_FirstKill) {
            GameObject.Find("o_3").GetComponent<Button>().interactable = true;
            GameObject.Find("?_3").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_3").GetComponent<Image>().enabled = true;
        }
        if (Settings.progress.e_Teleport_FirstKill) {
            GameObject.Find("o_4").GetComponent<Button>().interactable = true;
            GameObject.Find("?_4").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_4").GetComponent<Image>().enabled = true;
        }
    }

    void Update()
    {
        // Arrows: Move between abilities
        if (Input.GetKeyDown(KeyCode.LeftArrow) ^ Input.GetKeyDown(KeyCode.RightArrow) ^ Input.GetKeyDown(KeyCode.UpArrow) ^ Input.GetKeyDown(KeyCode.DownArrow)) {
            sound.PlaySelect();
        }

        // R: Restart game
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Game");
            Settings.ResetStatics();
        }

        // ESC: Quit game
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        // Q/E: Assign ability Q
        if (Input.GetKeyDown(KeyCode.Q) ^ Input.GetKeyDown(KeyCode.E)) {
            AssignAbilities();
        }
       
    }

    void AssignAbilities()
    {
        sound.PlaySelected();
        string selectedButtonName = EventSystem.current.currentSelectedGameObject.name;

        bool q_Pressed = Input.GetKeyDown(KeyCode.Q);
        bool e_Pressed = Input.GetKeyDown(KeyCode.E);

        // Saved the selected option
        if (selectedButtonName == "o_3") {
            if (q_Pressed) {
                Settings.progress.q_Ability = "Divide";
            }
            if (e_Pressed) {
                Settings.progress.e_Ability = "Divide";
            }
        }
        if (selectedButtonName == "o_4") {
            if (q_Pressed) {
                Settings.progress.q_Ability = "Teleport";
            }
            if (e_Pressed) {
                Settings.progress.e_Ability = "Teleport";
            }
        }
        
        // Reload the sprite images
        if (q_Pressed) {
            Settings.progress.q_Sprite = GameObject.Find(selectedButtonName.Replace("o", "Image")).GetComponent<Image>().sprite;
            GameObject.Find("?_Q").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_Q").GetComponent<Image>().enabled = true;
            GameObject.Find("Image_Q").GetComponent<Image>().sprite = Settings.progress.q_Sprite;
        }
        if (e_Pressed) {
            Settings.progress.e_Sprite = GameObject.Find(selectedButtonName.Replace("o", "Image")).GetComponent<Image>().sprite;
            GameObject.Find("?_E").GetComponent<Text>().enabled = false;
            GameObject.Find("Image_E").GetComponent<Image>().enabled = true;
            GameObject.Find("Image_E").GetComponent<Image>().sprite = Settings.progress.e_Sprite;
        }
    }
}
