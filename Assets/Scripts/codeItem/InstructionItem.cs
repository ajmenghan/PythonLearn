using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionItem : MonoBehaviour {

    public int codeItemId; //对应行对象id

    void Start()
    {

    }

    void Update()
    {

    }
    //点击事件
    public void OnClick()
    {
        //在code区域添加相应code对象
        UIManager.Instance.CurrentCodeContent.AddCodeItem(codeItemId);
    }
}
