//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Game/Core/Scripts/Input/InputMaster.inputactions
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

public partial class @InputMaster : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""9946ecd4-7734-4e8b-9060-a79317a09632"",
            ""actions"": [
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""544bc790-6ff5-4152-826d-63d65524f5cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""508b6a2e-5884-4176-aa28-c9c4fb1b4ea3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""8cffa909-c25d-4b83-b38b-44fc41288aec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""80696487-020c-4b42-96ad-8aa805ab0b6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""84961efe-fd39-4b7a-ac1d-b6ceba88a3f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""3d78456a-9bc2-4a97-96df-afb910825455"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""F"",
                    ""type"": ""Button"",
                    ""id"": ""6cee28ed-bf09-4e21-a6fe-f18bc2747d76"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""2c58bf6e-222b-4326-9a38-7e4660dbfb0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a12fb0e0-d362-44f4-86ba-a36fc15d4a63"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3128132-db5f-483b-b773-adf02ea3d272"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48dfc12a-d7ec-42a7-b8f9-c6802e02eed7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc1f438a-f13f-4014-92ce-9d8f58ba2f37"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d21957bc-7a3f-4bdd-abf3-692f630a3a3d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d1cbcd8-f425-4cd9-8c16-297d2e0fc8e9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f60563c-fe51-4d96-9f76-4f184d095d5c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da2ab123-2a0d-459a-9e26-0cb0c052015f"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_Forward = m_Keyboard.FindAction("Forward", throwIfNotFound: true);
        m_Keyboard_Back = m_Keyboard.FindAction("Back", throwIfNotFound: true);
        m_Keyboard_Right = m_Keyboard.FindAction("Right", throwIfNotFound: true);
        m_Keyboard_Left = m_Keyboard.FindAction("Left", throwIfNotFound: true);
        m_Keyboard_Space = m_Keyboard.FindAction("Space", throwIfNotFound: true);
        m_Keyboard_Tab = m_Keyboard.FindAction("Tab", throwIfNotFound: true);
        m_Keyboard_F = m_Keyboard.FindAction("F", throwIfNotFound: true);
        m_Keyboard_ESC = m_Keyboard.FindAction("ESC", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_Forward;
    private readonly InputAction m_Keyboard_Back;
    private readonly InputAction m_Keyboard_Right;
    private readonly InputAction m_Keyboard_Left;
    private readonly InputAction m_Keyboard_Space;
    private readonly InputAction m_Keyboard_Tab;
    private readonly InputAction m_Keyboard_F;
    private readonly InputAction m_Keyboard_ESC;
    public struct KeyboardActions
    {
        private @InputMaster m_Wrapper;
        public KeyboardActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Forward => m_Wrapper.m_Keyboard_Forward;
        public InputAction @Back => m_Wrapper.m_Keyboard_Back;
        public InputAction @Right => m_Wrapper.m_Keyboard_Right;
        public InputAction @Left => m_Wrapper.m_Keyboard_Left;
        public InputAction @Space => m_Wrapper.m_Keyboard_Space;
        public InputAction @Tab => m_Wrapper.m_Keyboard_Tab;
        public InputAction @F => m_Wrapper.m_Keyboard_F;
        public InputAction @ESC => m_Wrapper.m_Keyboard_ESC;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @Forward.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnForward;
                @Forward.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnForward;
                @Forward.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnForward;
                @Back.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnBack;
                @Right.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnRight;
                @Left.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeft;
                @Space.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSpace;
                @Space.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSpace;
                @Space.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSpace;
                @Tab.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnTab;
                @F.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnF;
                @F.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnF;
                @F.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnF;
                @ESC.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnESC;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Forward.started += instance.OnForward;
                @Forward.performed += instance.OnForward;
                @Forward.canceled += instance.OnForward;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Space.started += instance.OnSpace;
                @Space.performed += instance.OnSpace;
                @Space.canceled += instance.OnSpace;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @F.started += instance.OnF;
                @F.performed += instance.OnF;
                @F.canceled += instance.OnF;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnForward(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnSpace(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnF(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
    }
}
