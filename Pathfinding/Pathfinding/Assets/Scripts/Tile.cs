using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Tile : MonoBehaviour
{
    CustomGrid gridManager;

    public float startWeight;    
    public float addedWeight;
    public float totalWeight;

    public Transform neighbourAbove;
    public Transform neighbourBelow;
    public Transform neighbourLeft;
    public Transform neighbourRight;

    public void GetNeighbours()
    {
        gridManager = GetComponentInParent<CustomGrid>();

        neighbourAbove = getAbove();
        neighbourBelow = getBelow();
        neighbourLeft = getLeft();
        neighbourRight = getRight();
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
        totalWeight = startWeight + addedWeight;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.blue, Color.red, totalWeight * 0.12f);
    }

    public List<Tile> ReturnTransformList()
    {
        List<Tile> t = new List<Tile>();
        if (neighbourAbove != null)
        t.Add(neighbourAbove.GetComponent<Tile>());
        if(neighbourBelow != null)
        t.Add(neighbourBelow.GetComponent<Tile>());
        if(neighbourLeft != null)
        t.Add(neighbourLeft.GetComponent<Tile>());
        if(neighbourRight != null)
        t.Add(neighbourRight.GetComponent<Tile>());
        return t;
    }
}
