using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedBar : MonoBehaviour
{
    List<float> needs = new List<float>();
    public float[] needDecreaseRate ;
    List<Image> needBars = new List<Image>();
    List<float> maxSize = new List<float>();
    //public MovementInput mi;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Image img in GetComponentsInChildren<Image>())
        {
            if (img.CompareTag("NeedBar"))
            {
                needs.Add(100);
                needBars.Add(img);
                maxSize.Add(img.rectTransform.sizeDelta.x);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < needBars.Count; i++)
        {
            needs[i] -= Time.deltaTime * needDecreaseRate[i];
            needs[i] = Mathf.Clamp(needs[i], 0, 100);
            Vector3 tmp = needBars[i].rectTransform.sizeDelta;
            tmp.x = maxSize[i] / 100 * needs[i];
            needBars[i].rectTransform.sizeDelta = tmp;
        }
    }

    public float getNeedCap()
    {
        float needCap = 0;
        foreach(float n in needs)
        {
            needCap += n;
        }
        return needCap / needs.Count;
    }

    public void RefillNeed(string need, float value)
    {
        int needIndex = 0;
        if (need == "thirst")
            needIndex = 0;
        if (need == "hunger")
            needIndex = 1;
        if (need == "fun")
            needIndex = 2;
        if (need == "affection")
            needIndex = 3;
        needs[needIndex] += value;
    }
}
