using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHouseScript : MonoBehaviour
{

    private List<(GameObject, float)> exitsList = new List<(GameObject, float)>();

    private List<GameObject> currentRooms = new List<GameObject>();
    private int[,,] roomField = new int[18,3,18];

    [SerializeField] private int roomsCount;
    [SerializeField] private float roomSize;
    [SerializeField] private GameObject bedRoom;
    [SerializeField] private GameObject[] corridors;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject[] environment;

    void SpawnBedroom()
    {
        foreach(GameObject room in currentRooms)
            Destroy(room);
        GameObject currentRoom = Instantiate(bedRoom);
        currentRoom.transform.position = new Vector3(8 * roomSize, 0, 8 * roomSize);
        roomField = new int[18, 3, 18];
        roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % 18,
                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % 18,
                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % 18] = 1;

        exitsList.AddRange(currentRoom.GetComponent<RoomScript>().Exits);
        currentRooms.Add(currentRoom);
    }


    void Start()
    {
        if (roomSize == 0)
            throw new System.Exception("Ты больной что ли, какой размер конматы нулевой, у тебя юнити зависнет нахер, не делвй так");

        int randomExitNumber;
        GameObject currentRoom;
        SpawnBedroom();

        while (currentRooms.FindAll(x => x.tag == "Room").Count != roomsCount)
        {
            if (exitsList.Count == 0)
            {
                Debug.Log("Zero");
                SpawnBedroom();
            }
                
            
            randomExitNumber = Random.Range(0, exitsList.Count);

            if(Random.Range(0.0f, 1.0f) > 0.7)
            {
                currentRoom = Instantiate(rooms[Random.Range(0, rooms.Length)]);
            }
            else
                currentRoom = Instantiate(corridors[Random.Range(0, corridors.Length)]);


            currentRoom.transform.position = exitsList[randomExitNumber].Item1.transform.position;
            currentRoom.transform.Rotate(0.0f, exitsList[randomExitNumber].Item1.transform.parent.rotation.eulerAngles.y - exitsList[randomExitNumber].Item2, 0.0f, Space.Self);

            if (roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % 18,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % 18,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % 18] == 0)
            {
                roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % 18,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % 18,
                          (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % 18] = 1;

                exitsList.AddRange(currentRoom.GetComponent<RoomScript>().Exits);
                currentRooms.Add(currentRoom);
            }
            else
                Destroy(currentRoom);
                //Тут должен быть код заглушки в виде стены

            exitsList.RemoveAt(randomExitNumber);
        }

        //Тут должен быть код заглушек в виде окон

        //foreach((GameObject,float) remainingExit in exitsList)
        //{
        //    currentRoom = Instantiate(environment[Random.Range(0, environment.Length)]);

        //    currentRoom.transform.position = remainingExit.Item1.transform.position;
        //    currentRoom.transform.Rotate(0.0f, remainingExit.Item1.transform.parent.rotation.eulerAngles.y - remainingExit.Item2, 0.0f, Space.Self);

        //    if (roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % 32,
        //                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % 32,
        //                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % 32] == 0)
        //    {
        //        roomField[(int)(currentRoom.GetComponent<RoomScript>().Center.position.x / roomSize) % 32,
        //                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.y / roomSize) % 32,
        //                  (int)(currentRoom.GetComponent<RoomScript>().Center.position.z / roomSize) % 32] = 1;
        //    }
        //    else
        //        Destroy(currentRoom);

        //}
        for (int i = 0; i < roomField.GetLength(0); i++)
        {
            for (int j = 0; j < roomField.GetLength(2); j++)
            {
                if(roomField[i,0,j] == 0)
                {
                    currentRoom = Instantiate(environment[Random.Range(0, environment.Length)]);

                    currentRoom.transform.position = new Vector3(i * roomSize, 0, j * roomSize);
                }
            }
        }
    }


}
