using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Threading.Tasks;
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
    
    public List<PathRequest> RequestQueue;

    int currentrequest;

    [Button]
    public async void FufillRequest()
    {
        if (currentrequest == RequestQueue.Count) return;
        await Fufill(currentrequest);
        currentrequest++;
        FufillRequest();
    }


    private void Start()
    {
        RequestQueue = new List<PathRequest>();
        map = GetComponent<CustomGrid>();
    }


    private void Update()
    {
        if(currentrequest <= RequestQueue.Count - 1)
        {
            FufillRequest();
        }
    }


    public async Task Fufill(int req)
    {
        CalculatePath(RequestQueue[req]);
        await Task.Yield();
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
