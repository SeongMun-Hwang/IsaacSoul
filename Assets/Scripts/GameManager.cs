using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Row
{
    public List<int> columns = new List<int>(3);
}

public class GameManager : MonoBehaviour
{
    public List<Row> rows = new List<Row>(3);
    public List<GameObject> roomsPrefab;
    public int roomNumber;
    float horizontalDistance = 34;
    float verticalDistance = 22;

    public void CreateMap()
    {

    }
}