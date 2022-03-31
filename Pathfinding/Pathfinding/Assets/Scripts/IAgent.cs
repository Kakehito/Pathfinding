using System.Collections.Generic;
using UnityEngine;
public interface IAgent
{    
    public void AskForPath();    
    public void SetPath(List<Transform> pathway);
}
