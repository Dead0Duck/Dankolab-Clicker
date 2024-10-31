using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClickerGame : MonoBehaviour
{
    [Header("Математика")]
    [Description("Чем выше, тем дороже апгрейды с каждым уровнем.")] public double upgradeCost = 8.25 * 4.5;
    [Description("Чем выше, тем выгоднее будут нажатия с каждым уровнем.")] public double miningProfit = 2.3;

    [Header("Текст")]
    public TMP_Text moneyText;
    public TMP_Text levelText;
    public TMP_Text upgradeButtonText;
    public TMP_Text mineButtonText;

    [Header("Эффект вылетающих купюр")]
    public GameObject mineButton;
    public GameObject moneyPrefab;
    public int moneyParticleLimit = 50;

    private int plyLevel = 0;
    private int money = 0;
    
    void Start()
    {
        Application.targetFrameRate = 60;

        UpdateTexts();
    }

    void UpdateTexts()
    {
        levelText.text = $"LV {plyLevel + 1}";
        moneyText.text = $"{money}";
        upgradeButtonText.text = $"UPGRADE {GetUpgradeCost()}";
        mineButtonText.text = $"+{GetMinedMoney()}";
    }

    int GetUpgradeCost()
    {
        return (int)(20 + upgradeCost * plyLevel);
    }

    int GetMinedMoney()
    {
        return (int)(5 + miningProfit * plyLevel);
    }

    public void OnUpgradePressed()
    {
        int cost = GetUpgradeCost();
        if (cost > money) return;
        money -= cost;

        plyLevel++;
        UpdateTexts();
    }

    private List<GameObject> particles = new();
    public void OnMinePressed()
    {
        int minedMoney = GetMinedMoney();
        money += minedMoney;
        moneyText.text = $"{money}";

        var pos = Input.GetTouch(0).position;
        for (int i = 0; i < Math.Min(minedMoney, 10); i++)
        {
            if (particles.Count > moneyParticleLimit)
            {
                Destroy(particles[0]);
                particles.RemoveAt(0);
            }
            float ang = Random.Range(-75, 75);

            var money = Instantiate(moneyPrefab);
            var moneyTr = money.transform;
            moneyTr.position = pos;
            moneyTr.SetParent(mineButton.transform);
            moneyTr.Rotate(0, 0, ang);

            var moneyScr = money.GetComponent<MoneyParticle>();
            moneyScr.hSpd = -ang * 10;
            moneyScr.particles = particles;

            particles.Add(money);
        }    
    }
}
