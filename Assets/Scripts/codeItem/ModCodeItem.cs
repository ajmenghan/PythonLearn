using IronPython.Hosting;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class ModCodeItem : CodeItemBase {
    //模块（if、while）
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
    public override string Parse()
    {
        if (this.ParseParas() == true)
        {
            if (this.codeId == 30)
                return ""; //if：直接忽略该语句继续执行下面指令
            else if (this.codeId == 31)
                return "save"; //while：记录该语句(的对象)，之后再进行判断
            else return "";
        }
        else return "jump"; // 直接跳过该层级的所有指令
    }
    //解析参数
    private bool ParseParas()
    {
        return true;
    }
    

}
