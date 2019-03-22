using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeContent : MonoBehaviour {
    //code区域需要进行的操作
    private InputField _paraInputField; //参数输入框
    private GameObject _itemPre; //当前正在添加的code对象

    public void OnEndInput(string value)
    {
        _paraInputField.GetComponent<CanvasGroup>().alpha = 0;
        _paraInputField.GetComponent<CanvasGroup>().blocksRaycasts = false;
        _paraInputField.text = "";  
        if (null == _itemPre) return;
        CreateItem(value);
    }

    //在最后一行添加code对象
    public void AddCodeItem(int id)
    {
        //根据id在code区域生成对应code对象
        _itemPre = DataControl.Instance.GetCodeItemById(id);  
        //如果需要参数则首先生成输入框，否则直接生成code对象
        if (_itemPre.GetComponent<CodeItemBase>().bNeedPara)
        {
            _paraInputField = UIManager.Instance.ShowInputFiled(delegate { this.OnEndInput(_paraInputField.text); });            
            return;
        }
        CreateItem();
    }
    //创建item对象
    private void CreateItem(string value = null)
    {
        GameObject item = Instantiate(_itemPre, this.transform);
        //this.UpdateList();
        if (value != null) item.GetComponent<CodeItemBase>().SetContent("(" + value + ")");
        UIManager.Instance.UpdateContents();
    }
    //移除对应位置的code对象
    public void RemoveCodeItem(CodeItemBase item)
    {
        DestroyImmediate(item.gameObject);
        //this.UpdateList();
        UIManager.Instance.UpdateContents();
    }
    //更新列表显示
    public void UpdateList()
    {
        float initialPosy = -0f; 
        for (int i = 0; i < transform.childCount; i++) 
        {
            RectTransform item = transform.GetChild(i).GetComponent<RectTransform>();
            RectTransform preitem = (i == 0 ? item : transform.GetChild(i - 1).GetComponent<RectTransform>());
            float lasty = (i == 0 ? initialPosy + preitem.rect.size.y + 5 : preitem.anchoredPosition3D.y);
            item.anchoredPosition3D = new Vector3(0, lasty - preitem.rect.size.y - 5, 0);
        }
        //设置滚动区域大小
        Vector2 oldSize = this.GetComponent<RectTransform>().rect.size;
        RectTransform lastItem = transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>();
        float height = Mathf.Abs(lastItem.anchoredPosition3D.y) + lastItem.rect.size.y + 5;
        this.GetComponent<RectTransform>().sizeDelta =
            new Vector2(oldSize.x, height);
        transform.parent.GetComponent<RectTransform>().sizeDelta = this.GetComponent<RectTransform>().rect.size;
    }
    //交换相邻位置code对象的上下方位(当前操作的对象，上移还是下移)
    public void SwapLocation(CodeItemBase item,bool upOrdown)
    {
        Transform item_zero = transform.GetChild(0);
        if (item_zero == null) return;
        float initialPosy = item_zero.GetComponent<RectTransform>().position.y;
        //int index = Mathf.Abs((int)((item.GetComponent<RectTransform>().position.y - initialPosy) / 70));
        int index = item.transform.GetSiblingIndex();
        int newIndex = upOrdown ? (index - 1) : (index + 1);
        if (newIndex < 0 || newIndex >= transform.childCount) return;
        Transform needItem = transform.GetChild(newIndex);
        //设置层级交换顺序 以及 交换位置
        item.transform.SetSiblingIndex(newIndex);
        needItem.transform.SetSiblingIndex(index);
        //UpdateList();
        UIManager.Instance.UpdateContents();
    }
    //点击选中自身
    public void OnChooseSelfClick()
    {
        UIManager.Instance.CurrentCodeContent = this;
        CanvasGroup surface = transform.parent.Find("surface").GetComponent<CanvasGroup>();
        CanvasGroup bg = transform.parent.Find("bg").GetComponent<CanvasGroup>();
        surface.alpha = 0;
        surface.blocksRaycasts = false;
        bg.alpha = 1;
        bg.blocksRaycasts = true;
    }
    //点击隐藏自身
    public void OnHideSelfClick()
    {
        UIManager.Instance.CurrentCodeContent = null;
        CanvasGroup surface = transform.parent.Find("surface").GetComponent<CanvasGroup>();
        CanvasGroup bg = transform.parent.Find("bg").GetComponent<CanvasGroup>();
        bg.alpha = 0;
        bg.blocksRaycasts = false;
        surface.alpha = 1;
        surface.blocksRaycasts = true;
    }
    //将自身包含的code对象转换成指令
    public CodeItemBase[] ParseToCommonds()
    {
        //TODO
        CodeItemBase[] items = null;

        return items;
    }	
}
