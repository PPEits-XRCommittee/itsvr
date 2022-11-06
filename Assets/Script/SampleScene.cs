using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks
{
    //InputField���C���X�y�N�^�[��Őݒu
    public InputField inputField;
    public GameObject Menupanel;
    public GameObject Voice;
    public Toggle Mutetoggle;
    public GameObject[] AvatarList;
    private void Start()
    {

    }
    public void login()
    {        // �v���C���[���g�̖��O��"Player"�ɐݒ肷��
        PhotonNetwork.NickName = "Player";
        if(inputField.text != "")
        {
            PhotonNetwork.NickName = inputField.text;
        }
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
        Menupanel.SetActive(false);
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("ITSRoom", new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        int no = Random.Range(0, 2);
        GameObject temp;
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
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