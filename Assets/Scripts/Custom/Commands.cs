using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//所有函数对应的操作命令
public class Commands : MonoBehaviour {
    public static Commands Instance = null;
    void Awake()
    {
        Instance = this;
    }

    public GameObject player; //玩家角色对象
    public Vector3 origin; //坐标原点
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
                this.MoveTo(new Vector2Int(target.x - PlayerPoint.x, target.y - PlayerPoint.y));
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

    }
    //找到距离最近的敌人并返回名字
    public string FindNearEnemy()
    {
        string name = "";
        //TODO 
        return name;
    }
    //根据代码执行命令
    public void Execute(CodeItemBase[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            print(items[i].codeBody);
        }
    }




}
