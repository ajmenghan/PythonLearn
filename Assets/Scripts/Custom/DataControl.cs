using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataControl : MonoBehaviour {
    public static DataControl Instance = null;
    void Awake()
    {
        Instance = this;
    }

    /*item数据
         * id: 10~19 = 函数 
         *     20~29 = 变量 
         *     30~39 = 模块 
         *     40~49 = 操作符 
         */
    private CodeItem[] codeItems = {
        new CodeItem(10,    "hero.moveUp",  true,  -1),
        new CodeItem(11,    "hero.moveDown",  true,  -1),
        new CodeItem(12,    "hero.moveLeft",  true,  -1),
        new CodeItem(13,    "hero.moveRight",  true,  -1),
        new CodeItem(20,    "enemy",        false,  1),
        new CodeItem(30,    "if",           true,  -1),
        new CodeItem(31,    "while",        true,  -1),
        new CodeItem(40,    "#",            true,  -1)
    };

    //根据Id获取code对象
    public CodeItem GetCodeItemById(int id)
    {
        CodeItem item = null;        
        for(int i = 0; i < codeItems.Length; i++)
        {
            if(codeItems[i].id == id)
            {
                item = codeItems[i];
                break;
            }
        }
        return item;
    }	
    //根据关卡id获得item对象的list
    public CodeItem[] GetListById(int id)
    {
        CodeItem[] items = new CodeItem[] { codeItems[0], codeItems[1],codeItems[2], codeItems[3], codeItems[7], codeItems[5] };
        return items;
    }
}
public class CodeItem
{
    public int id;
    public string body;
    public bool bNeedPara;
    public int type;

    public CodeItem(int id,string body,bool bn,int type)
    {
        this.id = id;
        this.body = body;
        this.bNeedPara = bn;
        this.type = type;
    }
}
