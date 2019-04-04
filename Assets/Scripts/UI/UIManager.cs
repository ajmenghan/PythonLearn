using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public GameObject inputWindow;  //界面右边的代码编辑框
    public InputField paraInputField; //参数输入框
    private CodeContent _currentCodeConent = null;
    public CodeContent CurrentCodeContent {
        get
        {
            if (_currentCodeConent == null)
                _currentCodeConent = inputWindow.GetComponentInChildren<CodeContent>();             
            return _currentCodeConent;
        }
        set
        {
            _currentCodeConent = value;
        }
    } //当前正在编辑的代码框

    //更新列表显示
    public void UpdateContents()
    {
        CodeContent[] contents = inputWindow.GetComponentsInChildren<CodeContent>();
        for (int i = contents.Length - 1; i >= 0; i--) contents[i].UpdateList();
    }
    //执行按钮事件
    public void OnPerformBtnClick()
    {
        var itemsDic = CurrentCodeContent.ParseToCommonds();
        if(null != itemsDic)Commands.Instance.Execute(itemsDic);
    }
    //收起/打开编辑框
    public void OnCloseBtnClick()
    {
        if (inputWindow.GetComponent<RectTransform>().anchoredPosition3D.x < 0)
            inputWindow.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(210, 0, 0);
        else inputWindow.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-210, 0, 0);
    }
    //点击周围隐藏输入框 并 取消操作
    public void OnHideInputFildClick()
    {
        paraInputField.GetComponent<CanvasGroup>().alpha = 0;
        paraInputField.GetComponent<CanvasGroup>().blocksRaycasts = false;
        paraInputField.text = "";
    }
    //显示输入框 并 注册输入完成事件
    public InputField ShowInputFiled(UnityEngine.Events.UnityAction<string> call)
    {
        paraInputField.GetComponent<CanvasGroup>().alpha = 1;
        paraInputField.GetComponent<CanvasGroup>().blocksRaycasts = true;
        paraInputField.onEndEdit.RemoveAllListeners();
        paraInputField.onEndEdit.AddListener(call);
        return paraInputField;
    }
}
