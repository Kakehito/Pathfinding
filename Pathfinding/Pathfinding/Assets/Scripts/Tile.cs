using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class Tile : MonoBehaviour
{
    CustomGrid gridManager;

    public float startWeight = 10000;    
    public float addedWeight = 10000;
    public float totalWeight = 10000;

    Transform neighbourAbove;
    Transform neighbourBelow;
    Transform neighbourLeft;
    Transform neighbourRight;

    public void SetNeighbours()
    {
        gridManager = GetComponentInParent<CustomGrid>();

        neighbourAbove = getAbove();
        neighbourBelow = getBelow();
        neighbourLeft = getLeft();
        neighbourRight = getRight();
    }

    public List<Tile> GetNeighbours()
    {
        List<Tile> t = new List<Tile>();
        if (neighbourAbove != null)
            t.Add(neighbourAbove.GetComponent<Tile>());
        if (neighbourBelow != null)
            t.Add(neighbourBelow.GetComponent<Tile>());
        if (neighbourLeft != null)
            t.Add(neighbourLeft.GetComponent<Tile>());
        if (neighbourRight != null)
            t.Add(neighbourRight.GetComponent<Tile>());
        return t;
    }

    public Tile GetLowestWeightTile()
    {
        Tile i = new Tile();
       
        foreach(Tile t in GetNeighbours()){ i = (t.totalWeight < i.totalWeight) ? t : i;}

        return i;
    }

    public Transform getAbove()
    {
        foreach(Transform t in gridManager.Tiles)
        {
            if (t.position == new Vector3(transform.position.x, transform.position.y, transform.position.z + 1))
            {
                return t;
            }
        }
        return null;
    }

    public Transform getBelow()
    {
        foreach (Transform t in gridManager.Tiles)
        {
            if (t.position == new Vector3(transform.position.x, transform.position.y, transform.position.z - 1))
            {
                return t;
            }
        }
        return null;
    }

    public Transform getLeft()
    {
        foreach (Transform t in gridManager.Tiles)
        {
            if (t.position == new Vector3(transform.position.x - 1, transform.position.y, transform.position.z))
            {
                return t;
            }
        }
        return null;
    }

    public Transform getRight()
    {
        foreach (Transform t in gridManager.Tiles)
        {
            if (t.position == new Vector3(transform.position.x + 1, transform.position.y, transform.position.z))
            {
                return t;
            }
        }
        return null;
    }

    public void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void Update()
    {
        startWeight = totalWeight + addedWeight;
    }

}
