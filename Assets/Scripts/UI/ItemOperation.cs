using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOperation : MonoBehaviour {
    public static ItemOperation Instance = null;
    public GameObject itemContent; //盛放item的容器
    private CodeItemBase item; //当前被操作的对象
	
    void Awake()
    {
        Instance = this;
    }

	public void Show(Vector3 pos,CodeItemBase item)
    {
        this.GetComponent<RectTransform>().position = pos;
        this.item = item;
        item.SetbOperate(true);
    }
    public void Hide()
    {
        this.GetComponent<RectTransform>().position = new Vector3(.0f, 5000.0f, .0f);
        item.SetbOperate(false);
    }
    public void OnHideBGClick()
    {
        this.Hide();
    }
    //【移除】按钮点击事件
    public void OnRemoveBtnClick()
    {
        this.RemoveItem();
    }
    void RemoveItem()
    {
        //TODO 移除被操作对象
        this.Hide();
        UIManager.Instance.CurrentCodeContent.RemoveCodeItem(item);
    }
    //【上移】按钮点击事件
    public void OnUpBtnClick()
    {
        this.Hide();
        UIManager.Instance.CurrentCodeContent.SwapLocation(item, true);
    }
    //【下移】按钮点击事件
    public void OnDownBtnClick()
    {
        this.Hide();
        UIManager.Instance.CurrentCodeContent.SwapLocation(item, false);
    }
}
