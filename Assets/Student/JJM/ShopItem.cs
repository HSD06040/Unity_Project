using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName; // ������ �̸�
    public Sprite icon; // ������ ������
    public int price; // ������ ����
    public string description; // ������ ����
}
