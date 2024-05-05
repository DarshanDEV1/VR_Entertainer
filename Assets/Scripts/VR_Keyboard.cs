using UnityEngine;
using UnityEngine.UI;
using Google.XR.Cardboard;
using TMPro;
using System.Collections;

public class VR_Keyboard : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab; // Prefab for each key
    [SerializeField] private float keySize = 0.5f; // Size of each key
    [SerializeField] private float keySpacing = 0.1f; // Spacing between keys
    [SerializeField] private Camera _mainCamera;

    [SerializeField] internal TMP_Text outputText; // Reference to the UI Text element

    private GameObject[,] keys; // 2D array to store key GameObjects
    private string currentText = ""; // String to store the text entered

    private bool _key_press = false;

    private void Start()
    {
        // Create a 3D keyboard layout
        keys = new GameObject[3, 10]; // Adjust the dimensions here if needed
        for (int row = 0; row < 3; row++) // Adjust the row count here if needed
        {
            for (int col = 0; col < 10; col++) // Adjust the column count here if needed
            {
                // Instantiate a new key GameObject
                GameObject key = Instantiate(keyPrefab, transform.position + new Vector3(col * (keySize + keySpacing),
                    0, row * -(keySize + keySpacing)), Quaternion.Euler(90, 0, 0), transform);
                key.transform.localScale = new Vector3(keySize, keySize, keySize);
                keys[row, col] = key;

                // Assign a character to the key based on its position
                char character = GetCharacterForPosition(row, col);
                key.name = character.ToString();
                TextMesh textMesh = key.GetComponentInChildren<TextMesh>();
                if (textMesh == null)
                {
                    textMesh = key.AddComponent<TextMesh>();
                }
                textMesh.text = character.ToString();
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.color = Color.black;
                textMesh.characterSize = 0.2f;
            }
        }
    }

    private void Update()
    {
        // Check for gaze interaction with keys
        if ((Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetButtonDown("Fire1")) && !_key_press)
        {
            StartCoroutine(KeyPress());
            RaycastHit hit;
            Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Key"))
                {
                    // Append the character of the hit key to the current text
                    currentText += hitObject.transform.parent.name;

                    // Update the UI Text element with the current text
                    outputText.text = currentText;

                }
            }
        }
    }

    private IEnumerator KeyPress()
    {
        _key_press = true;
        yield return new WaitForSeconds(.5f);
        _key_press = false;
        StopCoroutine(KeyPress());
    }

    private char GetCharacterForPosition(int row, int col)
    {
        // Define the character layout for the keyboard
        string[] rowChars = { "qwertyuiop", "asdfghjkl", "zxcvbnm" };

        // Check if the row and column indices are valid
        if (row >= 0 && row < rowChars.Length && col >= 0 && col < rowChars[row].Length)
        {
            return rowChars[row][col];
        }
        else
        {
            // Return a default character if the indices are out of bounds
            return ' ';
        }
    }
}