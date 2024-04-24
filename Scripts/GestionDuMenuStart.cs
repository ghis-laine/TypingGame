using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class GestionDuMenuStart : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject Setting;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button settingButton;
    public Button aboutButton;
    public Button quitButton;

    public List<Button> returnButtons;
    void Start()
    {
        MenuActivé();

        //Hook events
        startButton.onClick.AddListener(StartGame);
        settingButton.onClick.AddListener(OptionActivé);
        aboutButton.onClick.AddListener(AboutActivé);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(MenuActivé);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        HideAll();
        SceneTransition.singleton.GoToSceneAsync(1);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        Setting.SetActive(false);
        about.SetActive(false);
    }
    public void MenuActivé()
    {
        mainMenu.SetActive(true);
        Setting.SetActive(false);
        about.SetActive(false);
    }
    public void OptionActivé()
    {
        mainMenu.SetActive(false);
        Setting.SetActive(true);
        about.SetActive(false);
    }
    public void AboutActivé()
    {
        mainMenu.SetActive(false);
        Setting.SetActive(false);
        about.SetActive(true);
    }

}
