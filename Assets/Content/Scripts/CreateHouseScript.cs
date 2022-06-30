using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHouseScript : MonoBehaviour
{

    private List<(GameObject, float)> exitsList = new List<(GameObject, float)>();
    private List<(GameObject, float, int)> roomPlugList = new List<(GameObject, float, int)>();

    private List<GameObject> currentRooms = new List<GameObject>();
    private int[,,] roomField;

    [SerializeField, Range(16, 32)] private int fieldSize;
    [SerializeField, Range(4, 14)] private int roomsCount;
    [SerializeField] private float roomSize;
    [SerializeField] private GameObject bedRoom;
    [SerializeField] private GameObject[] corridors;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject[] environment;
    [SerializeField] private GameObject[] roomWallPlugs;
    [SerializeField] private GameObject[] roomWindowPlugs;

    [SerializeField] private GameObject[] coridorWallPlugs;
    [SerializeField] private GameObject[] coridorWindowPlugs;
    [SerializeField] private GameObject[] farEnvironment;

    [SerializeField] private GameObject coridorDoorPlug;
    [SerializeField] private GameObject outsideWallPlug;
    [SerializeField] private GameObject outsideWindowPlug;

    [SerializeField] private GameObject person;

    void SpawnBedroom()
    {
        exitsList.Clear();
        roomPlugList.Clear();
        foreach (GameObject room in currentRooms)
            Destroy(room);
        GameObject currentRoom = Instantiate(bedRoom);
        currentRoom.transform.position = new Vector3(((fieldSize/2)-1) * roomSize, 0, ((fieldSize / 2) - 1) * roomSize);
        roomField = new int[fieldSize, 1, fieldSize];
        roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % fieldSize,
                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % fieldSize,
                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % fieldSize] = 1;

        exitsList.AddRange(currentRoom.GetComponent<RoomScript>().Exits);
        currentRooms.Add(currentRoom); 
    }


    void SpawnRawRooms()
    {
        int randomExitNumber;
        int randomPlugNumber;
        GameObject currentRoom;
        SpawnBedroom();

        while (currentRooms.FindAll(x => x.tag == "Room").Count != roomsCount)
        {
            if (exitsList.Count == 0)
                SpawnBedroom();

            randomPlugNumber = -1;
            randomExitNumber = Random.Range(0, exitsList.Count);

            if (Random.Range(0.0f, 1.0f) > 0.7)
            {
                randomPlugNumber = Random.Range(0, rooms.Length);
                currentRoom = Instantiate(rooms[randomPlugNumber]);
            }
            else
                currentRoom = Instantiate(corridors[Random.Range(0, corridors.Length)]);


            currentRoom.transform.position = exitsList[randomExitNumber].Item1.transform.position;
            currentRoom.transform.Rotate(0.0f, exitsList[randomExitNumber].Item1.transform.parent.rotation.eulerAngles.y - exitsList[randomExitNumber].Item2, 0.0f, Space.Self);
            

            if (roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % fieldSize] == 0)
            {
                roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % fieldSize] = 1;

                exitsList.AddRange(currentRoom.GetComponent<RoomScript>().Exits);
                currentRooms.Add(currentRoom);
                if (randomPlugNumber != -1)
                {
                    foreach ((GameObject, float) exit in currentRoom.GetComponent<RoomScript>().Plugs)
                        roomPlugList.Add((exit.Item1, exit.Item2, randomPlugNumber));
                    Instantiate(coridorDoorPlug, 
                        exitsList[randomExitNumber].Item1.transform.position,
                        Quaternion.Euler(new Vector3(0.0f, exitsList[randomExitNumber].Item1.transform.parent.rotation.eulerAngles.y - exitsList[randomExitNumber].Item2, 0.0f)),
                        currentRoom.gameObject.transform);
                }
            }
            else
            {
                //exitsList[randomExitNumber].Item1.transform.parent.rotation.eulerAngles.y 
                Destroy(currentRoom);
                currentRoom = Instantiate(coridorWallPlugs[Random.Range(0, coridorWallPlugs.Length)], exitsList[randomExitNumber].Item1.transform.parent.transform);
                currentRoom.transform.position = exitsList[randomExitNumber].Item1.transform.position;
                currentRoom.transform.Rotate(0.0f, - exitsList[randomExitNumber].Item2, 0.0f, Space.Self);
            }


            exitsList.RemoveAt(randomExitNumber);
        }

        //corridors
        foreach ((GameObject, float) exit in exitsList)
        {
            currentRoom = Instantiate(corridors[0]);
            currentRoom.transform.position = exit.Item1.transform.position;
            currentRoom.transform.Rotate(0.0f, exit.Item1.transform.parent.rotation.eulerAngles.y - exit.Item2, 0.0f, Space.Self);
            if (roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % fieldSize] == 0)
            {
                Destroy(currentRoom);
                currentRoom = Instantiate(coridorWindowPlugs[Random.Range(0, coridorWindowPlugs.Length)]);
            }
            else
            {
                Destroy(currentRoom);
                currentRoom = Instantiate(coridorWallPlugs[Random.Range(0, coridorWallPlugs.Length)]);
            }
            currentRoom.transform.position = exit.Item1.transform.position;
            currentRoom.transform.Rotate(0.0f, exit.Item1.transform.parent.rotation.eulerAngles.y - exit.Item2, 0.0f, Space.Self);
        }

        //rooms
        foreach ((GameObject, float, int) exit in roomPlugList)
        {
            currentRoom = Instantiate(corridors[0]);
            currentRoom.transform.position = exit.Item1.transform.position;
            currentRoom.transform.Rotate(0.0f, exit.Item1.transform.parent.rotation.eulerAngles.y - exit.Item2, 0.0f, Space.Self);
            if (roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % fieldSize,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % fieldSize] == 0)
            {
                Destroy(currentRoom);
                currentRoom = Instantiate(roomWindowPlugs[exit.Item3]);
                
            }
            else
            {
                Destroy(currentRoom);
                currentRoom = Instantiate(roomWallPlugs[exit.Item3]);
            }
            currentRoom.transform.position = exit.Item1.transform.position;
            currentRoom.transform.Rotate(0.0f, exit.Item1.transform.parent.rotation.eulerAngles.y - exit.Item2, 0.0f, Space.Self);
        }


        for (int i = 0; i < roomField.GetLength(0); i++)
        {
            for (int j = 0; j < roomField.GetLength(2); j++)
            {
                if((i < 2 || j < 2 || i > fieldSize-2 || j > fieldSize - 2) && roomField[i, 0, j] == 0)
                {
                    Instantiate(farEnvironment[Random.Range(0, farEnvironment.Length)], new Vector3(i * roomSize, 0, j * roomSize),Quaternion.identity);
                }
                else if (roomField[i, 0, j] == 0)
                {
                    currentRoom = Instantiate(environment[Random.Range(0, environment.Length)]);

                    currentRoom.transform.position = new Vector3(i * roomSize, 0, j * roomSize);
                }
            }
        }
    }


    void Awake()
    {
        if (roomSize == 0)
            throw new System.Exception("Ты больной что ли, какой размер комнаты нулевой, у тебя юнити зависнет нахер, не делaй так");

        //roomField = new int[fieldSize, 1, fieldSize];

        SpawnRawRooms();

        Instantiate(person, new Vector3(((fieldSize / 2) - 1) * roomSize + 6f, 2.5f, ((fieldSize / 2) - 1) * roomSize), new Quaternion(0, 0, 0, 0));
    }


}
