using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ClickAgent : MonoBehaviour, IAgent
{
    PathfindingManager manager;

    public float Speed = 2;
    public float Margin = 1.1f;

    public Tile StartingTile;
    public Tile TargetTile;

    public List<Tile> Path;
    int _currentpoint = 0;
    Transform _transform;

    private void Start()
    {
        Path = null;
        manager = PathfindingManager.instance;
        _transform = transform;
    }

    private void Update()
    {
        if (Path != null)
        {
            StartingTile = Path[_currentpoint];

            float dist = Vector3.Distance(_transform.position, StartingTile.transform.position);
            Vector3 targetPos = new Vector3(StartingTile.transform.position.x, 1, StartingTile.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos,   Speed  * Time.deltaTime / dist);

            if (dist < Margin && StartingTile != Path[Path.Count - 1]) 
                _currentpoint++;


        }
    }

    [Button]
    public void GetPath()
    {
        Path = null;
        _currentpoint = 0;
        manager.AddRequest(new PathRequest(GetComponent<ClickAgent>(), StartingTile, TargetTile));
    }
    
    public void SetPath(List<Tile> pathway)
    {
        Path = null;
        Path = pathway;
    }    
}
