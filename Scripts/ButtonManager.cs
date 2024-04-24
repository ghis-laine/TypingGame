using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject game; // Reference to the game GameObject (replace with "Game" if that's the actual name)
    public GameObject defeatPage;
    public GameObject victoryPage;

    private TypingGame typingGame; // Reference to the TypingGame script

    void Start()
    {
        game.SetActive(true);
        defeatPage.SetActive(false);
        victoryPage.SetActive(false);

        // Find the TypingGame script in the scene
        typingGame = FindObjectOfType<TypingGame>(); // Assuming TypingGame is a unique script in the scene
    }

    public void Replay()
    {
        // Ensure the TypingGame script is found
        if (typingGame != null)
        {
            // Reset the game state in TypingGame script
            typingGame.ResetGame(); // Call a ResetGame method in TypingGame to restart the game

            // Show the game and hide victory/defeat pages
            game.SetActive(true);
            defeatPage.SetActive(false);
            victoryPage.SetActive(false);
        }
        else
        {
            Debug.LogError("TypingGame script not found. Restart functionality may not work.");
        }
    }

    public void GoToMenu()
    {
        SceneTransition.singleton.GoToSceneAsync(0); // Assuming SceneTransition exists and handles scene loading
    }
    public void Quit()
    {
        Application.Quit();
    }
}
