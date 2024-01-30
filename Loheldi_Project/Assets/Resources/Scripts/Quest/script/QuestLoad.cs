using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLoad : MonoBehaviour
{
    public GameObject TempObject;

    //string QID;
    string QID2;
    string QID3;
    string QName;
    string From;
    string Content;
    string Reward;
    string authorName;
    string Type;
    public QuestScript Quest;
    public QuestDontDestroy DontDestroy;
    public QuestStatus QuestStatus;


    [SerializeField] Trans trans;

    public void QuestLoadStart()
    {
        DontDestroy = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        //Quest = GameObject.Find("chatManager").GetComponent<QuestScript>();

        string selectedProbabilityFileId = "";

        if (!trans.tranbool)
            selectedProbabilityFileId = "76254"; //����Ʈ ��Ʈ
        else
            selectedProbabilityFileId = "106927";

        var bro3 = Backend.Chart.GetChartContents(selectedProbabilityFileId);
        JsonData rows = bro3.GetReturnValuetoJSON()["rows"];

        var bro2 = Backend.GameData.GetMyData("QUEST_INFO", new Where());

        if (!bro3.IsSuccess())
        {
            Debug.LogError(bro3.ToString());
            return;
        }
        if (bro2.IsSuccess())
        {

            Param param2 = new Param();

            string QuestPreg;
            if (DontDestroy.ReQuest)
                QuestPreg = DontDestroy.QuestIndex;
            else
                QuestPreg = PlayerPrefs.GetString("QuestPreg");




            if (QuestPreg.Equals("0_0")) //���ǹ� �߰�(request�� ���� ����ǰ�)
            {
                QID2 = rows[0]["QID"]["S"].ToString();
                QName = rows[0]["QName"]["S"].ToString();
                From = rows[0]["From"]["S"].ToString();
                Content = rows[0]["Content"]["S"].ToString();
                Reward = rows[0]["Reward"]["S"].ToString();
                authorName = rows[0]["authorName"]["S"].ToString();
                Type = rows[0]["Type"]["S"].ToString();

                DontDestroy.QuestIndex = QID2;
                PlayerPrefs.SetString("NowQID", QID2);
                DontDestroy.ButtonPlusNpc = authorName;
                DontDestroy.From = From;

                //Debug.Log(DontDestroy.QuestIndex);
                //Debug.Log(QID2);
                //Debug.Log(QName);

                //�̹� ����Ʈ�� ���ִ��� �˻�
                Where where = new Where();
                where.Equal("QID", QID2);
                var chk_bro = Backend.GameData.GetMyData("QUEST_INFO", where);
                JsonData chk_rows = chk_bro.GetReturnValuetoJSON()["rows"];

                if (chk_rows.Count <= 0)
                {
                    Param param = new Param();
                    param.Add("QID", QID2);
                    param.Add("QName", QName);
                    param.Add("From", From);
                    param.Add("Content", Content);
                    param.Add("Reward", Reward);
                    param.Add("authorName", authorName);
                    param.Add("Type", Type);
                    Backend.GameData.Insert("QUEST_INFO", param);
                    Debug.Log("����Ʈ ���� �Ϸ�");
                }
                else
                {
                    Debug.Log("�̹� �ش� ����Ʈ�� �����߽��ϴ�.");
                }
            }
            else
            {
                //Debug.Log("���Ϲޱ�");
                int r;
                for (int i = 0; i < rows.Count; i++)
                {
                    //Debug.Log("���Ϲޱ�"+" i ="+i);
                    if (DontDestroy.ReQuest && !DontDestroy.QuestNF)
                        r = i;
                    else
                        r = i + 1;
                    string QID = rows[i]["QID"]["S"].ToString();
                    //Debug.Log("���Ϲޱ�" + " QID =" + QID + " QuestP = " + QuestPreg);
                    if (QID == QuestPreg)   //0_0�� �ƴ� ���¿��� ����Ʈ ���൵�� ��ġ
                    {
                        QuestStatus.QuestStepNumber = i;
                        //Debug.Log("���Ϲޱ�" + " i =" + i + " r = " + r);
                        QName = rows[r]["QName"]["S"].ToString();
                        if (QName == "end")
                        {
                            Debug.Log("������ ����Ʈ�Դϴ�");
                            DontDestroy.ReQuest = true;
                            DontDestroy.QuestIndex = PlayerPrefs.GetString("QuestPreg");
                            return;
                        }
                        /*if (DontDestroy.SDA)
                            return;*/
                        else if (DontDestroy.ToDay != DontDestroy.LastDay)
                        {
                            QID3 = rows[r]["QID"]["S"].ToString();
                            //QName = rows[r]["QName"]["S"].ToString();
                            From = rows[r]["From"]["S"].ToString();
                            Content = rows[r]["Content"]["S"].ToString();  //replace�� �����Կ��� �����ϴ� ���� �����ŵ� �����մϴ�.
                            Reward = rows[r]["Reward"]["S"].ToString();
                            authorName = rows[r]["authorName"]["S"].ToString();
                            if (DontDestroy.ReQuest)
                                Type = "ReQuest";
                            else
                                Type = rows[0]["Type"]["S"].ToString();

                            DontDestroy.QuestIndex = QID3;
                            PlayerPrefs.SetString("NowQID", QID3);
                            DontDestroy.ButtonPlusNpc = authorName;
                            DontDestroy.From = From;
                            //Debug.Log(QID3);
                            //Debug.Log(QName);
                            //Debug.Log(DontDestroy.QuestIndex);

                            //�̹� ����Ʈ�� ���ִ��� �˻�
                            Where where = new Where();
                            where.Equal("QID", QID3);
                            var chk_bro = Backend.GameData.GetMyData("QUEST_INFO", where);
                            JsonData chk_rows = chk_bro.GetReturnValuetoJSON()["rows"];
                            if (chk_rows.Count <= 0)
                            {
                                param2.Add("QID", QID3);
                                param2.Add("QName", QName);
                                param2.Add("From", From);
                                param2.Add("Content", Content);
                                param2.Add("Reward", Reward);
                                param2.Add("authorName", authorName);
                                param2.Add("Type", Type);
                                Backend.GameData.Insert("QUEST_INFO", param2);
                                Debug.Log("����Ʈ ���� �Ϸ�");
                                break;
                            }
                            else
                            {
                                Debug.Log("�̹� �ش� ����Ʈ�� �����߽��ϴ�.");
                            }
                        }
                    }
                    //Debug.Log("Type:" + Type);
                }
            }
            Quest.QuestStart();
        }
    }
}
