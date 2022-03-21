using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player�� �ٴڿ� �ִ��� Ȯ��
    public GameObject Player;             //Player����
    public GameObject Map;                //Map����
    public GameObject Inv;
    public GameObject ConditionWindow;                //Map����
    public Rigidbody Playerrb;            //Player�� Rigidbody����
    public Text conditionLevelText;            //����â ����

    public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��
    bool inv;
   public static bool conditionWindow;      //����â�� �����ִ��� Ȯ��


    private void Awake()
    {
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge");

        while (ChangColor.h < 2)
        {
            ChangColor.badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/imgList/");
            ChangColor.h++;
        }

        ConditionWindow.SetActive(false);
        conditionWindow = false;
    }

    void Start()
    {
        map = false;
        conditionWindow = false;
    }

    public void JumpButton()                //������ư
    {
        if (Player.GetComponent<Interaction>().NearNPC)     //NPC�ֺ��� �ִٸ�
        {
            ShopMok.SetActive(true);
        }
        else                                                //NPC�ֺ��� ���� �ʴٸ�
        {
            if (OnLand)                                         //Player�� �ٴڿ� �ִٸ�
            {
                Playerrb.AddForce(transform.up * 15000);
                OnLand = false;
                MainGameManager.exp = MainGameManager.exp + 100;
            }
        }
    }

    public void MapButton()                 //������ư
    {
        if (map)                                            //������ �����ִٸ�
        {
            Map.SetActive(false);
            map = false;
        }
        else                                                //������ �����ִٸ�
        {
            Map.SetActive(true);
            map = true;
        }
    }

    public void InvButton()
    {
        if (inv)
        {
            Inv.SetActive(false);
            map = false;
        }
        else
        {
            Inv.SetActive(true);
            map = true;
        }
    }

    public  void ConditionButton()                 //����â��ư
    {
        if (conditionWindow)                                            //����â�� �����ִٸ�
        {
            ConditionWindow.SetActive(false);
            conditionWindow = false;
        }
        else                                                //����â�� �����ִٸ�
        {
            ConditionWindow.SetActive(true);
            conditionWindow = true;
            conditionLevelText.text = MainGameManager.level.ToString();
        }
    }
}