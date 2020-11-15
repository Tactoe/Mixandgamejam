using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InterfaceManager : MonoBehaviour
{
    public bool inDialogue;

    public static InterfaceManager instance;
    public CanvasGroup dialogueCG, questCG;
    public TMP_Animated animatedText;
    public Image nameBubble;
    public TextMeshProUGUI nameTMP, questTMP, finishedQuestTMP;

    public DialogueData currentDialogue;

    public GameObject gameCam;
    public GameObject dialogueCam;

    public Villager currentVillager;
    private int dialogueIndex;
    private string targetVillager;
    private bool questOngoing;
    private int questsDone;
    public bool canExit;
    public bool nextDialogue;
    bool isInCutscene;

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
            isInCutscene = true;
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
                CloseDialogue();
            }
            else if (nextDialogue)
            {
                animatedText.ReadText(currentDialogue.conversationBlock[dialogueIndex]);
            }
        }
    }

    void CloseDialogue()
    {
        dialogueCG.alpha = 0;
        if (isInCutscene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            CheckQuestStatus();
            Invoke("ResetState", 0.2f);
        }
    }

    public void StartInteraction(Villager v)
    {
        SetDialogueData(v);
        ClearText();
        StartDialogue();
    }

    public void CheckQuestStatus()
    {
        if (questOngoing)
        {
            EndQuest();
        }
        else
        {
            StartQuest();
        }
    }

    public void StartQuest()
    {
        questOngoing = true;
        List<GameObject> villagers = new List<GameObject>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Villager"))
        {
            if (g != currentVillager.gameObject)
                villagers.Add(g);
        }
        Villager v = villagers[Random.Range(0, villagers.Count)].GetComponent<Villager>();
        questCG.alpha = 1;
        targetVillager = v.data.name;
        questTMP.text = "Bring stuff to <color=red>" + targetVillager + "</color>";
    }

    public void EndQuest()
    {
        if (targetVillager == currentVillager.data.name)
        {
            questsDone++;
            finishedQuestTMP.text = "Errands completed: " + questsDone;
            questOngoing = false;
            targetVillager = "";
            questCG.alpha = 0;
        }
    }

    // ========== DIALOGUE UI =================

    public void StartDialogue()
    {
        inDialogue = true;
        dialogueCG.alpha = 1;
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
        currentVillager = v;
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
