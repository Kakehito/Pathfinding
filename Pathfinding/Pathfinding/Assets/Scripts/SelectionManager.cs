using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    PathfindingManager manager;
    CustomGrid grid;

    public ClickAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        manager = PathfindingManager.instance;
        grid = GetComponent<CustomGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.transform.tag == "Grid")
                {
                    grid.UpdateGrid();
                    grid.Arrange();
                    agent.TargetTile = hit.transform.GetComponent<Tile>();
                    agent.GetPath();
                }
            }
        }
    }
}
