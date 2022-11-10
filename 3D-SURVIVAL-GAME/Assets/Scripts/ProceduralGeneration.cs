using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] blockTypes;

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
       // blockTypes = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos == Vector3.zero || HasPlayerMoved())
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                        0,
                        z * planeOffset + ZPlayerLocation);


                    if (!tilePlane.Contains(pos))
                    {
                        //int index = Mathf.Abs(z + radius) % 4;
                        int index = Random.Range(0,4);
                        int randomAngle = Random.Range(1, 4) * 90;
                        RoadType road = new RoadType(blockTypes[index],pos,randomAngle);
                        GameObject _plane = road.InstatiateRoad();
                        tilePlane.Add(pos, road);
                    }
                }
            }
        }

    }

    bool HasPlayerMoved()
    {
        if (Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset)
        {
            return true;
        }
        return false;
    }

    public class RoadType{

        private GameObject gameObject;
        public string name;
        private Vector3 position;
        public float rotation;
        private Hashtable connections;


        public RoadType(GameObject gameObject, Vector3 pos , float rotation )
        {
            this.gameObject = gameObject;
            this.position = pos;
            this.rotation = rotation;
            this.name = gameObject.name;
            connections = new Hashtable();
            switch (name)
            {
                case "CrossRoads":
                    connections.Add("Up", true);
                    connections.Add("Down", true);
                    connections.Add("Left", true);
                    connections.Add("Right", true);
                    break;
                case "StraightRoad":
                    if (rotation / 90 >= 1)
                    {
                        connections.Add("Left", true);
                        connections.Add("Right", true);
                    }
                    else
                    {
                        connections.Add("Up", true);
                        connections.Add("Down", true);
                    }
                    break;
                case "RightTurn":
                    if(rotation == 0)
                    {
                        connections.Add("Down", true);
                        connections.Add("Right", true);
                    }
                    else if(rotation == 90)
                    {
                        connections.Add("Down", true);
                        connections.Add("Left", true);
                    }else if(rotation == 180)
                    {
                        connections.Add("Up", true);
                        connections.Add("Left", true);
                    }else if(rotation == 270)
                    {
                        connections.Add("Up", true);
                        connections.Add("Right", true);
                    }
                    break;
                case "TJunction":
                    connections.Add("Up", true);
                    connections.Add("Down", true);
                    connections.Add("Left", true);
                    connections.Add("Right", true);
                    if (rotation == 0)
                    {
                        connections["Left"] = false;
                    }
                    else if (rotation == 90)
                    {
                        connections["Up"] = false;
                    }
                    else if (rotation == 180)
                    {
                        connections["Right"] = false;
                    }
                    else if (rotation == 270)
                    {
                        connections["Down"] = false;
                    }
                    break;
                default:
                    break;
            }
              
        }

        public bool HasConnection(string direction)
        {
            return connections.ContainsKey(direction) ? (bool) connections[direction] : false;
        }

        public GameObject InstatiateRoad()
        {
            //this.position = pos;
            //this.rotation = rotation;

            this.gameObject = Instantiate(gameObject, position, Quaternion.identity);
            this.gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
            return this.gameObject;
        }

        public bool WillConnect(RoadType other, string direction, string otherDirection)
        {
            return HasConnection(direction) && other.HasConnection(otherDirection);
            
        }

    }

}
