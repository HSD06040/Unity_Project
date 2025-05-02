using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    public Transform itemListParent; // ������ ����� ǥ���� �θ� ��ü
    public GameObject shopItemPrefab; // ���� ������ UI ������
    public Text playerGoldText; // �÷��̾��� ��带 ǥ���� �ؽ�Ʈ

    [Header("Shop Data")]
    public List<ShopItem> shopItems; // ������ ��ϵ� ������ ����Ʈ

    [Header("Player Data")]
    public PlayerData playerData; // �÷��̾� ������
    private void Start()
    {
        PopulateShop();
        UpdatePlayerGoldUI();
    }

    private void PopulateShop()
    {
        // ���� UI ����
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // ���� ������ UI ����
        foreach (var item in shopItems)
        {
            GameObject itemUI = Instantiate(shopItemPrefab, itemListParent);
            itemUI.GetComponentInChildren<Text>().text = $"{item.itemName}\nPrice: {item.price}";
            itemUI.GetComponentInChildren<Image>().sprite = item.icon;

            Button buyButton = itemUI.GetComponentInChildren<Button>();
            buyButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    private void BuyItem(ShopItem item)
    {
        if (playerData.CanAfford(item.price))
        {
            playerData.SubGold(item.price);
            playerData.AddItem(item);
            Debug.Log($"������ ����: {item.itemName}, ����: {item.price}");
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.LogWarning("��尡 �����մϴ�!");
        }
    }

    private void SellItem(ShopItem item)
    {
        if (playerData.inventory.Contains(item))
        {
            playerData.RemoveItem(item);
            playerData.AddGold(item.price / 2); // �Ǹ� ������ ���� ������ �������� ����
            Debug.Log($"������ �Ǹ�: {item.itemName}, ����: {item.price / 2}");
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.LogWarning("�κ��丮�� �ش� �������� �����ϴ�!");
        }
    }

    private void UpdatePlayerGoldUI()
    {
        playerGoldText.text = $"Gold: {playerData.gold}";
    }
}
