using UnityEngine;
using UnityEditor;

public class GridController : MonoBehaviour
{
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadius = .5f;
    [SerializeField] private LayerMask unwalkableMask;
    [SerializeField] private LayerMask encumberedMask;
    [SerializeField] private Transform targetWorldPosition;

    public FlowField currentFlowField { get; private set;}

    private void Awake()
    {
        InitializeFlowField();
        currentFlowField.CreateCostField();
        Node targetGridPosition = currentFlowField.GetNodeFromWorldPoint(targetWorldPosition.position);
        currentFlowField.CreateIntegrationField(targetGridPosition);

        currentFlowField.CreateFlowField();  
    }
   
    private void InitializeFlowField()
    {
        currentFlowField = new FlowField(nodeRadius, gridWorldSize, unwalkableMask, encumberedMask);
        currentFlowField.CreateGrid(transform.position);
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(currentFlowField != null)
        {
            if(currentFlowField.grid != null)
            {
                foreach(Node node in currentFlowField.grid)
                {
                    //Gizmos.color = node.walkable ? Color.green : Color.red;
                    
                    float t = (float) node.bestCost / 75;
                    Gizmos.color = Color.Lerp(Color.yellow, Color.magenta, t);
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeRadius*2 - .1f));
                    
                    //Gizmos.DrawWireCube(node.worldPosition, Vector3.one * (nodeRadius*2 - .1f));
                    //Handles.Label(node.worldPosition, node.bestCost.ToString());
                }
            }
        }
        
    }
    */
    
}
