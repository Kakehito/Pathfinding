using System.Collections.Generic;
using UnityEngine;
public interface IAgent
{    
    public void GetPath();    
    public void SetPath(List<Tile> pathway);
}
