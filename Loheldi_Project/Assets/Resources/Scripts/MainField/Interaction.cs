using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public Text text;                               //점프버튼에 글자를 선언
    public GameObject JumpButton;
    public bool NearNPC = false;                    //NPC근처에 있는지 확인하는 함수 선언
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
    void OnTriggerEnter(Collider other)             //다른 콜리더와 부딛혔을때
    {
        BollFalse();
        if (other.gameObject.tag == "NPC")          //콜리더의 Tag가 NPC라면
        {
            if (other.gameObject.name == "WallMirror")
            {
                if (!trans.tranbool)
                    text.text = "거울";
                else
                    text.text = "Mirror";
            }
            else if (other.gameObject.name == "GachaMachine")
            {
                Gacha = true;
                if (!trans.tranbool)
                    text.text = "뽑기";
                else
                    text.text = "Gacha";
            }
            else if (other.gameObject.name == "ThankApplesTree")
            {
                ThankTree = true;
                if (!trans.tranbool)
                    text.text = "감사나무";
                else
                    text.text = "Tankree";
            }
            else if (other.gameObject.name == "Bibim" || other.gameObject.name == "Fruit" || other.gameObject.name == "Wood")
            {
                if (!trans.tranbool)
                    text.text = "미니게임";
                else
                    text.text = "MiniGame";
            }
            else if (other.gameObject.name == "InfoSign")
            {
                if (!trans.tranbool)
                    text.text = "정보";
                else
                    text.text = "Info";
            }
            else
            {
                ThankTree = false;
                if (!trans.tranbool)
                    text.text = "대화";
                else
                    text.text = "Talk";
            }
            NearNPC = true;
            NameNPC = other.gameObject.name.ToString();
            NpcNameActive(other.gameObject);
            //Debug.Log("NPC이름=" + NameNPC);
            
        }
        else if (other.gameObject.name == "change")          //콜리더의 name가 change라면 (하우징)
        {
            change = GameObject.Find("HousingSystem").GetComponent<ChangeMode>();
        }
        else if (other.gameObject.name == "InDoor")          //콜리더의 Tag가 InDoor라면
        {
            Door = true; 
            if (!trans.tranbool)
                text.text = "들어가기";
            else
                text.text = "in";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "ExitDoor")
        {
            JumpButton.SetActive(true);
            Door = true;
            if (!trans.tranbool)
                text.text = "나가기";
            else
                text.text = "Out";
            NameNPC = other.gameObject.name.ToString();
        }
        else if (other.gameObject.name == "Field")
        {
            Farm = true;
            if (!trans.tranbool)
                text.text = "농장";
            else
                text.text = "Farm";
        }
        else if(other.gameObject.tag == "Ladder")
        {
            
            Ladder = true;
            if (!trans.tranbool)
                text.text = "오르기";
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

    void OnTriggerExit(Collider other)              //다른 콜리더와 떨어졌을때
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
        if (other.gameObject.tag == "NPC" || other.gameObject.name == "InDoor" ||other.gameObject.tag == "Ladder"|| other.gameObject.name == "GachaMachine" || other.gameObject.name == "Field")          //콜리더의 Tag가 NPC라면
        {
            if (other.gameObject.tag == "NPC")
            {
                NpcNameTF = false;
            }

            BollFalse();
            if (!trans.tranbool)
                text.text = "점프";
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