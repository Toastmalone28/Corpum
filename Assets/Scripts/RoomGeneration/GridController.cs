using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
        public int gridSizeVertical, gridSizeHorizontal;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector3> availablePoints = new();

    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - grid.gridSizeHorizontal;
        grid.rows = room.Height - grid.gridSizeVertical;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector3(x - (grid.columns - grid.verticalOffset), 1, y - (grid.rows - grid.horizontalOffset));
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
