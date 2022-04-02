using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public struct PathRequest
{
    public IAgent Agent;
    public Tile StartTile;
    public Tile TargetTile;

    public PathRequest(IAgent a, Tile t, Tile c)
    {
        Agent = a;
        StartTile = t;
        TargetTile = c;
    }
}

public class PathfindingManager : MonoBehaviour
{
    #region Singleton
    public static PathfindingManager instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    CustomGrid map;


    private void Start()
    {
        map = GetComponent<CustomGrid>();
    }

    public List<PathRequest> RequestQueue;


    [Button]
    public void FufillRequest()
    {
        CalculatePath(RequestQueue[0]);
    }

    public void AddRequest(PathRequest request)
    {
        RequestQueue.Add(request);
    }



    public void CalculatePath(PathRequest request)
    {
        Tile _targetTile = request.TargetTile;
        Tile _current = request.StartTile;
        List<Tile> _path = new List<Tile>();


        SetWeight(_targetTile.transform);


        while(_current != _targetTile)
        {
            _current.GetComponent<MeshRenderer>().material.color = Color.red;
            _path.Add(_current);

            Tile st = gameObject.GetComponent<Tile>();
            foreach(Tile t in _current.GetNeighbours())
            {
                    if(t.startWeight < st.startWeight) { st = t; }
            }

            _current = st;
        }


        _path.Add(_targetTile);
        request.Agent.SetPath(_path);

    }




 
    public void SetWeight(Transform target)
    {
        foreach (Transform b in map.GetTiles())
        {
            b.GetComponent<Tile>().startWeight = Vector3.Distance(b.position, target.position);  
        }
    }
  
}
