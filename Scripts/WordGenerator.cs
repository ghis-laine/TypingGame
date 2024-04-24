using UnityEngine;
using System.Collections;

public class WordGenerator : MonoBehaviour
{

    public string[] words = {
        "États-Unis",
        "France",
        "Japon",
        "Brésil",
        "Australie"}; // Liste de mots
    public float minSpeed = 1f; // Vitesse minimale de chute
    public float maxSpeed = 3f; // Vitesse maximale de chute

    private Transform _transform;

    void Start()
    {
        _transform = transform;
        StartCoroutine(GenerateWords());
    }

    IEnumerator GenerateWords()
    {
        while (true)
        {
            // Définir la position et la vitesse d'un nouveau mot
            Vector3 position = new Vector3(Random.Range(-2f, 2f), 5f, 0f);
            float speed = Random.Range(minSpeed, maxSpeed);

            GameObject wordObject = new GameObject("Word");
            wordObject.transform.position = position;
            wordObject.transform.parent = _transform;

            // Ajouter le script WordMover au nouveau mot
            WordMover wordMover = wordObject.AddComponent<WordMover>();
            wordMover.speed = speed;

            // Définir le mot à afficher
            TextMesh textMesh = wordObject.GetComponentInChildren<TextMesh>();
            textMesh.text = words[Random.Range(0, words.Length)];

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
}
