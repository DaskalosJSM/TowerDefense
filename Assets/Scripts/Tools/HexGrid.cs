using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public List<GameObject> tilePrefabs;
    public bool isFlattopped;
    public float value;

    private float radius;
   
    private void OnEnable()
    {
        CalculateRadiusFromPrefab();
        LayoutGrid();
    }

    private void CalculateRadiusFromPrefab()
    {
        if (tilePrefabs != null && tilePrefabs.Count > 0)
        {
            MeshRenderer renderer = tilePrefabs[0].GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;
                radius = bounds.size.x / value ; // Asumimos que el ancho del hexágono es el doble del radio
            }
        }
    }

    public void LayoutGrid()
    {
        ClearGrid(); // Limpiar el grid antes de generar nuevos tiles
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2Int coordinate = new Vector2Int(x, y);
                Vector3 position = GetPositionforHexFromCoordinate(coordinate);

                // Elegir un prefab al azar de la lista
                GameObject randomTilePrefab = tilePrefabs[Random.Range(0, tilePrefabs.Count)];

                // Instanciar el prefab del tile en la posición calculada
                GameObject tile = Instantiate(randomTilePrefab, position, Quaternion.Euler(-90, 0, 0), transform);                tile.name = $"Hex {x},{y}";

            }
        }
    }

    private void ClearGrid()
    {
        // Destruir todos los hijos del objeto HexGrid para limpiar el grid
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public Vector3 GetPositionforHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = radius;

        if (!isFlattopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);

            offset = (shouldOffset) ? width / 2 : 0;
            xPosition = (column * (horizontalDistance)) + offset;
            yPosition = (row * verticalDistance);
        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3f) * size;

            horizontalDistance = width * (3f / 4f);
            verticalDistance = height;

            offset = (shouldOffset) ? height / 2 : 0;
            xPosition = (column * (horizontalDistance));
            yPosition = (row * verticalDistance) - offset;
        }

        return new Vector3(xPosition, 0, -yPosition);
    }
}

    
                