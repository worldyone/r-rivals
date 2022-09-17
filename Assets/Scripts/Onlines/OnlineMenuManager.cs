using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineMenuManager : MonoBehaviourPunCallbacks
{
    bool isInRoom;
    bool isMatching;

    public void OnMatchingButton()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        isInRoom = true;

        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate("heart", position, Quaternion.identity);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    // 部屋が2人ならシーンを変える
    void Update()
    {
        if (isMatching) return;

        if (isInRoom)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isMatching = true;
                SceneManager.LoadScene("Game");
            }
        }
    }
}
