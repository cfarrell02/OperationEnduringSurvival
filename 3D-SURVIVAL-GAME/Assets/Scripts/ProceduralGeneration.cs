using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] blockTypes;

    [SerializeField] private int radius = 5, planeOffset = 25;

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
       // int randomAngle = 0;
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
                        int index = Random.Range(0, 4);
                      //index = 2;
                      //print(index);
                        int randomAngle = Random.Range(0, 4) * 90;
                      //randomAngle = (randomAngle + 90) % 360;
                        RoadType road = new RoadType(blockTypes[index], pos, randomAngle);
                        GameObject _plane = road.InstatiateRoad();
                        tilePlane.Add(pos, road);
                    }
                }
            }
            foreach(RoadType road in tilePlane.Values)
            {
                if (!Connected(road))
                {
                    RoadType clone = road;
                    road.DestroyGameObject();
                    int lastIndex = blockTypes.Length - 1;
                    RoadType newRoad = new RoadType(blockTypes[lastIndex], clone.position, clone.rotation);
                    newRoad.InstatiateRoad();
                    tilePlane[clone.position] = newRoad;
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

    bool Connected(RoadType road)
    {
       // if (road.name == "NoRoad") return true;
        Vector3 position = road.position;
        Vector3[] surroundingPositions = new Vector3[4];
        surroundingPositions[0] = road.position + new Vector3(planeOffset, 0, 0); //Up
        surroundingPositions[1] = road.position + new Vector3(-planeOffset, 0, 0);//Down
        surroundingPositions[2] = road.position + new Vector3(0, 0, planeOffset);//Left
        surroundingPositions[3] = road.position + new Vector3(0, 0, -planeOffset);//Right
        bool isConnected = false;
        for(int i =0;i<surroundingPositions.Length;++i)
        {
            if (tilePlane.ContainsKey(surroundingPositions[i]))
            {
                RoadType otherRoad = (RoadType) tilePlane[surroundingPositions[i]];
                switch (i)
                {
                    case 0:
                        isConnected = road.WillConnect(otherRoad, "Up", "Down");
                        break;
                    case 1:
                        isConnected = road.WillConnect(otherRoad, "Down", "Up");
                        break;
                    case 2:
                        isConnected = road.WillConnect(otherRoad, "Left", "Right");
                        break;
                    case 3:
                        isConnected = road.WillConnect(otherRoad, "Right", "Left");
                        break;
                }
                if (isConnected) return true;
            }
        }

        return isConnected;
    }

    public class RoadType{

        private GameObject gameObject;
        public string name;
        public Vector3 position;
        public float rotation;
        private Hashtable connections;


        public RoadType(GameObject gameObject, Vector3 pos , float rotation )
        {
            this.gameObject = gameObject;
            this.position = pos;
            this.rotation = rotation;
            this.name = gameObject.name.Split(' ')[0];
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
                    if (rotation == 90 || rotation == 270)
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
            }
              
        }

        public bool HasConnection(string direction)
        {
            return connections.ContainsKey(direction) ? (bool) connections[direction] : false;
        }

        public GameObject InstatiateRoad()
        {
            this.gameObject = Instantiate(gameObject, position, Quaternion.Euler(0,rotation,0));
            return this.gameObject;
        }

        public void DestroyGameObject()
        {
            Destroy(this.gameObject);
        }

        public bool WillConnect(RoadType other, string direction, string otherDirection)
        {
            print("Type " + name + " with rotation " + rotation);
            print("This " + direction + " other " + otherDirection);
            print("Connected with " + other.name + " with rotation " + other.rotation + ": ");
            print(HasConnection(direction) && other.HasConnection(otherDirection));
            return HasConnection(direction) && other.HasConnection(otherDirection);
            
        }

    }

}
