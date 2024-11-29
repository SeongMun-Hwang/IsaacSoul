using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    //RoomControl
    bool isCleared = false;

    //Door Control
    public GameObject[] doors;
    bool isDoorOpened = false;

    //Enemy Control
    public List<GameObject> enemies;
    public List<GameObject> spawnableEnemyList;

    //Player Reward
    public GameObject rewardCanvas;
    public GameObject horizontalLayoutGroup;
    public List<GameObject> rewardCards;
    private void Start()
    {   
        if (!isCleared)
        {
            //시작 시 모든 몬스터 스폰에서 몬스터 리스트의 몬스터 생성
            Transform monsterSpawnPosition = transform.Find("MonsterSpawnPosition");
            if (monsterSpawnPosition != null)
            {
                foreach (Transform child in monsterSpawnPosition)
                {
                    Debug.Log(child);
                    int number = Random.Range(0, spawnableEnemyList.Count);
                    GameObject go = Instantiate(spawnableEnemyList[number], child.position, spawnableEnemyList[number].transform.rotation);
                    enemies.Add(go);
                }
            }
        }
    }
    void Update()
    {
        if (enemies.Count == 0 && !isDoorOpened)
        {
            isDoorOpened = true;
            isCleared = true;
            foreach(GameObject door in doors)
            {
                door.GetComponent<Animator>().SetTrigger("Open");
            }
            CreateRandomRewardCards();
        }
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }
    void CreateRandomRewardCards()
    {
        rewardCanvas.SetActive(true);
        Time.timeScale = 0f;
        List<GameObject> buttons = rewardCards;
        for(int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, buttons.Count);
            Instantiate(buttons[num], horizontalLayoutGroup.transform);
            buttons.RemoveAt(num);
        }
    }
}