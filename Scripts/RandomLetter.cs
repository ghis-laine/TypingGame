using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RandomLetter : MonoBehaviour
{
    public int totalLetters = 30; // Le nombre total de lettres à générer
    public float timeBetweenLetters = 2.0f; // Le temps entre chaque lettre générée (en secondes)
    public TMP_Text textObject; // L'objet Text qui affichera les lettres
    public TMP_InputField inputField; // Le champ de saisie

    private string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    public List<char> lettersGenerated = new List<char>(); // Liste des lettres générées

    void Start()
    {
        StartCoroutine(GenerateLetters());
    }

    private IEnumerator GenerateLetters()
    {
        int lettersGenerated = 0;

        while (lettersGenerated < totalLetters)
        {
            // Génération d'une lettre aléatoire
            int randomIndex = UnityEngine.Random.Range(0, alphabet.Length);
            string randomLetter = alphabet[randomIndex];

            // Ajout de la lettre au texte
            textObject.text += randomLetter;

            lettersGenerated++;

            // Attente avant de générer la prochaine lettre
            yield return new WaitForSecondsRealtime(timeBetweenLetters);
        }

        // Activation du champ de saisie
        inputField.gameObject.SetActive(true);
    }

    public void CheckInput()
    {
        string inputText = inputField.text;

        // Vérification de la saisie
        if (inputText.Length > 0)
        {
            char lastLetter = inputText[inputText.Length - 1];

            if (lettersGenerated.Contains(lastLetter))
            {
                // Suppression de la dernière lettre du texte et de la liste
                lettersGenerated.Remove(lastLetter);
                textObject.text = "";

                // Ajout d'un espace à la place de la lettre supprimée
                textObject.text += " ";

                // Appel de la fonction RemoveLetters
                RemoveLetters(inputText);
            }
        }
    }
    /*  else
      {
          Debug.Log("Saisie incorrecte. Veuillez réessayer.");
      }*/

    // Désactivation du champ de saisie
    // inputField.gameObject.SetActive(false);
    public void RemoveLetters(string inputText)
    {
        // Suppression des lettres du texte et de la liste
        for (int i = 0; i < inputText.Length; i++)
        {
            lettersGenerated.Remove(inputText[i]);
            textObject.text = "";

            // Ajout d'un espace à la place de la lettre supprimée
            textObject.text += " ";
        }
    }
}

   

