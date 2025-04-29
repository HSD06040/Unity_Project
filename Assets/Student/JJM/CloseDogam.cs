using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDogam : MonoBehaviour
{
    public OpenDogam openDogam; // OpenDogam ��ũ��Ʈ�� ����

    public void CloseDogamUI()
    {
        if (openDogam != null)
        {
            // ���� UI�� ��Ȱ��ȭ
            if (openDogam.dogamCanvas.gameObject.activeSelf)
            {
                openDogam.ToggleDogamUI();
            }
        }
        else
        {
            Debug.LogWarning("OpenDogam ��ũ��Ʈ�� �������� �ʾҽ��ϴ�.");
        }
    }
}
