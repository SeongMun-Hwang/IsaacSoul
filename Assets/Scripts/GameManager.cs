using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public float posX;
    public float posY;
    public GameObject room;
}

public class GameManager : MonoBehaviour
{
    public List<GameObject> roomsPrefab;
    public GameObject baseRoomPrefab;
    public int roomNumber;
    float horizontalDistance = 34;
    float verticalDistance = 22;

    private void Start()
    {
        Room baseRoom = new Room();
        baseRoom.posX = 0;
        baseRoom.posY = 0;
        baseRoom.room = Instantiate(baseRoomPrefab, transform.position, Quaternion.identity);

        CreateMap(baseRoom);
    }
    public void CreateMap(Room baseRoom)
    {
        List<Room> createdRooms = new List<Room> { baseRoom };
        Vector2[] directions = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(1, 0)
        };
        for (int i = 0; i < roomNumber; i++)
        {
   
            Room lastRoom = createdRooms[Random.Range(0, createdRooms.Count)];

            Vector2 randomDirection = directions[Random.Range(0, directions.Length)];

            float newX = lastRoom.posX + randomDirection.x * horizontalDistance;
            float newY = lastRoom.posY + randomDirection.y * verticalDistance;

            if (createdRooms.Exists(room => room.posX == newX && room.posY == newY))
            {
                i--;
                continue;
            }

            Room newRoom = new Room();
            newRoom.posX = newX;
            newRoom.posY = newY;
            GameObject randomRoomPrefab = roomsPrefab[Random.Range(0, roomsPrefab.Count)];
            newRoom.room = Instantiate(randomRoomPrefab, new Vector3(newX, newY, 0), Quaternion.identity);

            createdRooms.Add(newRoom);
        }
    }
}