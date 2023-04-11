using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadius = .5f;
    [SerializeField] private LayerMask unwalkableMask;

    private FlowField currentFlowField;

    private void Awake()
    {
        InitializeFlowField();
    }

    private void InitializeFlowField()
    {
        currentFlowField = new FlowField(nodeRadius, gridWorldSize, unwalkableMask);
        currentFlowField.CreateGrid(transform.position);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(currentFlowField != null)
        {
            if(currentFlowField.grid != null)
            {
                foreach(Node node in currentFlowField.grid)
                {
                    Gizmos.color = node.walkable ? Color.white : Color.red;
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeRadius*2 - .1f));
                }
            }
        }
        
    }
    
}
