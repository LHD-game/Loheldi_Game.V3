using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NpcName : MonoBehaviour
{
    public GameObject Npc;
    public Interaction Inter;
    // Start is called before the first frame update

    void Update()
    {
        if (Inter.NpcNameTF)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(Npc.transform.position + new Vector3(0, 1.5f, 0));
        }
        else
            this.gameObject.SetActive(false);
    }


    // Update is called once per frame
    /*void OnTriggerStay(Collider other)
    {
        Debug.Log("¿Ã∏ß«•");
        this.transform.position = Camera.main.WorldToScreenPoint(other.transform.position + new Vector3(0, 2f, 0));
    }*/
    /*IEnumerator NpcNameFollow()
    {
        while (Inter.NpcNameTF)
        {
            this.transform.position = Camera.main.WorldToScreenPoint(Npc.transform.position + new Vector3(0, 1.5f, 0));
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield break;
    }*/
}
