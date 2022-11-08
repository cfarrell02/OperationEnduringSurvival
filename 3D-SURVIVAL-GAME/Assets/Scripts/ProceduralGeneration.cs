using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject player, crossRoads, straightRoad, noRoad, bend;
    private GameObject[] blockTypes;

    private int radius = 5, planeOffset = 25;

    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)(Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset);
    private int ZPlayerLocation => (int)(Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset);

    private Hashtable tilePlane = new Hashtable();


    // Start is called before the first frame update
    void Start()
    {
        blockTypes = new GameObject[4];
        blockTypes[0] = crossRoads;
        blockTypes[1] = straightRoad;
        blockTypes[2] = noRoad;
        blockTypes[3] = bend;
    }

    // Update is called once per frame
    void Update()
    {
        if(startPos == Vector3.zero || HasPlayerMoved())
        {
            for(int x = -radius; x< radius; x++)
            {
                for(int z = -radius; z<radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                        0,
                        z * planeOffset + ZPlayerLocation);

                    if (!tilePlane.Contains(pos))
                    {
                        //int index = Mathf.Abs(z + radius) % 4;
                        int index = ((int)player.transform.position.x) % 4;
                        print(index);
                        GameObject _plane = Instantiate(blockTypes[index], pos, Quaternion.identity);
                        tilePlane.Add(pos, _plane);
                    }
                }
            }
        }

    }

    bool HasPlayerMoved()
    {
        if(Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }

}
