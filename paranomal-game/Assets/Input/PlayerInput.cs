//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""OnFoot"",
            ""id"": ""a19730c1-ff11-4810-8f98-759b1bb1778d"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c8f12201-3010-4660-a97f-4638f4a2438e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""8cf83510-7e7d-4e67-8562-fdee47d00699"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryWeaponSwap"",
                    ""type"": ""Value"",
                    ""id"": ""1a2ebeb7-e619-403a-ab7d-d351b60b98a9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondaryWeaponSwap"",
                    ""type"": ""Value"",
                    ""id"": ""1f62b1e4-6146-482b-b848-b1fa15da0224"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""3f4a0dc6-78bb-4ba5-876b-77d6523b013a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""e554a739-623b-47b7-81b0-f2e757165665"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectFiringMode"",
                    ""type"": ""Button"",
                    ""id"": ""b072d7ca-265b-4eb0-b13f-b2c64fda2f35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b9496146-5da0-48c9-afeb-b3afcb28d255"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4d53f98-8e54-4461-954e-9f88c8ac6ad0"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4bb08d4-10c3-4229-a7dd-5a216767f62f"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""495a25ec-18eb-419b-8b0e-ad9973efc2f6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c18426ba-916b-4c8d-8de5-794a480d3e88"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryWeaponSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03c50442-b38f-4919-bb75-2b74a20e9c71"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryWeaponSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a6c5cf4-b749-42da-bd44-50b8235368c9"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryWeaponSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7825e1a5-5eb3-4193-829a-4d42b32e5362"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryWeaponSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9264796d-7234-4423-bcee-c273655c91c3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""012c1280-5c11-4e5b-92b2-ef9a01d51556"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14094b7a-f1db-4540-85d9-f37f9ccae60b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e71f5321-9b4e-4964-8954-cd60c35169c9"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectFiringMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // OnFoot
        m_OnFoot = asset.FindActionMap("OnFoot", throwIfNotFound: true);
        m_OnFoot_Interact = m_OnFoot.FindAction("Interact", throwIfNotFound: true);
        m_OnFoot_DropItem = m_OnFoot.FindAction("DropItem", throwIfNotFound: true);
        m_OnFoot_PrimaryWeaponSwap = m_OnFoot.FindAction("PrimaryWeaponSwap", throwIfNotFound: true);
        m_OnFoot_SecondaryWeaponSwap = m_OnFoot.FindAction("SecondaryWeaponSwap", throwIfNotFound: true);
        m_OnFoot_Shoot = m_OnFoot.FindAction("Shoot", throwIfNotFound: true);
        m_OnFoot_Reload = m_OnFoot.FindAction("Reload", throwIfNotFound: true);
        m_OnFoot_SelectFiringMode = m_OnFoot.FindAction("SelectFiringMode", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // OnFoot
    private readonly InputActionMap m_OnFoot;
    private IOnFootActions m_OnFootActionsCallbackInterface;
    private readonly InputAction m_OnFoot_Interact;
    private readonly InputAction m_OnFoot_DropItem;
    private readonly InputAction m_OnFoot_PrimaryWeaponSwap;
    private readonly InputAction m_OnFoot_SecondaryWeaponSwap;
    private readonly InputAction m_OnFoot_Shoot;
    private readonly InputAction m_OnFoot_Reload;
    private readonly InputAction m_OnFoot_SelectFiringMode;
    public struct OnFootActions
    {
        private @PlayerInput m_Wrapper;
        public OnFootActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_OnFoot_Interact;
        public InputAction @DropItem => m_Wrapper.m_OnFoot_DropItem;
        public InputAction @PrimaryWeaponSwap => m_Wrapper.m_OnFoot_PrimaryWeaponSwap;
        public InputAction @SecondaryWeaponSwap => m_Wrapper.m_OnFoot_SecondaryWeaponSwap;
        public InputAction @Shoot => m_Wrapper.m_OnFoot_Shoot;
        public InputAction @Reload => m_Wrapper.m_OnFoot_Reload;
        public InputAction @SelectFiringMode => m_Wrapper.m_OnFoot_SelectFiringMode;
        public InputActionMap Get() { return m_Wrapper.m_OnFoot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnFootActions set) { return set.Get(); }
        public void SetCallbacks(IOnFootActions instance)
        {
            if (m_Wrapper.m_OnFootActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnInteract;
                @DropItem.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnDropItem;
                @PrimaryWeaponSwap.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnPrimaryWeaponSwap;
                @PrimaryWeaponSwap.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnPrimaryWeaponSwap;
                @PrimaryWeaponSwap.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnPrimaryWeaponSwap;
                @SecondaryWeaponSwap.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnSecondaryWeaponSwap;
                @SecondaryWeaponSwap.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnSecondaryWeaponSwap;
                @SecondaryWeaponSwap.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnSecondaryWeaponSwap;
                @Shoot.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnShoot;
                @Reload.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnReload;
                @SelectFiringMode.started -= m_Wrapper.m_OnFootActionsCallbackInterface.OnSelectFiringMode;
                @SelectFiringMode.performed -= m_Wrapper.m_OnFootActionsCallbackInterface.OnSelectFiringMode;
                @SelectFiringMode.canceled -= m_Wrapper.m_OnFootActionsCallbackInterface.OnSelectFiringMode;
            }
            m_Wrapper.m_OnFootActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @PrimaryWeaponSwap.started += instance.OnPrimaryWeaponSwap;
                @PrimaryWeaponSwap.performed += instance.OnPrimaryWeaponSwap;
                @PrimaryWeaponSwap.canceled += instance.OnPrimaryWeaponSwap;
                @SecondaryWeaponSwap.started += instance.OnSecondaryWeaponSwap;
                @SecondaryWeaponSwap.performed += instance.OnSecondaryWeaponSwap;
                @SecondaryWeaponSwap.canceled += instance.OnSecondaryWeaponSwap;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @SelectFiringMode.started += instance.OnSelectFiringMode;
                @SelectFiringMode.performed += instance.OnSelectFiringMode;
                @SelectFiringMode.canceled += instance.OnSelectFiringMode;
            }
        }
    }
    public OnFootActions @OnFoot => new OnFootActions(this);
    public interface IOnFootActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
        void OnPrimaryWeaponSwap(InputAction.CallbackContext context);
        void OnSecondaryWeaponSwap(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSelectFiringMode(InputAction.CallbackContext context);
    }
}
