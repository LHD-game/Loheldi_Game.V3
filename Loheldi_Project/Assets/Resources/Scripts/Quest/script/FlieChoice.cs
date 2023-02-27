using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class FlieChoice : MonoBehaviour
{
    public LodingTxt chat;
    public GameObject EPin;

    [SerializeField]
    private UIButton UI;
    public QuestDontDestroy QDD;
    public Interaction Inter;
    private void Awake()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        if (SceneManager.GetActiveScene().name == "Quiz")
        {
            Quest(); 
        }
    }
    public void Tutorial()
    {
        Debug.Log("Ʃ�丮��");
        //Save_Log.instance.SaveQStartLog();  //����Ʈ ���� �ð� �α�
        chat.Main_UI.SetActive(false);
        chat.FileAdress = "Scripts/Quest/script";
        chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/tutorial");
        chat.Num = "0_1";
        
        chat.NewChat();
    }
    public void Quest()
    {
        if (SceneManager.GetActiveScene().name == "MainField" || SceneManager.GetActiveScene().name == "AcornVillage")
        {
            EPin.SetActive(false);
            Inter.NpcNameTF = false;
            chat.cuttoonImageList = Resources.LoadAll<Sprite>("Sprites/Quest/cuttoon/Quest" + chat.DontDestroy.QuestIndex.Substring(0, chat.DontDestroy.QuestIndex.IndexOf("_")));
        }
        /*if (QDD.weekend) 
            chat.FileAdress = "Scripts/Quest/scriptWeekend";
        else*/
            chat.FileAdress = "Scripts/Quest/script";
        chat.Num = chat.DontDestroy.QuestIndex;
        chat.NewChat();
        if (SceneManager.GetActiveScene().name == "Quiz") ;
        else
            Save_Log.instance.SaveQStartLog();  //����Ʈ ���� �ð� �α�
    }

    public void NpcChoice(string NameNPC) //npc�� ��ȭ �����ϴ� �Լ�
    {
        chat.NPCButton = 0;
        chat.FileAdress = "Scripts/Quest/DialogNPC";

        string[] QuestF = chat.DontDestroy.QuestIndex.Split('_');
            switch (NameNPC)
        {
            case "Himchan":
                if (chat.DontDestroy.From.Equals("��õ�غ���")&& chat.DontDestroy.ButtonPlusNpc.Equals("Himchan"))
                    chat.Num = "14";
                else
                    chat.Num = "1";
                chat.NPCButton += 2;
                break;
            case "Markatman":
                chat.Num = "2";
                chat.NPCButton += 2;
                break;
            case "Hami":
                if ((QuestF[0].Equals("1")||QuestF[0].Equals("6")||QuestF[0].Equals("11")) && QuestF[1].Equals("2"))
                    chat.Num = "11";
                else if (chat.DontDestroy.From.Equals("��õ�غ���") && chat.DontDestroy.ButtonPlusNpc.Equals("Hami"))
                    chat.Num = "15";
                else
                    chat.Num = "3";
                chat.NPCButton += 1;
                break;
            case "Suho":
                if (QuestF[0].Equals("3") && QuestF[1].Equals("2"))
                    chat.Num = "12";
                else if (chat.DontDestroy.QuestIndex.Equals("13_1"))
                    chat.Num = "18";
                else if (chat.DontDestroy.From.Equals("��õ�غ���") && chat.DontDestroy.ButtonPlusNpc.Equals("Suho"))
                    chat.Num = "16";
                else
                    chat.Num = "4";
                chat.NPCButton += 1;
                break;
            case "Nari":
                chat.Num = "5";
                chat.NPCButton += 1;
                break;
            case "Mei":
                if (chat.DontDestroy.From.Equals("��õ�غ���") && chat.DontDestroy.ButtonPlusNpc.Equals("Mei"))
                    chat.Num = "17";
                else if (chat.DontDestroy.QuestIndex.Equals("8_1"))
                    chat.Num = "19";
                else
                chat.Num = "6";
                chat.NPCButton += 1;
                break;
            case "Yomi":
                chat.Num = "7";
                chat.NPCButton += 2;
                break;
            case "Yeomi":
                chat.Num = "8";
                chat.NPCButton += 2;
                break;
            case "Mu":
                chat.Num = "9";
                chat.NPCButton += 2;
                break;
            case "WallMirror":
                chat.Num = "10";
                chat.NPCButton += 3;
                break;
            case "parents(Clone)":
                chat.Num = "13";
                chat.NPCButton += 2;
                break;
            case "GachaMachine":
                chat.Num = "20";
                chat.NPCButton += 2;
                break;
            case "ThankApplesTree":
                int nowTime = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                if (UI.time == nowTime)
                { 
                    chat.Num = "22";
                    chat.NPCButton += 1;
                }
                else
                {
                    chat.Num = "21";
                    chat.NPCButton += 2;
                }
                break;
            case "Kangteagom":

                if (SceneManager.GetActiveScene().name == "AcornVillage")
                    chat.Num = "24";
                else
                    chat.Num = "23";
                chat.NPCButton += 2;
                break;
            case "Bibim":

                    chat.Num = "25";
                chat.NPCButton += 2;
                break;
            case "Wood":

                    chat.Num = "26";
                chat.NPCButton += 2;
                break;
            case "Fruit":

                    chat.Num = "27";
                chat.NPCButton += 2;
                break;
        }
        chat.NewChat();
        chat.Buttons();

    }
    
}
