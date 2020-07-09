using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;           //포톤네트워크 핵심기능
using Photon.Realtime;      //포톤 서비스관련(룸옵션 디스커넥션 등)

//네트워크 매니저 : 룸(게임공간)으로 연결시켜주는 역할
//포톤네트워크 : 마스터서버 -> 로비(대기실) -> 룸(게임공간)
//MonoBehaviourPunCallbacks : 포톤서버 접속, 로비접속, 룸접속
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text infoText;         //network state
    [SerializeField] private Button connectButton;  //join room button

    private string gameVersion = "1";
    private void Awake()
    {
        Screen.SetResolution(800, 600, FullScreenMode.Windowed);
    }

    //네트워크 매니저가 실행되면 제일먼저 할일은?

    // Start is called before the first frame update
    void Start()
    {
        //접속에 필요한 정보(게임버전) 설정
        PhotonNetwork.GameVersion = gameVersion;

        //마스터서버에 접속하는 함수(very important)
        PhotonNetwork.ConnectUsingSettings();
        //접속시도중 텍스트표시
        infoText.text = "마스터 서버에 접속중...";
        //룸(게임공간) 접속 버튼 비활성화
        connectButton.interactable = false;
    }
    //마스터서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        //접속 정보 표시
        infoText.text = "ONLINE : Connect to Master Server";
        //룸(게임공간) 접속버튼 활성화
        connectButton.interactable = true;
    }

    //혹시나 시작하면서 마스터 서버에 접속 실패했을시 자동 실행된다.
    public override void OnDisconnected(DisconnectCause cause)
    {
        connectButton.interactable = false;

        infoText.text = "OFFLINE : Connection fail to Master Server";
        //마스터 서버에 접속하는 함수(중요)
        PhotonNetwork.ConnectUsingSettings();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConnect()
    {
        //중복접속 방지
        connectButton.interactable = false;

        //마스터 서버에 접속중이냐
        if(PhotonNetwork.IsConnected)
        {
            infoText.text = "Random Matching...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            infoText.text = "OFFLINE : Connection fail to Master Server \n re";
            PhotonNetwork.ConnectUsingSettings();

        }
    }
    public override void OnJoinedRoom()
    {
        infoText.text = "방 참가 성공";
        //모든 룸 참가자들이 "GameScene"을 로드
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        infoText.text = "빈 방이 없으니 새로운 방 생성중...";

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }
}
