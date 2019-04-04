using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private int needJumpLevel = -1;//执行命令时需要跳过的语句的层级
    private List<KeyValuePair<KeyValuePair<CodeItemBase, int>, int>> tempItems = 
        new List<KeyValuePair<KeyValuePair<CodeItemBase, int>, int>>(); //临时保存item对象，用于判断while循环(item(item,level),index)

    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) Move(2, 3);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray,out hit))
        {
            if(hit.transform.tag == GlobalValue.TAGS[(int)TAG.MAPTILE])
            {
                //Vector3 tempPos = transform.TransformPoint(hit.transform.position);
                //target = new Vector2Int((int)(tempPos.x - origin.x) / 16, 
                //                        (int)(tempPos.z - origin.z) / 16);
                ////print("目标点："+target.x + " " + target.y);
                ////print("玩家位置："+PlayerPoint.x + " " + PlayerPoint.y);
                ////this.MoveTo(new Vector2Int(target.x - PlayerPoint.x, target.y - PlayerPoint.y));
                var str = hit.transform.gameObject.name.Split(',');
                target = new Vector2Int(int.Parse(str[1]), int.Parse(str[2])); 
                //string name = this.FindNearEnemy();
                //this.Attack(name);
                //this.MoveTo(target);
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
    //检查移动的坐标或敌人是否合法
    private bool checkLegal(Vector2Int pos)
    {
        if (null == pos) return false;
        return GameManager.Instance.mapControl.isRoad(pos);
    }
    //向某方向移动(方向，步长)
    public void Move(int dir, int step)
    {
        player.GetComponent<PlayerControl>().Move(dir, step);
    }
    //坐标移动
    public void MoveTo(Vector2Int tar)
    {
        if (checkLegal(tar) == false) print("位置不可抵达！");
        else player.GetComponent<PlayerControl>().Move(GameManager.Instance.mapControl.FindPath(PlayerPoint, tar));
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
        //foreach (var item in itemsDic)
        for (int i = 0; i < itemsDic.Count; i++)  
        {
            var item = itemsDic.ElementAt(i);
            if (item.Value == needJumpLevel) continue; //需要跳过的层级直接跳过
            foreach(var temp in tempItems) //需要循环的命令进行循环
            {
                if (item.Value < temp.Key.Value || i == itemsDic.Count - 1) 
                {
                    //当前层级比存储的moditem层级小说明已经跳出了循环体，需要重新判断循环条件是否满足
                    if (temp.Key.Key.Parse() == "save")
                    {
                        //满足循环条件，继续循环
                        i = temp.Value; //重定向i
                    }
                    else tempItems.Remove(temp); //不满足则直接跳出循环，并且将moditem从列表移除
                }
            }
            print(item.Key.codeBody + "  " + item.Value);
            string ins = item.Key.Parse();
            if (ins == "jump") needJumpLevel = item.Value;
            else if (ins == "save")  
                tempItems.Add(new KeyValuePair<KeyValuePair<CodeItemBase, int>, int>(item, i));
            yield return new WaitUntil(() => !bOperate);
        }
    }
}
