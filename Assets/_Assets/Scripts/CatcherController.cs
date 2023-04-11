using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour
{
    [SerializeField] private GridController gridController;
    [SerializeField] private List<Transform> catchersInGame;
    [SerializeField] private float moveSpeed = .5f;


    private void FixedUpdate()
    {
        if(gridController.currentFlowField == null) return;
        foreach(Transform catcher in catchersInGame)
        {
            Node nodeBelow = gridController.currentFlowField.GetNodeFromWorldPoint(catcher.position);
            Vector3 moveDirection = new Vector3(nodeBelow.bestDirection.Vector.x, 0, nodeBelow.bestDirection.Vector.y);
            Rigidbody catcherRB = catcher.GetComponent<Rigidbody>();
            catcherRB.velocity = moveDirection * moveSpeed;
        }
    }
}
