using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    public Transform[] spawnPoints;
    //해상도설정 해주는게 좋음
    private void Awake()
    {
        Screen.SetResolution(800, 600, FullScreenMode.Windowed);
    }

    //게임매니저는 사용자가 게임세상에 들어오면 플레이어 하나를 설정해준다
    //플레이어가 프리팹으로 만들어져있을때
    //프리팹은 반드시 Resources폴더 경로안에 들어있어야한
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreatePlayer()
    {
        int index = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate("Player", spawnPoints[index].position, spawnPoints[index].rotation);
    }
}
