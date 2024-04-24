using UnityEngine;
using System.Collections;

public class WordMover : MonoBehaviour
{

    public float speed = 2f; // Vitesse de chute du mot
    private bool _isCorrect = false; // Indique si le mot a �t� tap� correctement

    void Update()
    {
        // D�placement du mot vers le bas
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // D�tecter les touches press�es
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (c == GetComponentInChildren<TextMesh>().text[0])
                {
                    _isCorrect = true;
                    Destroy(gameObject);
                    break;
                }
            }
        }

        // G�rer les erreurs de frappe
        if (transform.position.y < -5f && !_isCorrect)
        {
            // D�finir des actions en cas d'erreur (retrait de points, p�nalit� de temps, etc.)
            Debug.Log("Erreur de frappe !");
            Destroy(gameObject);
        }
    }
}
