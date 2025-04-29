using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDogam : MonoBehaviour
{
    [Header("Dogam UI")]
    public Canvas dogamCanvas; // ���� UI�� ����� ĵ����
    public GameObject itemUIPrefab; // ������ UI ������ (������ �̸�, ������ ǥ��)
    public Transform itemListParent; // ������ ����� ǥ���� �θ� ��ü
    public Text itemDetailText; // ������ �� ������ ǥ���� �ؽ�Ʈ
    public Image itemDetailIcon; // ������ �� ������ ������ ǥ��

    [Header("Item Data")]
    public List<ItemData> itemDatabase; // ������ �����ͺ��̽�

    private void Start()
    {
        PopulateDogam();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleDogamUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dogamCanvas.gameObject.activeSelf)
            {
                ToggleDogamUI();
            }
        }
    }

    public void ToggleDogamUI()
    {
        if (dogamCanvas != null)
        {
            dogamCanvas.gameObject.SetActive(!dogamCanvas.gameObject.activeSelf);
            Debug.Log($"Dogam Canvas ����: {dogamCanvas.gameObject.activeSelf}");
        }
        else
        {
            Debug.LogWarning("Dogam Canvas�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void PopulateDogam()
    {
        if (itemDatabase == null || itemDatabase.Count == 0)
        {
            Debug.LogError("itemDatabase�� ��� �ֽ��ϴ�. ������ �����͸� �߰��ϼ���.");
            return;
        }

        foreach (var item in itemDatabase)
        {
            if (item == null)
            {
                Debug.LogError("itemDatabase�� null �׸��� �ֽ��ϴ�.");
                continue;
            }

            Debug.Log($"������ �߰�: {item.itemName}");

            GameObject itemUI = Instantiate(itemUIPrefab, itemListParent);
            if (itemUI == null)
            {
                Debug.LogError("itemUIPrefab���� ������ itemUI�� null�Դϴ�.");
                continue;
            }

            Text textComponent = itemUI.GetComponentInChildren<Text>();
            if (textComponent == null)
            {
                Debug.LogError("itemUIPrefab�� Text ������Ʈ�� �����ϴ�.");
                continue;
            }
            textComponent.text = item.itemName;

            Image imageComponent = itemUI.GetComponentInChildren<Image>();
            if (imageComponent == null)
            {
                Debug.LogError("itemUIPrefab�� Image ������Ʈ�� �����ϴ�.");
                continue;
            }
            imageComponent.sprite = item.icon;

            Button itemButton = itemUI.GetComponent<Button>();
            if (itemButton == null)
            {
                Debug.LogError("itemUIPrefab�� Button ������Ʈ�� �����ϴ�.");
                continue;
            }
            Debug.Log($"Button ������Ʈ�� �����Ǿ����ϴ�: {item.itemName}");
            itemButton.onClick.AddListener(() => ShowItemDetails(item));
        }
    }

    private void ShowItemDetails(ItemData item)
    {
        Debug.Log($"������ �� ���� ǥ��: {item.itemName}");
        // ������ �� ���� ǥ��
        itemDetailText.text = $"Name: {item.itemName}\n" +
                              $"Grade: {item.itemGrade}\n" +
                              $"Description: {item.description}";
        itemDetailIcon.sprite = item.icon;
        itemDetailIcon.color = item.GetItemGradeColor(); // ��޿� ���� ���� ����
    }
}
