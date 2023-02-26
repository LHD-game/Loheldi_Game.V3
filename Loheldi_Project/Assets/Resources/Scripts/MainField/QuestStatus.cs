using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestStatus : MonoBehaviour
{
    public int QuestStepNumber;
    public GameObject[] QuestButtons;
    public GameObject PImag;
    public GameObject ButtonParent;
    GameObject[] ButtonParents;
    public ScrollRect Qsr;
    GameObject child;

    QuestDontDestroy QDD;
    // Start is called before the first frame update
    void Start()
    {
        //QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        GetButtons();
    }
    void GetButtons()
    {
        Debug.Log("¹öÆ°µé °¡Á®¿À±â »þ¶ó¶ó¶ó¶ö¶ö¶ó");
        //GameObject ButtonParent = GameObject.Find("QuestContent");
        //GameObject[] ButtonParents = new GameObject[QuestButtons.Length];
        ButtonParents = new GameObject[QuestButtons.Length];
        for (int i = 0; i < QuestButtons.Length; i++)
        {
            ButtonParents[i] = ButtonParent.transform.GetChild(i).gameObject;
        }

        Debug.Log("ButtonL = " + QuestButtons.Length);
        Debug.Log("ButtonParentsL = " + ButtonParents.Length);
        int j = 0;
        for (int i = QuestButtons.Length - 1; i > -1; i--)
        {
            Debug.Log("ButtonN = " + i);
            QuestButtons[j] = ButtonParents[i].transform.GetChild(0).gameObject;
            j++;
        }
    }

    public void PlayerStepCheck()
    {
        Debug.Log("QuestStepNumber = " + QuestStepNumber);
        child = Instantiate(PImag, QuestButtons[QuestStepNumber].transform.position, Quaternion.Euler(0, 0, 0), GameObject.Find("Canvas").transform);
        child.transform.parent = QuestButtons[QuestStepNumber].GetComponent<Transform>();

        Debug.Log("child = " + child.transform.position.x);
        float QFx = child.transform.position.x - 1500;
        Vector3 QF = new Vector3(-QFx, Qsr.content.localPosition.y, 0);

        Debug.Log("child = " + QF);
        Qsr.content.localPosition = QF;
        //QuestButtons[QuestStepNumber];
    }
    public void ResetQS()
    {
        Destroy(child);
        Qsr.content.localPosition = new Vector3(0, Qsr.content.localPosition.y, 0); ;
    }
}
