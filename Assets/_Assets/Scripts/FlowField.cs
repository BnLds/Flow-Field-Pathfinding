using UnityEngine;

public class FlowField
{
    public Node[,] grid { get; private set; }
    public Vector2 gridWorldSize { get; private set; }
    public float nodeRadius { get; private set; }
    public LayerMask unwalkableMask { get; private set; }

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    private Vector3 worldBottomLeftCorner;
    private Vector3 worldGridPosition;

    public FlowField(float _nodeRadius, Vector2 _gridWorldSize, LayerMask _unwalkableMask)
    {
        nodeRadius = _nodeRadius;
        gridWorldSize = _gridWorldSize;
        unwalkableMask = _unwalkableMask;
        nodeDiameter = _nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
    }

    public void CreateGrid(Vector3 position)
    {
        grid = new Node[gridSizeX, gridSizeY];
        worldGridPosition = position;
        worldBottomLeftCorner = position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeftCorner + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, new Vector2Int(x,y));
            }
        }
    }

    public void CreateCostField()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeftCorner + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                if(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask))
                {
                    grid[x, y].cost = byte.MaxValue;
                }
            }
        }

    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        int x = Mathf.FloorToInt(Mathf.Clamp(gridSizeX * percentX, 0, gridSizeX - 1)); // need to substract 1 from gridSizeX as x and y node coordinates are 0 based in the grid array 
        int y = Mathf.FloorToInt(Mathf.Clamp(gridSizeY * percentY, 0, gridSizeY - 1));

        return (grid[x, y]);
    }
}
