using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void callFunc(); //定义回调函数的委托
//所有函数对应的操作命令
public class Commands : MonoBehaviour {
    public static Commands Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public GameObject player; //玩家角色对象
    public Vector3 origin; //坐标原点
    public bool bOperate = false; //是否正在进行某项操作
    private Vector2Int target; //点选的坐标

    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) Move(2, 3);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray,out hit))
        {
            if(hit.transform.tag == GlobalValue.TAGS[(int)TAG.MAPTILE])
            {
                Vector3 tempPos = transform.TransformPoint(hit.transform.position);
                target = new Vector2Int((int)(tempPos.x - origin.x) / 16, 
                                        (int)(tempPos.z - origin.z) / 16);
                //print("目标点："+target.x + " " + target.y);
                //print("玩家位置："+PlayerPoint.x + " " + PlayerPoint.y);
                //this.MoveTo(new Vector2Int(target.x - PlayerPoint.x, target.y - PlayerPoint.y));
                string name = this.FindNearEnemy();print(name);
                this.Attack(name);
            }
        }
    }
    //角色相对于原点位置的坐标
    private Vector2Int PlayerPoint
    {
        get
        {
            Vector3 tempPos = transform.TransformPoint(player.transform.position);
            return new Vector2Int((int)(tempPos.x - origin.x) / 16,
                                  (int)(tempPos.z - origin.z) / 16);
        }
    }
    //向某方向移动(方向，步长)
    public void Move(int dir, int step)
    {
        player.GetComponent<PlayerControl>().Move(dir, step);
    }
    //坐标移动
    public void MoveTo(Vector2Int tar)
    {
        player.GetComponent<PlayerControl>().Move(tar);
    }
    //攻击敌人
    public void Attack(string enemy_name)
    {
        player.GetComponent<PlayerControl>().Attack(enemy_name);
    }
    //找到距离最近的敌人并返回名字
    public string FindNearEnemy()
    {
        string name = "";
        var enemys = GameObject.FindObjectsOfType<Enemy>();
        float dis = float.MaxValue;
        for(int i=0;i<enemys.Length;i++)
        {
            float temp = Vector3.Distance(player.transform.position, enemys[i].transform.position);
            if (temp < dis)
            {
                dis = temp;
                name = enemys[i].gameObject.name;
            }
        }
        return name;
    }
    //根据代码执行命令
    public void Execute(Dictionary<CodeItemBase, int> itemsDic)
    {
        StartCoroutine("execute", itemsDic);
    }
    //延迟等待前一个操作完成再进行下一个操作
    private IEnumerator execute(Dictionary<CodeItemBase, int> itemsDic)
    {
        foreach (var item in itemsDic)
        {
            print(item.Key.codeBody + "  " + item.Value + " " + item.Key.Parse());
            yield return new WaitUntil(() => !bOperate);
        }
    }
    //执行if模块
    public void IfExecute(List<bool> conditions, List<callFunc> mods)
    {
        if(conditions.Count==0)
        {
            mods[0]();
            return;
        }
        if (conditions[0]) mods[0]();
        else
        {
            conditions.RemoveAt(0);
            mods.RemoveAt(0);
            IfExecute(conditions, mods);
        } 
    }
    public void WhileExecute(bool condition, callFunc mod)
    {
        while (condition)
            mod();
    }




}
