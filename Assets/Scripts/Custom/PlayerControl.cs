using Assets.GoblinsAndMagic.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public int attack = 20;
    public int hp = 100;

    private Animator animator;
    private Vector3[] directions; //方向向量
    private bool bMove = false; //是否正在移动
    private Vector3 targetPos = Vector3.zero; //目标位置
    private int dir; //移动方向
    private Enemy ememy; //需要攻击的目标敌人 

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        directions = new Vector3[4] {new Vector3(0, 0, 1), new Vector3(0, 0, -1),
            new Vector3(1,0,0), new Vector3(-1, 0, 0) };
    }
    void Update()
    {
        if(bMove) move();
    }
    //外面调用,向dir方向移动step步
    public void Move(int dir,int step)
    {
        this.dir = dir;
        if (dir > 1) transform.localScale = new Vector3((dir == 3 ? -1 : 1), 1, 1); //左右转向
        if (targetPos == Vector3.zero) targetPos = transform.position + directions[dir] * 16 * step;
        animator.Play("Move");
        bMove = true;
        Commands.Instance.bOperate = bMove;
    }
    public void Move(Vector2Int[] path)
    {
        Vector2Int[] points = new Vector2Int[path.Length - 1];
        if (points.Length == 0) return;
        for (int i = 0; i < points.Length; i++)
        {
            int tx = path[i + 1].x - path[i].x,
                ty = path[i + 1].y - path[i].y;
            if(tx!=0)
            {
                points[i].x = (tx > 0 ? 2 : 3);
                points[i].y = Mathf.Abs(tx);
            }else if(ty!=0)
            {
                points[i].x = (ty > 0 ? 0 : 1);
                points[i].y = Mathf.Abs(ty);
            }
        }
        StartCoroutine("move", points);
        bMove = true;
        Commands.Instance.bOperate = bMove;
    }
    //沿着一系列（dir，step）移动
    private IEnumerator move(Vector2Int[] points)
    {
        for (int i = 0; i < points.Length; i++) 
        {
            this.Move(points[i].x, points[i].y);
            //print(points[i].x+" "+ points[i].y);
            yield return new WaitUntil(() => !bMove);
        }
    }
    private void move()
    {
        transform.position = 
            Vector3.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            bMove = false;
            Commands.Instance.bOperate = bMove;
            transform.position = targetPos;
            targetPos = Vector3.zero;
            animator.Play("Stand");
            
        }
        //print(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }
    public void Attack(string enemy_name)
    {
        ememy = GameObject.Find(enemy_name).GetComponent<Enemy>();
        animator.Play("Attack");
        if (null != ememy) ememy.Damage(this.attack);
    }
    public void Damage(int hurt)
    {
        hp -= hurt;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
        else this.GetComponentInChildren<StatusBars>().SetHp(hp - hurt, 100);
    }
}
