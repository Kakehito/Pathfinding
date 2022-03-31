using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ClickAgent : MonoBehaviour, IAgent
{
    PathfindingManager manager;
    public Transform currentTile;
    public float speed;
    public float margin;
    
    private void Start()
    {
        manager = PathfindingManager.instance;
    }
    private void Update()
    {
        Vector3 targetPos = new Vector3(currentTile.transform.position.x, 1, currentTile.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed*Time.deltaTime);        
    }

    public void AskForPath()
    {

    }
    
    public void SetPath(List<Transform> pathway)
    {

    }    
}
