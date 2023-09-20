using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerSide playerSide;
    [Space]
    [SerializeField] private Upgrade[] dmgUpgrade;
    [Space]
    [SerializeField] private Upgrade[] atkSpdUpgrade;

    [SerializeField] private PlayerSide[] sides;
    [Header("Damage")]
    [SerializeField] private TextMeshProUGUI dmgCostTxt;
    [SerializeField] private TextMeshProUGUI dmgTierTxt;
    [Header("Atk Speed")]
    [SerializeField] private TextMeshProUGUI atkSpdCostTxt;
    [SerializeField] private TextMeshProUGUI atkSpdTierTxt;

    private int playerSideIndex;
    public int dmgTier;
    public int atkSpdTier;
    
    void Start()
    {
        dmgTier = 0;
        atkSpdTier = 0;
        SetUpUpgrades();
    }

    void Update()
    {
        
    }

    public void ChangeSelectedSide(int index)
    {
        playerSideIndex = index;
    }

    public void BuyDmgUpgrade()
    {
        if (dmgTier >= dmgUpgrade.Length) return;
        if (dmgUpgrade[dmgTier].cost <= player.money && dmgTier < dmgUpgrade.Length)
        {
            for(int i = 0; i < sides.Length; i++)
            {
            sides[i].bonusDmg = (dmgTier + 1) * 20;
            }
            player.money -= dmgUpgrade[dmgTier].cost;
            dmgTier++;
            SetUpUpgrades();
        }
    }
    public void BuyAtkSpdUpgrade()
    {
        if (atkSpdTier >= atkSpdUpgrade.Length) return;
        if (atkSpdUpgrade[atkSpdTier].cost <= player.money && atkSpdTier < atkSpdUpgrade.Length)
        {
            for(int i = 0; i < sides.Length; i++)
            {
            sides[i].bonusAtkSpd = (atkSpdTier + 1) * 10;
            }
            player.money -= atkSpdUpgrade[atkSpdTier].cost;
            atkSpdTier++;
            SetUpUpgrades();
        }
    }

    private void SetUpUpgrades()
    {

        if (dmgTier < dmgUpgrade.Length) dmgCostTxt.text = "Cost: " + dmgUpgrade[dmgTier].cost;
        else dmgCostTxt.text = "MAXED";

        if (atkSpdTier < atkSpdUpgrade.Length) atkSpdCostTxt.text = "Cost: " + atkSpdUpgrade[atkSpdTier].cost;
        else atkSpdCostTxt.text = "MAXED";
       


        atkSpdTierTxt.text = "Current Tier: " + atkSpdTier + " (" + (atkSpdTier * 10) + "%)";
        dmgTierTxt.text = "Current Tier: " + dmgTier + " (" + (dmgTier * 20) + "%)";
    }
}

[System.Serializable]
public class Upgrade
{
    public int cost;
    public float bonusStat;
}
