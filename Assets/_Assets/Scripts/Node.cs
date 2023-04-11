using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public Vector2Int gridIndex;
    public byte cost;

    public Node(bool _walkable, Vector3 _worldPosition, Vector2Int _gridIndex)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridIndex = _gridIndex;
        cost = 1;
    }
}
