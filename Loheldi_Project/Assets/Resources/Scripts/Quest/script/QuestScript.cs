using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class QuestScript : MonoBehaviour
{
    public bool Quest = false;               //현재 받은 퀘스트 메일이 있는지 확인하는 함수 (MailDontDestroy 211참조)
    [SerializeField]

    public MailLoad Mail;
    public LodingTxt chat;

    public bool Draw=false;
    public Camera MainCamera;
    public Camera DrawCamera;
    public GameObject[] ExclamationMark;

    public bool note=false;
    public bool farm = false;

    private int QuestNum;
    public FlieChoice file;

    public QuestDontDestroy DontDestroy;

    
    public void QuestStart()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();

        if (PlayerPrefs.GetString("QuestPreg").Equals("0_0")) //튜토리얼
        {
            file.Tutorial();
            DontDestroy.QuestIndex = "0_1";
        }
        else if (PlayerPrefs.GetString("QuestPreg").Equals("0_1")) //밭 튜토리얼
        {
            StartCoroutine("QFarmLoop");
        }
        else if (DontDestroy.weekend&&PlayerPrefs.GetString("WeeklyQuestPreg").Equals("01_1")) //양치게임 해보기
        {
            Debug.Log("양치");
            DontDestroy.ToothQ = true;
        }
        else if (DontDestroy.LastDay != DontDestroy.ToDay)
        {
            QuestChoice();
        }

    }

    public void QuestChoice()
    {
        if (DontDestroy.QuestIndex.Equals("4_1"))  //힘찬이 옆에 자전거 만들기
                Instantiate(Resources.Load<GameObject>("Prefabs/Q/Qbicycle"), new Vector3(4.2f, -6.39158726f, -9.8f), Quaternion.Euler(0, 51.4773521f, 0));
        else if (DontDestroy.QuestIndex.Equals("8_1"))
        {
            note = true;
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (DontDestroy.QuestIndex.Equals("11_1")|| DontDestroy.QuestIndex.Equals("14_1")|| DontDestroy.QuestIndex.Equals("18_1")|| DontDestroy.QuestIndex.Equals("20_1")|| DontDestroy.QuestIndex.Equals("22_1"))
        {
            //캐릭터 플레이어 집 앞으로 데려오기
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(3.8f, -6.41383314f, -0.1f);
        }
        else if (DontDestroy.QuestIndex.Equals("13_1")) //이장님 감사나무 앞
        {
            note = true;
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(17.3f, -4.3f, 27.4f);
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 77, 0));
        }
        else if (DontDestroy.QuestIndex.Equals("19_1")) //힘찬이 옆에 나리가져다놓기
        {
            //GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position = new Vector3(125, 15, 170);
            GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation = Quaternion.Euler(new Vector3(0, 157, 0));

            GameObject NariIm = GameObject.Find("Nari");
            NariIm.transform.position = GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position+new Vector3(2, 0, -1);
            NariIm.transform.rotation = Quaternion.Euler(new Vector3(0, 207, 0));
        }
        else if (DontDestroy.QuestIndex.Equals("21_1")|| DontDestroy.QuestIndex.Equals("02_1")|| DontDestroy.QuestIndex.Equals("03_1")) //부모님 데려다 놓기
        {
            Instantiate(Resources.Load<GameObject>("Models/NPC/npc/parents"), new Vector3(-0.2f, -6.4f, 0), Quaternion.Euler(new Vector3(0, 133, 0)));
        }
        else if (DontDestroy.QuestIndex.Equals("24_1")) //힘찬이 훌라후프
        {
            chat.NPCHula.SetActive(true);
        }


        if (SceneManager.GetActiveScene().name == "MainField")
        {
            if (DontDestroy.QuestIndex.Equals("22_2"))
            {
                GameObject.Find(DontDestroy.ButtonPlusNpc).SetActive(false);
            }
            ExclamationMarkCreate();
        }
    }


    IEnumerator QFarmLoop()
    {
        while (true)
        {
            GameObject click = EventSystem.current.currentSelectedGameObject;
            if (click == null)
            { }
            else if (click.name.Equals("ItemBtn_"))
            {
                PlayerPrefs.SetString("QuestPreg", DontDestroy.QuestIndex);
                DontDestroy.ToDay = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
                PlayInfoManager.GetQuestPreg();
                farm = false;
                chat.QuestLoad.QuestLoadStart();
                StopCoroutine("QFarmLoop");
            }
            yield return null;
        }
    }
    private void ExclamationMarkCreate()
    {
        Debug.Log("퀘스트 느낌표 생성");
        Transform Parent = GameObject.Find(DontDestroy.ButtonPlusNpc).GetComponent<Transform>();
        GameObject child;
        child = Instantiate(ExclamationMark[1], GameObject.Find(DontDestroy.ButtonPlusNpc).transform.position+new Vector3(0,1.5f,0), GameObject.Find(DontDestroy.ButtonPlusNpc).transform.rotation);
        child.transform.parent = Parent;
        file.EPin.SetActive(true);
        file.EPin.GetComponent<MapPin>().Owner = GameObject.Find(DontDestroy.ButtonPlusNpc);

        GameObject[] clone = GameObject.FindGameObjectsWithTag("ExclamationMark");
        if(clone.Length >2)
            Destroy(clone[0]);
    }

    public void ChangeDrawCamera()
    {
        if (DrawCamera.enabled == false)
        {
            Draw=true;
            MainCamera.enabled = false;
            DrawCamera.enabled = true;
        }
        else
        {
            MainCamera.enabled = true;
            DrawCamera.enabled = false;
        }
    }
}
