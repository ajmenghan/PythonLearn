using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VAR_TYPE //变量的数据类型
{
    INT,  //int
    STRING,  //string
    LIST,  //list
    FLOAT  //float
}
public class VarCodeItem : CodeItemBase {

    public VAR_TYPE type = VAR_TYPE.INT;  //变量数据类型

    public override void Init(CodeItem item)
    {
        base.Init(item);
        this.type = (VAR_TYPE)Enum.ToObject(typeof(VAR_TYPE), item.type);
    }

    public int getInt()
    {
        int temp = 0;

        return temp;
    }

    public string getString()
    {
        string temp = "";

        return temp;
    }

    public float getFloat()
    {
        float temp = .0f;

        return temp;
    }

    public Dictionary<VAR_TYPE, string> getList()
    {
        Dictionary<VAR_TYPE, string> temp = new Dictionary<VAR_TYPE, string>();

        return temp;
    }

}
