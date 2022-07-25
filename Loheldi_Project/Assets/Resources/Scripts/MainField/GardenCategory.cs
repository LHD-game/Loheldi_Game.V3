using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenCategory : MonoBehaviour
{
    List<Dictionary<string, object>> seedItem = new List<Dictionary<string, object>>();

    JsonData myInven_rows = new JsonData();
    static List<GameObject> seed_list = new List<GameObject>();   //�κ��丮 �������� �����ϴ� ����

    public void PopGarden(GameObject c_seed)
    {
        seedItem.Clear();

        for(int i=0; i < seed_list.Count; i++)
        {
            Destroy(seed_list[i]);
        }
        seedItem = new List<Dictionary<string, object>>();

        GetChartContents(ChartNum.AllItemChart);
        MakeCategory(c_seed, seedItem, seed_list);
    }

    void GetChartContents(string itemChart)  //��ü ������ ��ϰ� ���� ������ ����� �����´�.
    {
        var allItemChart = Backend.Chart.GetChartContents(itemChart); //������ ���������� �ҷ��´�.
        var myInven = Backend.GameData.GetMyData("INVENTORY", new Where(), 100);

        JsonData allItem_rows = allItemChart.GetReturnValuetoJSON()["rows"];
        myInven_rows = myInven.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();

        int s = 0;

        for (int i = 0; i < allItem_rows.Count; i++)
        {
            StoreItem data = pj.ParseBackendData<StoreItem>(allItem_rows[i]);

            //������ �׸��� ���� �ٸ� ����Ʈ�� ����.

            if (data.Category.Equals("seed"))   //���� ������
            {
                seedItem.Add(new Dictionary<string, object>()); // list�� ������ ������ݴϴ�.
                initItem(seedItem[s], data);
                s++;
            }
        }
    }

    GameObject itemBtn;

    //---init list---//
    //itemTheme ���� ��Ƽ� ����
    protected void initItem(Dictionary<string, object> item, StoreItem data)
    {
        item.Add("ICode", data.ICode);
        item.Add("IName", data.IName);
        item.Add("Category", data.Category);
        item.Add("ItemType", data.ItemType);
    }

    protected void MakeCategory(GameObject category, List<Dictionary<string, object>> dialog, List<GameObject> itemObject)
    {
        itemBtn = (GameObject)Resources.Load("Prefabs/UI/InvenItemforFarming");
        ParsingJSON pj = new ParsingJSON();

        for (int i = 0; i < dialog.Count; i++)
        {
            GameObject child = Instantiate(itemBtn);    //create itemBtn instance
            child.transform.SetParent(category.transform);  //move instance: child
                                                            //������ �ڽ� ũ�� �缳��
            RectTransform rt = child.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1f, 1f, 1f);

            itemObject.Add(child);


            GameObject ItemBtn = child.transform.Find("ItemBtn").gameObject;

            //change catalog box img
            GameObject item_img = ItemBtn.transform.Find("ItemImg").gameObject;
            Image img = item_img.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/Catalog_Images/Store/" + dialog[i]["ICode"] + "_catalog");


            //change catalog box item name (���ý� �ش� �������� ã�� ���� ����ǥ �뵵)
            GameObject item_name = ItemBtn.transform.Find("ItemName").gameObject;
            Text txt = item_name.GetComponent<Text>();
            txt.text = dialog[i]["IName"].ToString();

            //change catalog box item code
            GameObject item_code = ItemBtn.transform.Find("ItemCode").gameObject;
            Text item_code_txt = item_code.GetComponent<Text>();
            item_code_txt.text = dialog[i]["ICode"].ToString();

            GameObject disable_img = child.transform.Find("Disable").gameObject;
            disable_img.SetActive(true);
            for (int j = 0; j < myInven_rows.Count; j++)
            {
                MyItem data = pj.ParseBackendData<MyItem>(myInven_rows[j]);
                if (data.ICode.Equals(dialog[i]["ICode"].ToString()))
                {
                    //��Ȱ�� â ������Ʈ(Disable)�� ��Ȱ��ȭ
                    disable_img.SetActive(false);
                    //change catalog box price
                    GameObject amount_parent = ItemBtn.transform.Find("Amount").gameObject;
                    GameObject amount_text = amount_parent.transform.Find("Text").gameObject;
                    Text a_txt = amount_text.GetComponent<Text>();
                    a_txt.text = data.Amount.ToString();
                    break;
                }
            }
        }
    }
}