using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CamPlayerMain : MonoBehaviour
{

    private FirstPersonController firstPersonController;
    private ThirdPersonController thirdPersonController;
    private OrthographicController orthographicController;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;
    private Camera defaultCamera;

    [SerializeField]
    private GameObject ButtonsHolder;

    [SerializeField]
    private Button FirstPersonCamActivator;

    [SerializeField]
    private Button ThirdPersonCamActivator;

    [SerializeField]
    private Button OrthographicCamActivator;

    public delegate void OnCameraChanged(Camera newCamera);

    public static event OnCameraChanged CameraChanged;

    //private void OnEnable()
    //{
    //    CharacterInstantiation.CharacterInstantiated += this.OnCharacterInstantiated;
    //    PhotonNetwork.AddCallbackTarget(this);
    //}

    //private void OnDisable()
    //{
    //    CharacterInstantiation.CharacterInstantiated -= this.OnCharacterInstantiated;
    //    PhotonNetwork.RemoveCallbackTarget(this);
    //}


    private void Start()
    {
        this.defaultCamera = Camera.main;
        this.initialCameraPosition = new Vector3(this.defaultCamera.transform.position.x,
            this.defaultCamera.transform.position.y, this.defaultCamera.transform.position.z);
        this.initialCameraRotation = new Quaternion(this.defaultCamera.transform.rotation.x,
            this.defaultCamera.transform.rotation.y, this.defaultCamera.transform.rotation.z,
            this.defaultCamera.transform.rotation.w);
        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        this.FirstPersonCamActivator.onClick.AddListener(this.FirstPersonMode);
#else
            this.FirstPersonCamActivator.gameObject.SetActive(false);
#endif
        this.ThirdPersonCamActivator.onClick.AddListener(this.ThirdPersonMode);
        this.OrthographicCamActivator.onClick.AddListener(this.OrthographicMode);
    }

    public void OnCharacterInstantiated(GameObject character)
    {
        this.firstPersonController = character.GetComponent<FirstPersonController>();
        this.firstPersonController.enabled = false;
        this.thirdPersonController = character.GetComponent<ThirdPersonController>();
        this.thirdPersonController.enabled = false;
        this.orthographicController = character.GetComponent<OrthographicController>();
        this.ButtonsHolder.SetActive(true);
    }

    private void FirstPersonMode()
    {
        this.ToggleMode(this.firstPersonController);
    }

    private void ThirdPersonMode()
    {
        this.ToggleMode(this.thirdPersonController);
    }

    private void OrthographicMode()
    {
        this.ToggleMode(this.orthographicController);
    }

    private void ToggleMode(BaseController controller)
    {
        if (controller == null) { return; } // this should not happen, throw error
        if (controller.ControllerCamera == null) { return; } // probably game is closing
        controller.ControllerCamera.gameObject.SetActive(true);
        controller.enabled = true;
        this.FirstPersonCamActivator.interactable = !(controller == this.firstPersonController);
        this.ThirdPersonCamActivator.interactable = !(controller == this.thirdPersonController);
        this.OrthographicCamActivator.interactable = !(controller == this.orthographicController);
        this.BroadcastChange(controller.ControllerCamera); // BroadcastChange(Camera.main);
    }

    private void BroadcastChange(Camera camera)
    {
        if (camera == null) { return; } // should not happen, throw error
        if (CameraChanged != null)
        {
            CameraChanged(camera);
        }
    }
}
