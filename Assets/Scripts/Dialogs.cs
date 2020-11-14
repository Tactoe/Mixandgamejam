using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogs : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public float delay;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadDialog());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && textDisplay.text.Length == sentences[index].Length)
        {
            NextSentence();
            Debug.Log("" + textDisplay.text.Length + " " +  sentences[index].Length);
        }
        
    }

    void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(ReadDialog());
        }
    }

    IEnumerator ReadDialog()
    {
        textDisplay.text = "";
        foreach (char c in sentences[index].ToCharArray())
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(delay);
        }

    }
}
