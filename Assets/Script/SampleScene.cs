using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    //InputFieldをインスペクター上で設置
    public InputField inputField;
    public GameObject Menupanel;
    public GameObject Voice;
    public Toggle Mutetoggle;
    public GameObject[] AvatarList;
    private void Start()
    {

    }
    public void login()
    {        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";
        if(inputField.text != "")
        {
            PhotonNetwork.NickName = inputField.text;
        }
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        Menupanel.SetActive(false);
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("ITSRoom", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        int no = Random.Range(0, 2);
        GameObject temp;
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(-3f, 3f), 1,Random.Range(-3f, 3f));
        temp = PhotonNetwork.Instantiate(AvatarList[no].name, position, Quaternion.identity);
        //voiceCoonect.SetActive(true);
        this.GetComponent<CamPlayerMain>().OnCharacterInstantiated(temp);

    }
    public void OvMuteToggle()
    {
        Voice.GetComponent<Recorder>().TransmitEnabled = Mutetoggle.isOn;
    }
}