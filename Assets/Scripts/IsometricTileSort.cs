using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class IsometricTileSort : MonoBehaviour
{
    private Tilemap tilemap;
    private SortingGroup sortingGroup;

    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        sortingGroup = GetComponentInChildren<SortingGroup>();

        if (tilemap == null)
        {
            Debug.LogError("Tilemap ������Ʈ�� �����ϴ�!");
            enabled = false;
            return;
        }

        if (sortingGroup == null)
        {
            Debug.LogError("Sorting Group ������Ʈ�� �����ϴ�!");
            enabled = false;
            return;
        }

        UpdateTileSorting();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            UpdateTileSorting();
        }
#endif
    }

    void UpdateTileSorting()
    {
        // Ÿ�ϸ��� Y ��ǥ�� �������� Order in Layer ���� ����
        int baseOrder = Mathf.RoundToInt(transform.position.y * -10); // Y ��ǥ�� �������� Order in Layer ���� ���� ����
        sortingGroup.sortingOrder = baseOrder;
    }
}
