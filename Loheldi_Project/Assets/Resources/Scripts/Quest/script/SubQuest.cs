using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd;
using System;
using LitJson;
public class SubQuest : MonoBehaviour
{
    public GameObject Main_UI;
    public GameObject AppleTree;
    public GameObject AppleTreeOBJ;
    public TMP_InputField AppleTreeTxt;
    public GameObject ErrorWin;
    public Text ErrorWinTxt;
    public MainGameManager MainUI;
    public UIButton UI;

    [SerializeField]
    private ParticleSystem HeartFx;


    [SerializeField]
    Trans trans;

    public void AppleTreeQ()
    {
        if(AppleTreeTxt.text.Length<10)
        {
            ErrorWin.SetActive(true); 
            if (trans.tranbool)
            {
                ErrorWinTxt.text = "�����ߴ� ���� ���ݸ� �� �ڼ��� �������! \n <10���� �̻� �����ּ���>";
            }
            else
            {
                ErrorWinTxt.text = "Write down what you were thankful for in more detail! \n <Please write at least 10 letters>";
            }
        }
        else
        {
            AppleTreeSave();
        }
    }

    private void AppleTreeSave()
    {
        PlayInfoManager.GetExp(10);
        PlayInfoManager.GetCoin(10);
        MainUI.UpdateField();
        AppleTree.SetActive(false);
        Main_UI.SetActive(true);
        string BasicCSV = ChartNum.BasicCustomItemChart;

        BackendReturnObject BRO = Backend.Chart.GetChartContents(BasicCSV);

        UI.time = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
        if (BRO.IsSuccess())
        {
            Param param = new Param();  // �� ��ü ����

            param.Add("LastThankTreeTime", UI.time);    //��ü�� �� �߰�

            var bro = Backend.GameData.Get("USER_SUBQUEST", new Where());
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

            //�ش� row�� ���� update
            var bro2 = Backend.GameData.UpdateV2("USER_SUBQUEST", rowIndate, Backend.UserInDate, param);
            if (bro2.IsSuccess())
            {
                Debug.Log("SAVESUBQUEST ����. USER_SUBQUEST ������Ʈ �Ǿ����ϴ�.");
            }
            else
            {
                Debug.Log("SAVESUBQUEST ����.");
            }
        }
        Invoke("HeartFX", 0.15f);
    }

    public void HeartFX()    //���� ��Ʈ ��ƼŬ
    {
        Debug.Log("��¦");
        ParticleSystem newfx = Instantiate(HeartFx);
        newfx.transform.position = AppleTreeOBJ.transform.position + new Vector3(0, 7, -3);

        newfx.Play();
    }
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
