using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour {

    public GameObject[] mapTilePres; //地图块预制体
    public Transform mapParent; //地图元素的父物体
    public Vector3 orgin; //地图原点
    public int width = 12; //地图的宽度
    private List<GameObject> mapTiles = new List<GameObject>(); //保存所有地图块

    void Start()
    {
        InitMap(1);
    }
    //根据数据生成地图
    private void InitMap(int level)
    {
        var mapdata = DataControl.Instance.GetMapDataByLevel(level);
        for (int i = 0, j = -1; i < mapdata.Length; i++)
        {
            if (i % 12 == 0) j++;
            var mapTile = Instantiate(mapTilePres[mapdata[i]], mapParent);
            mapTile.transform.localPosition = orgin + new Vector3(16 * (i % 12), 0, 16 * j);
            mapTile.name = mapdata[i] + "," + i % 12 + "," + j; //名字 = 类型 + 坐标
            mapTiles.Add(mapTile);
        }
    }
    //获取对应位置的maptile
    public GameObject GetTileByPos(Vector2Int pos)
    {
        foreach(var temp in mapTiles)
        {
            var str = temp.gameObject.name.Split(','); 
            if (pos.x == int.Parse(str[1]) && pos.y == int.Parse(str[2])) return temp;
        }
        return null;
    }
    //判断对应位置的tile是否是路径
    public bool isRoad(Vector2Int pos)
    {
        var tile = this.GetTileByPos(pos);
        if (null == tile || int.Parse(tile.name[0] + "") != 1) return false;
        return true;
    }
    //获取移动路径（如果可以抵达）
    public Vector2Int[] FindPath(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        if (isRoad(end)) findPath(path, start, end);
        foreach(var temp in path)
        {
            //print(temp.x + "-" + temp.y);
        }
        return path.ToArray();
    }
    private bool findPath(List<Vector2Int> path, Vector2Int start, Vector2Int end)
    {
        if (start.x < 0 || start.y < 0 || isRoad(start) == false || path.IndexOf(start) != -1) return false;
        path.Add(start);
        if (start == end) return true;
        bool isHave = (findPath(path, new Vector2Int(start.x + 1, start.y), end)) ||
            (findPath(path, new Vector2Int(start.x - 1, start.y), end)) ||
            (findPath(path, new Vector2Int(start.x, start.y + 1), end)) ||
            (findPath(path, new Vector2Int(start.x, start.y - 1), end));
        if (!isHave) path.Remove(start);
        return isHave;
    }
}
