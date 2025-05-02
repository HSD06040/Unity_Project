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

    [Header("Purchase Popup")]
    public GameObject purchasePopup; // ���� Ȯ�� �˾�
    public Text popupMessageText; // �˾� �޽��� �ؽ�Ʈ
    public Button confirmButton; // Ȯ�� ��ư
    public Button cancelButton; // ��� ��ư

    private ShopItem selectedItem; // ���� ���õ� ������
    private void Start()
    {
        PopulateShop();
        UpdatePlayerGoldUI();

        // �˾� ��ư �̺�Ʈ ����
        confirmButton.onClick.AddListener(ConfirmPurchase);
        cancelButton.onClick.AddListener(ClosePopup);
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
            buyButton.onClick.AddListener(() => ShowPurchasePopup(item));
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
    private void ShowPurchasePopup(ShopItem item)
    {
        selectedItem = item; // ���õ� ������ ����
        popupMessageText.text = $"'{item.itemName}'��(��) {item.price} ��忡 �����Ͻðڽ��ϱ�?";
        purchasePopup.SetActive(true); // �˾� Ȱ��ȭ
    }

    private void ConfirmPurchase()
    {
        if (selectedItem != null && playerData.CanAfford(selectedItem.price))
        {
            playerData.SubGold(selectedItem.price);
            playerData.AddItem(selectedItem);
            Debug.Log($"������ ����: {selectedItem.itemName}, ����: {selectedItem.price}");
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.LogWarning("��尡 �����մϴ�!");
        }

        ClosePopup(); // �˾� �ݱ�
    }

    private void ClosePopup()
    {
        purchasePopup.SetActive(false); // �˾� ��Ȱ��ȭ
        selectedItem = null; // ���õ� ������ �ʱ�ȭ
    }
    private void UpdatePlayerGoldUI()
    {
        playerGoldText.text = $"Gold: {playerData.gold}";
    }
}
