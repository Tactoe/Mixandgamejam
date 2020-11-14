using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Villager : MonoBehaviour
{
    public VillagerData data;
    public DialogueData dialogues;

    private TMP_Animated animatedText;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatedText = InterfaceManager.instance.animatedText;
        animatedText.onEmotionChange.AddListener((newEmotion) => EmotionChanger(newEmotion));
    }

    public void EmotionChanger(Emotion e)
    {

        animator.SetTrigger(e.ToString());
    }

    public void SetAction(string action)
    {
        if (action == "shake")
        {
            Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
        else
        {
        }
    }

    public void PlayParticle(string p)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
