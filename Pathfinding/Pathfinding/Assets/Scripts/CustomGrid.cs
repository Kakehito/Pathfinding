using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CustomGrid : MonoBehaviour
{
    public List<Transform> Tiles; 

    [OnValueChanged("Arrange")]
    public int width;

    public Transform currentSorted;    

    public void ArrangeRow(int startingIndex=0, int row=0)
    {
        int count=0;

        for (int i = startingIndex; i < width+startingIndex; i++)
        {
            Tiles[i].position = new Vector3(count, 0, row);

            if (i == Tiles.Count - 1)
                return;

            count++;
        }

        ArrangeRow(startingIndex + width, row+1);
    }

    [Button]
    public void Arrange()
    {
        ArrangeRow();

        foreach(Transform t in Tiles)
        {
            if(t.gameObject.GetComponent<Tile>()==null)
                t.gameObject.AddComponent<Tile>();

            t.GetComponent<Tile>().GetNeighbours();
            t.GetComponent<Tile>().ChangeColor();
        }        
    }

    [Button]
    public void UpdateGrid()
    {
        Tiles = new List<Transform>();

        foreach(Transform t in gameObject.GetComponentsInChildren<Transform>())
        {
            if(t!=transform)
                Tiles.Add(t);
        }        
    }

    public List<Transform> GetPath()
    {
        return Tiles;
    }
}
