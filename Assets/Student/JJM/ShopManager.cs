using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    public Transform itemListParent; // ������ ����� ǥ���� �θ� ��ü
    public GameObject shopItemPrefab; // ���� ������ UI ������

    [Header("Shop Data")]
    public List<ShopItem> shopItems; // ������ ��ϵ� ������ ����Ʈ

    private void Start()
    {
        PopulateShop();
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
        Debug.Log($"������ ����: {item.itemName}, ����: {item.price}");
        // ���� ���� �߰� (��: �÷��̾� ��� ����, ������ �߰� ��)
    }
}
