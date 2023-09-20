using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopSlot[] slots;
    private int money;
    [SerializeField] private Player player;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private Transform selectedSide;
    [SerializeField] private GameObject sideVFX;


    [Header("Sides")]
    [SerializeField] private SideSO[] sides;
    private int sideTypeIndex;
    

    // ORDER: NORTH(0), SOUTH(1), EAST(2), WEST(3), NONE(-1)
    [SerializeField] private PlayerSide[] playerSides;
    private int playerSideIndex;

    private GameObject N, S, E, W;


    void Start()
    {
        SetUpShop();
        playerSideIndex = -1;
        playerSides[0].currentSide = sides[0].sideGO;
        GameObject a = Instantiate(sides[0].sideGO, playerSides[0].transform.position, playerSides[0].transform.rotation, playerSides[0].transform);
        a.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        money = player.money;
        if (playerSideIndex >= 0) { sideVFX.SetActive(true); sideVFX.transform.position = playerSides[playerSideIndex].transform.position * 2; }
        else sideVFX.SetActive(false);
    }

    public void ToggleShop()
    {
        playerSideIndex = -1;
        shopMenu.SetActive(!shopMenu.activeSelf);
        if (shopMenu.activeSelf == true) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ChangeSelectedSide(int index)
    {
        playerSideIndex = index;
    }
    public void BuySide(int typeIndex)
    {

        int cost = sides[typeIndex].cost;
        if (money < cost) return;

        if (playerSides[playerSideIndex].currentSide != null) playerSides[playerSideIndex].currentSide.SetActive(false);
        GameObject purchasedSide = Instantiate(sides[typeIndex].sideGO, playerSides[playerSideIndex].transform.position, playerSides[playerSideIndex].transform.rotation, playerSides[playerSideIndex].transform);

        playerSides[playerSideIndex].currentSide = purchasedSide;

        player.money -= cost;
        purchasedSide.SetActive(true);

    }

    private void SetUpShop()
    {
        for(int i = 0; i < sides.Length; i++)
        {
            slots[i].title.text = sides[i].sideName;
            slots[i].cost.text = sides[i].cost + " coins";
            if (sides[i].type == SideSO.SideType.Attack) slots[i].stats.text = "Dmg = " + sides[i].damage + " | Atk. Speed: " + (1/sides[i].attackCooldown);
            if (sides[i].type == SideSO.SideType.Economy) slots[i].stats.text = "Money/Sec: " + sides[i].moneyPerTick; 
        }
    }
}

[System.Serializable]
public class ShopSlot
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI cost;
    public TextMeshProUGUI stats;

}
