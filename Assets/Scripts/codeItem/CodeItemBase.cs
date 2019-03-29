using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CodeItemType
{
    FUNC,  //函数
    VAR,   //变量
    MOD,   //模块
    SYM,   //操作符
}

public class CodeItemBase : MonoBehaviour {

	public int codeId; //当前行代码id
    public string codeBody; //代码主体
    public CodeItemType itype; //code类型
    private bool bOperate = false; //是否正在操作
    public bool bNeedPara = false; //是否需要参数
    public string paras; //参数

    public virtual void Init(CodeItem item)
    {
        this.codeId = item.id;
        this.codeBody = item.body;
        this.bNeedPara = item.bNeedPara;
        this.itype = (CodeItemType)Enum.ToObject(typeof(CodeItemType), item.id / 10 - 1);
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        this.GetComponent<Button>().onClick.AddListener(delegate { this.OnClick(); });
    }
    //点击事件
    public void OnClick()
    {
        if(bOperate)
        {
            ItemOperation.Instance.Hide();
            return;
        }
        Vector3 pos = this.GetComponent<RectTransform>().position;
        ItemOperation.Instance.Show(new Vector3(pos.x, pos.y-70, pos.z),this);
    }
    public void SetbOperate(bool btemp)
    {
        this.bOperate = btemp;
    }
    //设置code对象显示的内容
    public void SetContent(string value)
    {
        this.paras = value;
        if (itype == CodeItemType.FUNC) value = "(" + value + ")";
        else value = " " + value;
        this.GetComponentInChildren<Text>().text = codeBody + value;
    }
    //将自己解析成一系列命令
    public virtual string Parse()
    {
        string com = "";
        return com;       
    }
}
