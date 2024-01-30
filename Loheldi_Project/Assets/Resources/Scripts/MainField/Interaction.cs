using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Text text;                               //������ư�� ���ڸ� ����
    public GameObject JumpButton;
    public bool NearNPC = false;                    //NPC��ó�� �ִ��� Ȯ���ϴ� �Լ� ����
    public string NameNPC;
    public bool Door;
    public bool Gacha;
    public bool Farm = false;
    public bool ThankTree = false;
    public bool Ladder = false;

    public bool NpcNameTF = false;
    public List<string> Npcs = new List<string>();
    public GameObject[] NpcNames;

    public GameObject FarmingMaster;

    public Camera MainCam;
    public Camera TCam;
    public VirtualJoystick VJS;
    public FurnitureChangeClick FCC;
    public HousingElevator HE;


    private ChangeMode change;

    [SerializeField] Trans trans;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Housing")
        {
            MainCam.enabled = true;
            TCam.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)             //�ٸ� �ݸ����� �ε�������
    {
        BollFalse();
        if (other.gameObject.tag == "NPC")          //�ݸ����� Tag�� NPC���
        {
            if (other.gameObject.name == "WallMirror")
            {
                if (!trans.tranbool)
                    text.text = "�ſ�";
                else
                    text.text = "Mirror";
            }
            else if (other.gameObject.name == "GachaMachine")
            {
                Gacha = true;
                if (!trans.tranbool)
                    text.text = "�̱�";
                else
                    text.text = "Gacha";
            }
            else if (other.gameObject.name == "ThankApplesTree")
            {
                ThankTree = true;
                if (!trans.tranbool)
                    text.text = "���糪��";
                else
                    text.text = "Tankree";
            }
            else if (other.gameObject.name == "Bibim" || other.gameObject.name == "Fruit" || other.gameObject.name == "Wood")
            {
                if (!trans.tranbool)
                    text.text = "�̴ϰ���";
                else
                    text.text = "MiniGame";
            }
            else if (other.gameObject.name == "InfoSign")
            {
                if (!trans.tranbool)
                    text.text = "����";
                else
                    text.text = "Info";
            }
            else
            {
                ThankTree = false;
                if (!trans.tranbool)
                    text.text = "��ȭ";
                else
                    text.text = "Talk";
            }
            NearNPC = true;
            NameNPC = other.gameObject.name.ToString();
            NpcNameActive(other.gameObject);
            //Debug.Log("NPC�̸�=" + NameNPC);
            
        }
        else if (other.gameObject.name == "change")          //�ݸ����� name�� change��� (�Ͽ�¡)
        {
            change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
        }
        else if (other.gameObject.name == "InDoor")          //�ݸ����� Tag�� InDoor���
        {
            Door = true; 
            if (!trans.tranbool)
                text.text = "����";
            else
                text.text = "in";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(true);
            Door = true;
            if (!trans.tranbool)
                text.text = "������";
            else
                text.text = "Out";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "Field")
        {
            Farm = true;
            if (!trans.tranbool)
                text.text = "����";
            else
                text.text = "Farm";
        }
        else if(other.gameObject.tag == "Ladder")
        {
            
            Ladder = true;
            if (!trans.tranbool)
                text.text = "������";
            else
                text.text = "Up";
        }
    }
    void BollFalse()
    {
        Door = false;
        Gacha = false;
        Farm = false;
        ThankTree = false;
        NearNPC = false;
        Ladder = false;
    }

    void OnTriggerExit(Collider other)              //�ٸ� �ݸ����� ����������
    {
        if (SceneManager.GetActiveScene().name == "Housing")
        {
            if (other.gameObject.name == "ExitDoor")
            {
                JumpButton.SetActive(false);
            }
            else if(other.gameObject.name == "Line")
            {
                if(this.transform.position.z<4.5f)
                {
                    MainCam.enabled = true;
                    TCam.enabled = false;
                    VJS.TempInt = 1;
                    FCC.getCamera = MainCam;
                }
                else
                {
                    MainCam.enabled = false;
                    TCam.enabled = true;
                    VJS.TempInt = 2;
                    FCC.getCamera = TCam;
                }
            }
        }
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" ||other.gameObject.tag == "Ladder"|| other.gameObject.name == "GachaMachine" || other.gameObject.name == "Field")          //�ݸ����� Tag�� NPC���
        {
            if (other.gameObject.tag == "NPC")
            {
                NpcNameTF = false;
            }

            BollFalse();
            if (!trans.tranbool)
                text.text = "����";
            else text.text = "Jump";
            NameNPC = " ";
        }
        
    }
    public void NpcNameActive(GameObject other)
    {
        if (NameNPC == "ThankApplesTree" || NameNPC == "parents(Clone)") return;
        else
        {
            int NpcNum = Npcs.IndexOf(NameNPC);
            NpcNameTF = true;
            NpcNames[NpcNum].SetActive(true);
            //StartCoroutine(NpcNameFollow(other, NpcNum));
        }
    }
}