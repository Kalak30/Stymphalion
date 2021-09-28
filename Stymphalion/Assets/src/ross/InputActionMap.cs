// GENERATED AUTOMATICALLY FROM 'Assets/src/ross/InputActionMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActionMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionMap"",
    ""maps"": [
        {
            ""name"": ""MainActionMap"",
            ""id"": ""f4da5bf8-53cf-4f48-b97c-a9752f4f2eff"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""df99a8e5-6ef0-4d13-a5a9-6c45c63223df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""75d17ecc-51be-42a2-8e8b-9d23984fcc2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ee7824ad-bbd7-46c0-a49c-c098113e5322"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""00c8447f-655f-432d-8c01-78d9d22de349"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f8c5255-10ea-42cf-af9e-a1fdd72eef27"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2d8c250e-ce8f-4fca-b362-686bcab63649"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""42059e7d-9661-45fc-a59f-2c0ea501a037"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4d8c1ffb-9ca6-4e49-b4c6-767b04a20aac"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainActionMap
        m_MainActionMap = asset.FindActionMap("MainActionMap", throwIfNotFound: true);
        m_MainActionMap_Movement = m_MainActionMap.FindAction("Movement", throwIfNotFound: true);
        m_MainActionMap_Interact = m_MainActionMap.FindAction("Interact", throwIfNotFound: true);
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

    // MainActionMap
    private readonly InputActionMap m_MainActionMap;
    private IMainActionMapActions m_MainActionMapActionsCallbackInterface;
    private readonly InputAction m_MainActionMap_Movement;
    private readonly InputAction m_MainActionMap_Interact;
    public struct MainActionMapActions
    {
        private @InputActionMap m_Wrapper;
        public MainActionMapActions(@InputActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MainActionMap_Movement;
        public InputAction @Interact => m_Wrapper.m_MainActionMap_Interact;
        public InputActionMap Get() { return m_Wrapper.m_MainActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IMainActionMapActions instance)
        {
            if (m_Wrapper.m_MainActionMapActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_MainActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public MainActionMapActions @MainActionMap => new MainActionMapActions(this);
    public interface IMainActionMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
