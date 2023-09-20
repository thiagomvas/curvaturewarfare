using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : MonoBehaviour
{
    public bool canDoAction;
    public float bonusAtkSpd;
    public float bonusDmg;
    [SerializeField] private SoundManager sm;
    [SerializeField] private Player player;
    public GameObject currentSide;
    private SideSO sideSO;
    private float timer;
    private float nextAttackTime;
    private float nextEcoTime;
    // Start is called before the first frame update
    void Start()
    {
        canDoAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (currentSide == null) return;
        timer += Time.deltaTime;
        sideSO = currentSide.GetComponent<Side>().sideType;


        if (!canDoAction) return;
        if (Input.GetMouseButton(0) && sideSO.type == SideSO.SideType.Attack) Attack();
        if (sideSO.type == SideSO.SideType.Economy) GenerateIncome();
    }

    private void Attack()
    {
        if (timer < nextAttackTime) return;
        //                       Multiply attack cooldown by the cooldown reduction bonus
        //                       EX: Bonus Atk Speed = 20%
        //                       attackTime = timer + (1 * (1 - 20/100) = timer + (1 * 0.8) = timer + 0.8
        nextAttackTime = timer + (sideSO.attackCooldown * (1 - bonusAtkSpd/100));

        Vector3 shootDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;

        GameObject bullet = Instantiate(sideSO.projectile, this.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = (int)(sideSO.damage * (1 + bonusDmg/100));
        

        sm.PlaySound(0);
    }

    private void GenerateIncome()
    {
        if (timer < nextEcoTime) return;
        nextEcoTime = timer + sideSO.timeBetweenTicks;
        player.money += sideSO.moneyPerTick;

    }
}
