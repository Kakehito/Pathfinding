using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ClickAgent : MonoBehaviour, IAgent
{
    PathfindingManager manager;

    public float Speed;
    public float Margin;

    public Tile StartingTile;
    public Tile TargetTile;
    public List<Tile> Path;
    int _currentpoint;

    private void Start()
    {
        manager = PathfindingManager.instance;
    }

    private void Update()
    {
        Vector3 targetPos = new Vector3(TargetTile.transform.position.x, 1, TargetTile.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Speed*Time.deltaTime);
        
      
    }

    [Button]
    public void GetPath()
    {
        manager.AddRequest(new PathRequest(GetComponent<ClickAgent>(), StartingTile, TargetTile));
    }
    
    public void SetPath(List<Tile> pathway)
    {
        Path = pathway;
    }    
}
