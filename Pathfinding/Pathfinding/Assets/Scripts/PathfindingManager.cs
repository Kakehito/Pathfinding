using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PathfindingManager : MonoBehaviour
{
    PathfindingManager manager;

    List<IAgent> agentQueue;

    private void Start()
    {
        manager = GetComponent<PathfindingManager>();   
    }

    #region Singleton
    public static PathfindingManager instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;        
    }
    #endregion

    #region Weight
    public Tile currentTarget;
    public CustomGrid map;

    [Button]
    public void SetWeight()
    {
        CalculateDistance(map.Tiles);
    }

    public void CalculateDistance(List<Transform> tileList)
    {
        foreach(Transform i in tileList)
        {
            i.GetComponent<Tile>().addedWeight = Vector3.Distance(i.position, currentTarget.transform.position);
        }        
    }
    #endregion

    public void MoveTo(Transform target)
    {
        currentTarget = target.GetComponent<Tile>();

        if (currentTarget != manager.currentTarget)
        {
            foreach (Tile t in currentTarget.ReturnTransformList())
            {
                if (t.totalWeight <= currentTarget.totalWeight)
                {
                    currentTarget = t;
                }
            }
        }
    }

    public Tile NextTile(Tile next)
    {
        List<Tile> pathway = new List<Tile>();
        Tile temp = new Tile();

        if (next != currentTarget)
        {
            foreach(Tile t in next.ReturnTransformList())
            {
                if (t.totalWeight <= temp.totalWeight)
                {
                    temp = t;                    
                }
            }   
            
            if(temp!= currentTarget)
            {
                NextTile(temp);
                return null;
            }
            else
            {
                NextTile(temp);
            }
        }
        
        return temp;
    }

    Tile currentTile = new Tile();

    public void Move()
    {
        List<Tile> tiles = new List<Tile>();        

        if (currentTile != currentTarget)
        {
            currentTile = NextTile();
        }
    }

    public void ReceivePathRequest(IAgent agent)
    {
        agentQueue.Add(agent); 
    }

    //public List<Transform> GivePathResult()
    public void GivePathResult(IAgent agent)
    {
        List<Transform> results = new List<Transform>();
        agent.SetPath(results);
        agentQueue.Remove(agent);
    }
}
