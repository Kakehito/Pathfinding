using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Enemy : MonoBehaviour ,IAgent
{
    public List<Tile> Path;


    public float Speed = 2;
    public float Margin = 1.1f;

    public Tile StartingTile;
    public Tile TargetTile;

    PathfindingManager manager;
    int _currentpoint = 0;

    private void Start()
    {
        SetWeights();
    }

    [Button]
    public void SetWeights()
    {
        StartingTile.startWeight += 10000f;
        StartingTile.ChangeColor(1f);
        foreach(Tile t in StartingTile.GetNeighbours())
        {
            t.startWeight += 100f;
            t.ChangeColor(0.5f);
        }
    }



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

    private void OnDestroy()
    {
        StartingTile.startWeight -= 10000f;
        StartingTile.ChangeColor(0f);
        foreach (Tile t in StartingTile.GetNeighbours())
        {
            t.startWeight -= 100f;
            t.ChangeColor(0);
        }
    }
}
