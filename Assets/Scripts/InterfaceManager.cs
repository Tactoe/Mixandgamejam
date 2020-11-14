using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public bool inDialogue;

    public static InterfaceManager instance;
    public CanvasGroup canvasGroup;
    public TMP_Animated animatedText;
    public Image nameBubble;
    public TextMeshProUGUI nameTMP;

    public DialogueData currentDialogue;

    public GameObject gameCam;
    public GameObject dialogueCam;

    private int dialogueIndex;
    public bool canExit;
    public bool nextDialogue;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animatedText.onDialogueFinish.AddListener(() => FinishDialogue());
        if (currentDialogue != null)
        {
            StartDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inDialogue)
        {
            if (animatedText.isReadingDialogue)
            {
                animatedText.isReadingDialogue = false;
            }
            else if (canExit)
            {
                canvasGroup.alpha = 0;
                Invoke("ResetState", 0.2f);
            }
            else if (nextDialogue)
            {
                animatedText.ReadText(currentDialogue.conversationBlock[dialogueIndex]);
            }
        }
    }

    public void StartDialogue()
    {
        inDialogue = true;
        canvasGroup.alpha = 1;
        dialogueIndex = 0;
        animatedText.ReadText(currentDialogue.conversationBlock[0]);
    }

    public void ClearText()
    {
        animatedText.text = string.Empty;
    }

    public void SetDialogueData(Villager v)
    {
        if (v.data.villagerName != "")
        {
            nameTMP.text = v.data.villagerName;
            nameTMP.color = v.data.villagerNameColor;
            nameBubble.color = v.data.villagerColor;
        }
        currentDialogue = v.dialogues;
    }

    public void ResetState()
    {
        //currentVillager.Reset();
        FindObjectOfType<MoveBehaviour>().canMove = true;
        canExit = false;
        inDialogue = false;

    }

    public void FinishDialogue()
    {
        if (dialogueIndex < currentDialogue.conversationBlock.Count - 1)
        {
            dialogueIndex++;
            nextDialogue = true;
        }
        else
        {
            nextDialogue = false;
            canExit = true;
        }
    }
}
