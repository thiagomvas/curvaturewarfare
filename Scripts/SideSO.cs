using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Side", menuName = "Side")]
public class SideSO : ScriptableObject
{
    public string sideName;
    public Image icon;
    public GameObject sideGO;
    public int cost;


    public SideType type;

    public enum SideType { Attack, Defense, Support, Economy };

    [Header("Attack Type")]
    public GameObject projectile;
    public float attackCooldown;
    public int damage;

    [Header("Defense Type")]
    public int extraHp;
    public int extraDef;

    [Header("Support Type")]
    public string placeholder;

    [Header("Economy Type")]
    public int moneyPerTick;
    public int timeBetweenTicks;
}
