using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHouseScript : MonoBehaviour
{

    private List<(GameObject, float)> exitsList = new List<(GameObject, float)>();

    private float[,,] roomField = new float[32,3,32];
    [SerializeField] private float roomRadius;
    [SerializeField] private GameObject bedRoom;
    [SerializeField] private GameObject[] corridors;

    void Start()
    {
        int randomExitNumber;
        int randomRoomNumber;
        
        
        GameObject currentRoom = Instantiate(bedRoom);
        currentRoom.transform.position = new Vector3(15 * roomRadius, 0, 15 * roomRadius);
        roomField[15, 0, 15] = 1;
        exitsList.AddRange(currentRoom.GetComponent<RoomScript>().Exits);
        

        for (int i = 0; i < 32; i++)
        {
            if (exitsList.Count == 0)
                break;

            randomExitNumber = Random.Range(0, exitsList.Count);
            randomRoomNumber = Random.Range(0, corridors.Length);
            if (roomField[(int)(exitsList[randomExitNumber].Item1.transform.position.x / roomRadius) % 32, 
                          (int)(exitsList[randomExitNumber].Item1.transform.position.y / roomRadius) % 32, 
                          (int)(exitsList[randomExitNumber].Item1.transform.position.z / roomRadius) % 32] == 0)
            {
                roomField[(int)(exitsList[randomExitNumber].Item1.transform.position.x / roomRadius) % 32,
                          (int)(exitsList[randomExitNumber].Item1.transform.position.y / roomRadius) % 32,
                          (int)(exitsList[randomExitNumber].Item1.transform.position.z / roomRadius) % 32] = 1;

                currentRoom = Instantiate(corridors[randomRoomNumber]);

                currentRoom.transform.position = exitsList[randomExitNumber].Item1.transform.position;
                currentRoom.transform.Rotate(0.0f, exitsList[randomExitNumber].Item1.transform.parent.rotation.eulerAngles.y - exitsList[randomExitNumber].Item2, 0.0f, Space.Self);

                exitsList.AddRange(currentRoom.GetComponent<RoomScript>().Exits);
            }

            exitsList.RemoveAt(randomExitNumber);
        }

    }


}
