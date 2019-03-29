using Assets.GoblinsAndMagic.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int attack = 10;
    public int hp = 100;

	public void Attack()
    {
        var target = Commands.Instance.player.GetComponent<PlayerControl>();
        GetComponent<Animator>().Play("Attack");
        if (null != target) target.Damage(this.attack);
    }
    public void Damage(int hurt)
    {
        hp -= hurt;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            return;
        }
        this.GetComponentInChildren<StatusBars>().SetHp(hp - hurt, 100);
        this.Attack();
    }
}
