using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionItem : MonoBehaviour {

    public int codeItemId; //对应行对象id

    public void Init(CodeItem item)
    {
        this.codeItemId = item.id;
        this.GetComponentInChildren<Text>().text = item.body;
    }
    //点击事件
    public void OnClick()
    {
        //在code区域添加相应code对象
        UIManager.Instance.CurrentCodeContent.AddCodeItem(codeItemId);
    }
}
