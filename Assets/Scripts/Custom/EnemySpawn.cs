using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	public GameObject[] enemyPres; //所有敌人的预制体
	private List<Enemy> enemyList;  //当前关卡所有敌人
	
    public void AddEnemy(int index)
    {

    }
    //移除某个敌人
	public void Remove(Enemy enemy)
	{
        enemyList.Remove(enemy);
	}
    //清空敌人列表
	public void Clear()
	{
		foreach(var e in enemyList)
			e.Death();
        enemyList.Clear();
	}
}
