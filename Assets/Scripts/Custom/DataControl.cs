using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataControl : MonoBehaviour {
    public static DataControl Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public GameObject[] codeItemPres; //code对象预制体集合

    public GameObject GetCodeItemById(int id)
    {
        GameObject item = null;
        //TODO 根据Id获取code对象
        for(int i = 0; i < codeItemPres.Length; i++)
        {
            if(codeItemPres[i].GetComponent<CodeItemBase>().codeId == id)
            {
                item = codeItemPres[i];
                break;
            }
        }
        return item;
    }
	
}
