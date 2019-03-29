using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel : MonoBehaviour {
    public ScrollRect[] scrollRects; //所有模块滚动层面板
    public GameObject instItemPre; //item对象预制体

    void Start()
    {
        InitSRP(1);
    }

    //根据leveid初始化各个滚动层
    public void InitSRP(int level)
    {
        CodeItem[] items = DataControl.Instance.GetListById(level);
        for (int i = 0; i < items.Length; i++)
        {
            var temp = scrollRects[items[i].id / 10 - 1].GetComponentInChildren<VerticalLayoutGroup>();
            GameObject item = Instantiate(instItemPre, temp.transform);
            item.GetComponent<InstructionItem>().Init(items[i]);
        }
    }
    //打开对应的滚动层面板
    void OpenChooseSRP(int index)
    {
        for (int i = 0; i < scrollRects.Length; i++)
            scrollRects[i].gameObject.SetActive(false);
        scrollRects[index].gameObject.SetActive(true);
    }

    public void OnFuncToggleClick(bool check)
    {
        if (check) this.OpenChooseSRP(0);
    }
    public void OnVarToggleClick(bool check)
    {
        if (check) this.OpenChooseSRP(1);
    }
    public void OnModToggleClick(bool check)
    {
        if (check) this.OpenChooseSRP(2);
    }
    public void OnSymToggleClick(bool check)
    {
        if (check) this.OpenChooseSRP(3);
    }

}
