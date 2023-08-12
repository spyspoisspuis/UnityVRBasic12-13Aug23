using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController : MonoBehaviour
{

    private bool _teleportIsActive = false;

    private enum ControllerType {
        RightHand,
        LeftHand
    }

    [SerializeField] private ControllerType targetController;

    
    //References our Input Actions that we are using
    public InputActionAsset inputAction;

    //References the rayInteractor to be enabled/disabled later
    public XRRayInteractor rayInteractor;

    //References the Teleportation Provider so we can use it to teleport the Player in the event of a succesful teleport call
    public TeleportationProvider teleportationProvider;


    //Will reference the Thumbstick Input Action when the scene starts up
    private InputAction _thumbstickInputAction;

    //Stores Action for Teleport Mode Activate
    private InputAction _teleportActivate;

    //Stores Action for Teleport Mode Cancel
    private InputAction _teleportCancel;

    // Start is called before the first frame update
    void Start()
    {
         //We don't want the rayInteractor to on unless we're using the forward press on the thumbstick so we deactivate it here
        rayInteractor.enabled = false;

        //This will find the Action Map of our target controller for Teleport Mode Activate.
        //It will enable it and then subscribe itself to our OnTeleportActivate function
        _teleportActivate = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Activate");
        _teleportActivate.Enable();
        _teleportActivate.performed += OnTeleportActivate;

        //This will find the Action Map of our target controller for Teleport Mode Cancel.
        //It will enable it and then subscribe itself to our OnTeleportCancel function
        _teleportCancel = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Cancel");
        _teleportCancel.Enable();
        _teleportCancel.performed += OnTeleportCancel;


        //We grab this reference so we can use it to tell if the thumbstick is still being pressed
        _thumbstickInputAction = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Move");
        _thumbstickInputAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
         if (!_teleportIsActive)
        {
            return;
        }
        if (!rayInteractor.enabled)
        {
            return;
        }
        if (_thumbstickInputAction.triggered)
        {
            return;
        }

        // rayInteractor.enabled = false;
        // _teleportIsActive = false;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if (!_teleportIsActive)
        {
            rayInteractor.enabled = true;
            _teleportIsActive = true;
        }

    }

    //This is called when our Teleport Mode Cancel action map is triggered
    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        if (_teleportIsActive && rayInteractor.enabled == true)
        {
            if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit))
            {
                rayInteractor.enabled = false;
                _teleportIsActive = false;
                return;
            }
            if (raycastHit.collider.gameObject.tag != "Teleport")
            {

                rayInteractor.enabled = false;
                _teleportIsActive = false;
                return;
            }
            TeleportRequest teleportRequest = new TeleportRequest()
            {
                destinationPosition = raycastHit.point,
            };

            teleportationProvider.QueueTeleportRequest(teleportRequest);
            rayInteractor.enabled = false;
            _teleportIsActive = false;
        }

    }

    private void OnDestroy()
    {
        _teleportActivate.performed -= OnTeleportActivate;
        _teleportCancel.performed -= OnTeleportCancel;
    }
}
