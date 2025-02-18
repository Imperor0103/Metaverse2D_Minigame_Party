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
            Debug.LogError("Tilemap 컴포넌트가 없습니다!");
            enabled = false;
            return;
        }

        if (sortingGroup == null)
        {
            Debug.LogError("Sorting Group 컴포넌트가 없습니다!");
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
        // 타일맵의 Y 좌표를 기준으로 Order in Layer 값을 설정
        int baseOrder = Mathf.RoundToInt(transform.position.y * -10); // Y 좌표가 낮을수록 Order in Layer 값을 높게 설정
        sortingGroup.sortingOrder = baseOrder;
    }
}
