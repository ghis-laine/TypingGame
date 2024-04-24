using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypingGame : MonoBehaviour
{ // Public fields for inspector configuration
    public TMP_InputField inputField;
    public TMP_Text targetText;
    public TMP_Text scoreText;
    public TMP_Text livesText; // Add public field for lives UI Text
    public TMP_Text timerText; // Add public field for timer UI Text
    public TMP_Text defeatScoreText;
    public TMP_Text victoryScoreText;
    public GameObject gameOverUI;
    public GameObject VictoryUI;
    public AudioSource typingSoundSource; // Reference to typing sound AudioSource
    public AudioSource victorySoundSource; // Reference to victory sound AudioSource
    public AudioSource defeatSoundSource; // Reference to defeat sound AudioSource
    public AudioSource errorSoundSource; // Reference to defeat sound AudioSource

    public float timeLimit = 30f; // Timer duration in seconds
    public int numLettersToGenerate = 10; // Default value of 10
    public int score = 0;
    public int maxLives = 3; // Maximum number of lives

    // Private fields
    private char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray(); // Array of characters (corrected)
    private List<char> lettersGenerated = new List<char>(); // List to store target characters
    private int lives = 3; // Current number of lives
    private float timer = 0f; // Current timer value

    // Start function for initialization
    public void Start()
    {
        GenerateLetters(); // Generate initial target characters
        UpdateTargetText(); // Display target characters
        UpdateScoreText(); // Initially update score display
        UpdateLivesText(); // Add initial lives display
        UpdateTimerText(); // Initialize timer display to 0
        gameOverUI.SetActive(false);
        //Start the timer
        timer = timeLimit;
        StartCoroutine(TimerCoroutine()); // Start a coroutine to update the timer
    }

    // Function to generate random target characters
   void GenerateLetters()
    {
        lettersGenerated.Clear(); // Clear existing characters

        // Generate 10 random characters
        for (int i = 0; i < numLettersToGenerate; i++)
        {
            int randomIndex = Random.Range(0, alphabet.Length);
            lettersGenerated.Add(alphabet[randomIndex]); // Add characters directly (corrected)
        }
    }

    // Function to update the displayed target text
    void UpdateTargetText()
    {
        string targetString = "";

        // Concatenate characters into a string for display
        foreach (char letter in lettersGenerated)
        {
            targetString += letter + " ";
        }

        targetText.text = targetString;
    }

    // Function to check user input
    public void CheckInput()
    {
        if (inputField.text.Length > 0) // Check if user entered a character
        {
            char inputLetter = inputField.text[0]; // Get the first character

            // Verify if the input matches the first target character
            if (lettersGenerated.Count > 0 && lettersGenerated[0] == inputLetter)
            {
                lettersGenerated.RemoveAt(0); // Remove the matched character
                UpdateTargetText(); // Update displayed text
                score++; // Increase score
                UpdateScoreText();

                typingSoundSource.Play();

                inputField.text = ""; // Clear the input field

                // Check if all target characters have been typed
                if (lettersGenerated.Count == 0)
                {
                    // Activate victory GameObject
                    VictoryUI.SetActive(true);
                    // Update score text in victory GameObject
                    victoryScoreText.text = "Score: " + score;
                    victorySoundSource.Play();



                    GenerateLetters(); // Generate new target characters
                    
                }
            }
            else
            {
                // Incorrect input handling
                lives--;
                UpdateLivesText();

                inputField.text = ""; // Clear the input field

                // Check if lives are depleted
                if (lives == 0)
                {
                    GameOver(); // Handle game over scenario
                }
                else
                {
                    // Play error sound ONLY when a typo is made
                    errorSoundSource.Play();

                    // ... (rest of the incorrect input handling)
                }

            }
        }
    }
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Set the score text
    }
    void UpdateLivesText()
    {
        // Add a UI Text element for lives display and assign it to livesText
        // ...
       

        livesText.text = "Vies: " + lives;
    }
    void UpdateTimerText()
    {
        int hours = (int)(timer / 3600f);
        int minutes = (int)((timer % 3600f) / 60f);
        int seconds = (int)(timer % 60f);

        string formattedTime;

        // Handle case for minutes less than one
        if (minutes == 0)
        {
            formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        timerText.text = formattedTime; // Remove "elapsedTime:"
    }

    // Coroutine to update the timer
    IEnumerator TimerCoroutine()
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            // Check if time is up
            if (timer <= 0f)
            {
                GameOver();
            }

            yield return null; // Wait for next frame
        }
    }
    public void ResetGame()
    {
        // Reset game state variables
        score = 0;
        lives = maxLives;
        timer = timeLimit;
       // gameStarted = false; // Reset game start flag

        // Clear existing target characters
        lettersGenerated.Clear();

        // Generate new target characters based on numLettersToGenerate
        GenerateLetters();

        // Update UI elements
        UpdateScoreText();
        UpdateLivesText();
        UpdateTimerText();
        UpdateTargetText(); // Update target text display

        // Reset input field
        inputField.text = "";

        // Hide victory/defeat UI
        gameOverUI.SetActive(false);
        VictoryUI.SetActive(false);

        // Reset typing sound
        typingSoundSource.Stop();

        // Reset error sound
        errorSoundSource.Stop();
    }

    // Function to handle game over
    void GameOver()
    {
       
            // Defeat scenario
            defeatSoundSource.Play();
            gameOverUI.SetActive(true);
            defeatScoreText.text = "Score: " + score;
        
        
    }
}