using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Room
{
    public GameObject room;
    public Vector3 worldPos = Vector3.zero;
}

public class GameManager : MonoBehaviour
{
    public List<GameObject> roomsPrefab;
    public GameObject baseRoomPrefab;
    public int roomNumber;

    public int row = 10;
    public int col = 10;

    float horizontalDistance = 34;
    float verticalDistance = 22;

    private Room[,] roomGrid;
    private bool[,] isRoomPosDisabled;

    //확장 방향
    Vector2Int[] directions = new Vector2Int[]
{
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(1,0),
            new Vector2Int(-1,0),
};
    private void Start()
    {
        roomGrid = new Room[row, col];
        isRoomPosDisabled = new bool[row, col];
        //기본 방(몹x) 생성
        Room baseRoom = new Room();
        baseRoom.worldPos.x = 0;
        baseRoom.worldPos.y = 0;
        baseRoom.room = Instantiate(baseRoomPrefab, baseRoom.worldPos, Quaternion.identity);
        //배열 가운데 좌표 지정
        int centerX = row / 2;
        int centerY = col / 2;
        isRoomPosDisabled[centerX, centerY] = true;
        //맵 생성
        CreateMap(centerX, centerY);
    }
    public void CreateMap(int startX, int startY)
    {
        //DFS
        Stack<Vector2Int> currentRoomPos = new Stack<Vector2Int>();
        //기본 방 배열 인덱스 추가
        currentRoomPos.Push(new Vector2Int(startX, startY));
        //생성된 방 갯수(디폴트:기본 방)
        int createdRooms = 1;
        while (createdRooms < roomNumber) //생성된 방이 생성할 갯수보다 적으면
        {
            if (currentRoomPos.Count == 0) break; //방을 생성할 곳이 없으면

            Vector2Int currentPos = currentRoomPos.Peek(); //스택 위 좌표부터 방 생성
            Vector2Int dir = directions[Random.Range(0, directions.Length)]; //방 기준 상하좌우
            int nextX = currentPos.x + dir.x;
            int nextY = currentPos.y + dir.y;
            if (CheckNextRoomPosition(nextX, nextY)) //방 생성 가능한지 확인
            {
                GameObject go = roomsPrefab[Random.Range(0, roomsPrefab.Count)];
                CreateRoom(nextX, nextY, go);

                currentRoomPos.Push(new Vector2Int(nextX, nextY)); //생성하고 생성한 방 좌표 푸시
                createdRooms++;
            }
            else
            {
                isRoomPosDisabled[currentPos.x, currentPos.y] = true; //생성 불가 참
                currentRoomPos.Pop();
            }
        }
    }
    Room CreateRoom(int x, int y, GameObject room)
    {
        float worldPosX = (x - row / 2) * horizontalDistance;
        float worldPosY = (y - col / 2) * verticalDistance;

        Room newRoom = new Room
        {
            worldPos = new Vector3(worldPosX, worldPosY),
            room = Instantiate(room, new Vector3(worldPosX, worldPosY, 0), Quaternion.identity)
        };
        //newRoom.room.SetActive(false);
        roomGrid[x, y] = newRoom;
        isRoomPosDisabled[x, y] = true;

        return newRoom;
    }
    bool CheckNextRoomPosition(int x, int y)
    {
        if (x < 0 || x >= row || y < 0 || y >= col) return false;
        if (isRoomPosDisabled[x, y]) return false;
        int count = 0;
        foreach (Vector2Int dir in directions)
        {
            int nextX = x + dir.x;
            int nextY = y + dir.y;

            if (nextX >= 0 && nextX < row && nextY >= 0 && nextY < col && roomGrid[nextX, nextY] != null)
            {
                count++;
                if (count >= 3)
                {
                    return false;
                }
            }
        }
        return true;
    }
}