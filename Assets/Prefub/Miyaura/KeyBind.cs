// GENERATED AUTOMATICALLY FROM 'Assets/Prefub/Miyaura/KeyBind.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KeyBind : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KeyBind()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""KeyBind"",
    ""maps"": [
        {
            ""name"": ""MyPlayerControls"",
            ""id"": ""7dc609f6-a46a-4399-b429-b6402bb17d74"",
            ""actions"": [
                {
                    ""name"": ""AnyKey"",
                    ""type"": ""Button"",
                    ""id"": ""95b2505d-c487-45f1-8318-0bc726b460e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ac18c1c6-1645-411a-814b-363c192245fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""ffec3df2-77dd-436f-a98e-b9d0a6d98177"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""de3ad16a-8eb9-4f5c-87d9-023d134457ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""GetItem"",
                    ""type"": ""Button"",
                    ""id"": ""183c40b5-d8d7-405f-9575-3566131ae62c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ActionItem"",
                    ""type"": ""Button"",
                    ""id"": ""44faff10-18c6-446b-8818-b011729be49e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressArrowUp"",
                    ""type"": ""Button"",
                    ""id"": ""6f41d33f-510e-4e13-bc4b-8b12b7ab17fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressArrowDown"",
                    ""type"": ""Button"",
                    ""id"": ""7bc08558-8ef4-4383-b429-764890f24518"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressArrowLeft"",
                    ""type"": ""Button"",
                    ""id"": ""90525f51-4841-44e2-aed6-4f228ab1f536"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressArrowRight"",
                    ""type"": ""Button"",
                    ""id"": ""50821082-bd97-4ab0-9c4e-3d7a15901cd4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ReleaseArrowUp"",
                    ""type"": ""Button"",
                    ""id"": ""860b4145-47d5-45b8-9ac2-2301dde80c31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""ReleaseArrowDown"",
                    ""type"": ""Button"",
                    ""id"": ""23477947-9f1e-4dcb-a369-54e32507a459"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""ReleaseArrowLeft"",
                    ""type"": ""Button"",
                    ""id"": ""02120a90-e5a0-4911-9f21-39dd73de8ca2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""ReleaseArrowRight"",
                    ""type"": ""Button"",
                    ""id"": ""f6046956-1a87-4c88-8d41-9986db498b23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""HoldArrowUp"",
                    ""type"": ""Button"",
                    ""id"": ""4ed2a168-e689-4ad0-b854-057623a8ed0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""HoldArrowDown"",
                    ""type"": ""Button"",
                    ""id"": ""86fea6be-c393-4206-b811-dd80bd97bfc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""HoldArrowLeft"",
                    ""type"": ""Button"",
                    ""id"": ""ea8c8f03-e04a-4c80-8fbd-dee7992d8f2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""HoldArrowRight"",
                    ""type"": ""Button"",
                    ""id"": ""73abe09a-9a39-4e63-9c98-3dbac403dffd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""PressNorthButton"",
                    ""type"": ""Button"",
                    ""id"": ""57fab01b-1448-44e4-be6a-b03f9c29db37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressSouthButton"",
                    ""type"": ""Button"",
                    ""id"": ""20e1ad2f-28fb-43bb-a45e-8ce3a3d187aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressWestButton"",
                    ""type"": ""Button"",
                    ""id"": ""538c3f33-bdef-46f7-a140-232e224ad829"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PressEastButton"",
                    ""type"": ""Button"",
                    ""id"": ""b85b91d9-f524-4f42-9315-2671efe23594"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ReleaseNorthButton"",
                    ""type"": ""Button"",
                    ""id"": ""3793872c-5a55-429e-a626-9be17a2c46ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""ReleaseSouthButton"",
                    ""type"": ""Button"",
                    ""id"": ""44f2840a-18d1-4f1d-a72a-adfdf293a21d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""ReleaseWestButton"",
                    ""type"": ""Button"",
                    ""id"": ""775fa03a-4d44-4e5e-8c88-116292733ff8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""ReleaseEastButton"",
                    ""type"": ""Button"",
                    ""id"": ""5e71f471-c473-427c-9216-4f8b54cb11f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""HoldNorthButton"",
                    ""type"": ""Button"",
                    ""id"": ""1358d75d-015c-441b-9280-b496cbae5555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""HoldSouthButton"",
                    ""type"": ""Button"",
                    ""id"": ""639bc48e-b817-4d0e-9b60-895a2c864637"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""HoldWestButton"",
                    ""type"": ""Button"",
                    ""id"": ""8e7177fb-9ae4-4935-9bcf-60747de0599b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""HoldEastButton"",
                    ""type"": ""Button"",
                    ""id"": ""d083cb43-9eb8-45dd-a293-cbb92caa4366"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""LeftTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""6d1738ea-e1d3-416e-8d43-7058b75eccf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LeftShoulder"",
                    ""type"": ""Button"",
                    ""id"": ""9869eb48-027d-4870-aa88-1928782d4135"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RightTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""9bdefa60-b947-4790-b8dd-7832c3e3f7f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RightShoulder"",
                    ""type"": ""Button"",
                    ""id"": ""6e90b468-6d48-4483-9d6c-3e3f499e7797"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3bdc2a72-ca64-48b0-bbb1-99fe85410308"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ee52e4f-9cd0-4e52-b91c-b8261874926d"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f00a84f-14d2-4c4e-a223-b1b5aa8add39"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d340d38-b738-4548-a8ad-0247cb2d6c51"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36dc38c7-5cc0-4683-abba-82b546a5832a"",
                    ""path"": ""<DualShockGamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76a5da83-aca7-4f7e-983b-4eaf70231d46"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""324cf6f4-8a54-4216-86e6-cf722286969b"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cee59f56-8477-49d7-9710-5ed124890b76"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c631cef7-64f9-4b28-b899-fa781c6e92ec"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95cd024f-0822-47ec-8d99-b4f07029d6af"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d05a213-ddf1-4f8d-9961-4248842c004a"",
                    ""path"": ""<DualShockGamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98af515f-db37-4b1e-9f0b-861815f7d072"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7271a3f-10bf-4a2f-be7d-bd95bf7efc33"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df2dcc02-970b-412d-8f62-0ad2c608432b"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ab2388a-b98d-4d9c-98a4-a3b4a1614bfd"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2207279-f490-4f32-939b-9b2b60043d74"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea84bf2c-c9cd-4cf6-b961-aab28687978a"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""557695f8-ecd7-436a-946d-a36b088ed658"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93fd7a88-2e1f-45c5-a511-48330895620f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d1e8106-47ff-4dd9-9a51-e9c9351715cb"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c938850-8e66-4a64-966f-861838f4b154"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac37cb04-fc1b-429b-a16c-a78b82e870aa"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48fb4680-4acf-4cad-a988-80185093860e"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c36dbcdd-1baf-4957-b2dd-e6bf12650bd9"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RightShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f9a4cbd-a66d-46b1-af50-e85c30feb7dd"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0535e7f-e2ce-45fc-a338-b8eb08aabb6c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e12f9d59-00c2-477c-ae79-bb3218f3e4a5"",
                    ""path"": ""<DualShockGamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8d367f4-d577-4111-8b85-3882cabe9873"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12f54fb7-3f68-40ff-a0e7-5e1f7902540b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4ec0c14-ffa6-47c8-99a3-61419f1c3ccd"",
                    ""path"": ""<DualShockGamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""669e7d51-a59d-4e36-8e35-ad49d5e29b48"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b592310e-7594-4d0a-a14c-10d68fc749cd"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f301f26-adc3-4aed-85ab-da3a97185b45"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2883e297-50ac-4b33-a8fa-f39d9798ffcc"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80dcd580-d0e1-4e2a-826c-ec21a88f9475"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LeftShoulder"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a714c29-7c23-4e5f-9530-9be724407e7d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f978edb-6e51-4361-83ef-e25803c87e0f"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""effa55ab-5a15-48b2-a6ff-cd2368a33337"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eaad4fa7-edc7-45ac-9061-b1b57d8abc33"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PressArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e685b8c-6001-4701-ac4d-e28a757b137a"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ff998ee-f8f0-400a-847e-9b4c8d72e197"",
                    ""path"": ""<DualShockGamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PressArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1af0db3-6f4e-4c35-bb52-6e35beeb8dbd"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44e41f5d-5bb7-47f4-a4e4-91f18549666c"",
                    ""path"": ""<DualShockGamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c644922-e88a-41b8-b3c6-9a3fe8053fe4"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5c03501-53f4-4ba6-b28a-75841425f77a"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb8cdd10-e335-455d-ae6a-375e19a8ecbc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c70f8d9-20f4-4bb8-9ed3-9c4399e9e04e"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2363b98d-7d37-4e17-a00d-5ceb8f8c8647"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8957e6fd-5421-4aff-ad62-24177db94e90"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ActionItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5776648-2ea7-43f9-9444-52e5fc70c35c"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ActionItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79c80703-96db-45ca-a522-939c484aeeb9"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ActionItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b261135-0976-4355-a436-88fd12ba28e3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""GetItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b4b1db4-4cc4-4f20-89a8-e792d5ba0d51"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GetItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92e9ae7a-ad51-4cec-bab9-e557e8a97361"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GetItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a5872d65-4001-42b7-a409-1c67a7cd2e91"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e36016d1-1699-45ff-85ff-5ee61bb434fd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""872c2a8d-37ad-409f-821f-b66333e735b5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e9719784-3310-4f2e-bd82-1359c57a6710"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9f266be1-9b68-40f7-892d-487029691ba7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c9eb4865-7b18-42f1-bbc4-53d97ed6abaf"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7de3afd4-f006-468e-86f2-e90176c5f7d3"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cef82e7-a4eb-4491-b62e-43245c728490"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""92c6125d-5de3-409a-b08c-0bf5402d4bac"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8632a0d6-55ff-4c34-8564-25b6383b66c6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9969d31a-24f8-4079-9abb-a4f04837c57f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f8985a37-5c30-4c66-883a-80bad2ddc1ca"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""383ef31e-81e3-45e0-8b2c-a1b4fb595f6f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""83a24818-7672-4b87-973c-edc9d46fac9c"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7e1921e-9409-4366-a52e-758e48679e31"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c72bd8b-6cb1-4148-841b-decd49c87ffa"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ade858ee-2d00-4427-a65a-1f013975ecb0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a2cd829-7ed9-4181-b71c-0e02e183b41f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8520b2a5-bef3-4792-aa26-2af4ce038e27"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f1f4e6d-1884-4cb8-828a-3aebfd952af7"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2817d48-23e3-4b1a-b214-27cd1e1a4bb6"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""519a9519-8acb-4cbf-869f-98c1a4eb5f6c"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c525572-d8fe-4703-a49c-70591e37ea59"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53d5928b-b79a-46dc-8513-bc3c0cc3cda4"",
                    ""path"": ""<DualShockGamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4ef07f2-f162-4d78-bf89-c55a6a345998"",
                    ""path"": ""<DualShockGamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4342500-33f7-4818-b260-e29ea3b59780"",
                    ""path"": ""<DualShockGamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99aea41a-ff3b-4d35-93fe-1e3f5241152b"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e53e4a89-e8d9-4eb4-860e-623ffb00972b"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5562552a-c031-41a9-8818-9d43abe0d231"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c763400a-b6ae-4da3-a21a-0204d352cc68"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5c3a7de-9374-4159-9f74-76ba22d33178"",
                    ""path"": ""<DualShockGamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ea7abb4-9885-4bd1-81ad-401b72dc4054"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bebae60c-da14-48ac-a9c9-4461f85cf9d3"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e0c8b05-06b9-4e79-83ea-bacd95b98e74"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88d1b7f5-17e2-43fd-9f8c-0046e58d4f8d"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02064931-cf9d-4cc4-a3b5-02684c21dc7e"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""695c8b8a-20f6-4ebf-9286-8046ed785d38"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fceca3fa-5a78-4f2d-8517-cd6391f9d75f"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41458dcd-8ede-43be-a2e1-c782b9c65068"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33c249c3-c054-4da9-8c61-c3e538919a36"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44f4c674-bf8d-46ce-a3c0-ed09fb359a8b"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b506982-5678-4599-aee5-2a5a8ae40466"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74e752ac-8739-4b1d-ba64-2986b8858164"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cf8cd7d-3fb3-4f4b-b82e-69bf1a185548"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0dbc35d-8aa0-45f3-ae0f-1c0c24ef48f3"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""673977e6-fbe0-44e0-9139-a3aa9cb48cac"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfa4f717-84bf-48a4-a106-b3219e9a205e"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AnyKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25c371da-9d0c-446f-993e-637e979fe4d5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c82a758b-86ed-456a-ace4-90beee23f57a"",
                    ""path"": ""<DualShockGamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8377a80b-410b-4edf-8a46-eea7577f4396"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a93dc8c9-f12a-451a-bf4f-ec5b8c30ce7f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e481af65-a33a-4b5e-92ad-5bbca16944c0"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""374f4d16-7207-4471-afe8-85bec4b0a8c6"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ba6a7d3-7603-4040-8aae-5d8e4f05fe87"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afc7ed50-67a0-4b2f-981c-62b7e5754d4c"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b847b24c-5bff-4ab4-b842-27669aec82db"",
                    ""path"": ""<DualShockGamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84b3d058-fcd1-40b4-a97b-6f6852c1b08c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""871c1365-f81c-4b66-b0bd-59471b48deb8"",
                    ""path"": ""<DualShockGamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79b14e65-123b-49a8-9fb0-b0fc48e28f72"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseArrowRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60245c4b-5a24-431d-b3ab-03f5d24dd2b7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ec0a0fe-059a-4034-9231-a65942bbf5af"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""229a306b-f19c-47ad-a775-5ad4cfde5a1e"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dfbdb78-0ebc-4dbd-a4da-6f3f825ccabc"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HoldSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97ebea27-7f12-429b-bece-abc35b463c29"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51b7a136-a876-4738-850d-c83c53e3868a"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32a2867b-ba70-464b-adae-b885745f6e75"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d3aaeda-b825-4f1d-aff6-617d25aae5fb"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70aca3ac-8918-413d-b54b-6a44e25bcd7f"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldNorthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f48cd6e-d5b3-4d6c-98a6-3a7b5edd82b1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a43033d-7805-4ec1-9a53-5ec3e922c16f"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c383ed9-9e78-4cee-892e-ba6a6c5fd2bc"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3638f497-1c85-405b-9a74-3dae4e3d21c0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HoldEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9226258-ad1e-4c5b-8c95-2f1ce4fa7f18"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffbfb04b-d0f2-4daf-ab42-45a1c0e704b8"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HoldEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8173b09b-f423-4cbe-953c-ec3e93af1db1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReleaseSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f5921cb-54a9-4915-a29c-c06cb1589d44"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9b05cc8-3dae-495a-a242-cd7aa890425b"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseSouthButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca649995-4eec-4ffb-8bba-d25a50de326c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d039d80-f2c0-442d-b8a6-4cf17124d748"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""912f8e14-6038-4a64-960e-f630262771a3"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseEastButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40645692-cddc-4e4f-89d1-64ad56701eab"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ReleaseWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""142da898-779a-4667-9fc2-735c32b01cba"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""213d440b-f32d-4068-a30d-d6b109022950"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ReleaseWestButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard And Mouse"",
            ""bindingGroup"": ""Keyboard And Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MyPlayerControls
        m_MyPlayerControls = asset.FindActionMap("MyPlayerControls", throwIfNotFound: true);
        m_MyPlayerControls_AnyKey = m_MyPlayerControls.FindAction("AnyKey", throwIfNotFound: true);
        m_MyPlayerControls_Move = m_MyPlayerControls.FindAction("Move", throwIfNotFound: true);
        m_MyPlayerControls_Look = m_MyPlayerControls.FindAction("Look", throwIfNotFound: true);
        m_MyPlayerControls_Submit = m_MyPlayerControls.FindAction("Submit", throwIfNotFound: true);
        m_MyPlayerControls_GetItem = m_MyPlayerControls.FindAction("GetItem", throwIfNotFound: true);
        m_MyPlayerControls_ActionItem = m_MyPlayerControls.FindAction("ActionItem", throwIfNotFound: true);
        m_MyPlayerControls_PressArrowUp = m_MyPlayerControls.FindAction("PressArrowUp", throwIfNotFound: true);
        m_MyPlayerControls_PressArrowDown = m_MyPlayerControls.FindAction("PressArrowDown", throwIfNotFound: true);
        m_MyPlayerControls_PressArrowLeft = m_MyPlayerControls.FindAction("PressArrowLeft", throwIfNotFound: true);
        m_MyPlayerControls_PressArrowRight = m_MyPlayerControls.FindAction("PressArrowRight", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseArrowUp = m_MyPlayerControls.FindAction("ReleaseArrowUp", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseArrowDown = m_MyPlayerControls.FindAction("ReleaseArrowDown", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseArrowLeft = m_MyPlayerControls.FindAction("ReleaseArrowLeft", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseArrowRight = m_MyPlayerControls.FindAction("ReleaseArrowRight", throwIfNotFound: true);
        m_MyPlayerControls_HoldArrowUp = m_MyPlayerControls.FindAction("HoldArrowUp", throwIfNotFound: true);
        m_MyPlayerControls_HoldArrowDown = m_MyPlayerControls.FindAction("HoldArrowDown", throwIfNotFound: true);
        m_MyPlayerControls_HoldArrowLeft = m_MyPlayerControls.FindAction("HoldArrowLeft", throwIfNotFound: true);
        m_MyPlayerControls_HoldArrowRight = m_MyPlayerControls.FindAction("HoldArrowRight", throwIfNotFound: true);
        m_MyPlayerControls_PressNorthButton = m_MyPlayerControls.FindAction("PressNorthButton", throwIfNotFound: true);
        m_MyPlayerControls_PressSouthButton = m_MyPlayerControls.FindAction("PressSouthButton", throwIfNotFound: true);
        m_MyPlayerControls_PressWestButton = m_MyPlayerControls.FindAction("PressWestButton", throwIfNotFound: true);
        m_MyPlayerControls_PressEastButton = m_MyPlayerControls.FindAction("PressEastButton", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseNorthButton = m_MyPlayerControls.FindAction("ReleaseNorthButton", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseSouthButton = m_MyPlayerControls.FindAction("ReleaseSouthButton", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseWestButton = m_MyPlayerControls.FindAction("ReleaseWestButton", throwIfNotFound: true);
        m_MyPlayerControls_ReleaseEastButton = m_MyPlayerControls.FindAction("ReleaseEastButton", throwIfNotFound: true);
        m_MyPlayerControls_HoldNorthButton = m_MyPlayerControls.FindAction("HoldNorthButton", throwIfNotFound: true);
        m_MyPlayerControls_HoldSouthButton = m_MyPlayerControls.FindAction("HoldSouthButton", throwIfNotFound: true);
        m_MyPlayerControls_HoldWestButton = m_MyPlayerControls.FindAction("HoldWestButton", throwIfNotFound: true);
        m_MyPlayerControls_HoldEastButton = m_MyPlayerControls.FindAction("HoldEastButton", throwIfNotFound: true);
        m_MyPlayerControls_LeftTrigger = m_MyPlayerControls.FindAction("LeftTrigger", throwIfNotFound: true);
        m_MyPlayerControls_LeftShoulder = m_MyPlayerControls.FindAction("LeftShoulder", throwIfNotFound: true);
        m_MyPlayerControls_RightTrigger = m_MyPlayerControls.FindAction("RightTrigger", throwIfNotFound: true);
        m_MyPlayerControls_RightShoulder = m_MyPlayerControls.FindAction("RightShoulder", throwIfNotFound: true);
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

    // MyPlayerControls
    private readonly InputActionMap m_MyPlayerControls;
    private IMyPlayerControlsActions m_MyPlayerControlsActionsCallbackInterface;
    private readonly InputAction m_MyPlayerControls_AnyKey;
    private readonly InputAction m_MyPlayerControls_Move;
    private readonly InputAction m_MyPlayerControls_Look;
    private readonly InputAction m_MyPlayerControls_Submit;
    private readonly InputAction m_MyPlayerControls_GetItem;
    private readonly InputAction m_MyPlayerControls_ActionItem;
    private readonly InputAction m_MyPlayerControls_PressArrowUp;
    private readonly InputAction m_MyPlayerControls_PressArrowDown;
    private readonly InputAction m_MyPlayerControls_PressArrowLeft;
    private readonly InputAction m_MyPlayerControls_PressArrowRight;
    private readonly InputAction m_MyPlayerControls_ReleaseArrowUp;
    private readonly InputAction m_MyPlayerControls_ReleaseArrowDown;
    private readonly InputAction m_MyPlayerControls_ReleaseArrowLeft;
    private readonly InputAction m_MyPlayerControls_ReleaseArrowRight;
    private readonly InputAction m_MyPlayerControls_HoldArrowUp;
    private readonly InputAction m_MyPlayerControls_HoldArrowDown;
    private readonly InputAction m_MyPlayerControls_HoldArrowLeft;
    private readonly InputAction m_MyPlayerControls_HoldArrowRight;
    private readonly InputAction m_MyPlayerControls_PressNorthButton;
    private readonly InputAction m_MyPlayerControls_PressSouthButton;
    private readonly InputAction m_MyPlayerControls_PressWestButton;
    private readonly InputAction m_MyPlayerControls_PressEastButton;
    private readonly InputAction m_MyPlayerControls_ReleaseNorthButton;
    private readonly InputAction m_MyPlayerControls_ReleaseSouthButton;
    private readonly InputAction m_MyPlayerControls_ReleaseWestButton;
    private readonly InputAction m_MyPlayerControls_ReleaseEastButton;
    private readonly InputAction m_MyPlayerControls_HoldNorthButton;
    private readonly InputAction m_MyPlayerControls_HoldSouthButton;
    private readonly InputAction m_MyPlayerControls_HoldWestButton;
    private readonly InputAction m_MyPlayerControls_HoldEastButton;
    private readonly InputAction m_MyPlayerControls_LeftTrigger;
    private readonly InputAction m_MyPlayerControls_LeftShoulder;
    private readonly InputAction m_MyPlayerControls_RightTrigger;
    private readonly InputAction m_MyPlayerControls_RightShoulder;
    public struct MyPlayerControlsActions
    {
        private @KeyBind m_Wrapper;
        public MyPlayerControlsActions(@KeyBind wrapper) { m_Wrapper = wrapper; }
        public InputAction @AnyKey => m_Wrapper.m_MyPlayerControls_AnyKey;
        public InputAction @Move => m_Wrapper.m_MyPlayerControls_Move;
        public InputAction @Look => m_Wrapper.m_MyPlayerControls_Look;
        public InputAction @Submit => m_Wrapper.m_MyPlayerControls_Submit;
        public InputAction @GetItem => m_Wrapper.m_MyPlayerControls_GetItem;
        public InputAction @ActionItem => m_Wrapper.m_MyPlayerControls_ActionItem;
        public InputAction @PressArrowUp => m_Wrapper.m_MyPlayerControls_PressArrowUp;
        public InputAction @PressArrowDown => m_Wrapper.m_MyPlayerControls_PressArrowDown;
        public InputAction @PressArrowLeft => m_Wrapper.m_MyPlayerControls_PressArrowLeft;
        public InputAction @PressArrowRight => m_Wrapper.m_MyPlayerControls_PressArrowRight;
        public InputAction @ReleaseArrowUp => m_Wrapper.m_MyPlayerControls_ReleaseArrowUp;
        public InputAction @ReleaseArrowDown => m_Wrapper.m_MyPlayerControls_ReleaseArrowDown;
        public InputAction @ReleaseArrowLeft => m_Wrapper.m_MyPlayerControls_ReleaseArrowLeft;
        public InputAction @ReleaseArrowRight => m_Wrapper.m_MyPlayerControls_ReleaseArrowRight;
        public InputAction @HoldArrowUp => m_Wrapper.m_MyPlayerControls_HoldArrowUp;
        public InputAction @HoldArrowDown => m_Wrapper.m_MyPlayerControls_HoldArrowDown;
        public InputAction @HoldArrowLeft => m_Wrapper.m_MyPlayerControls_HoldArrowLeft;
        public InputAction @HoldArrowRight => m_Wrapper.m_MyPlayerControls_HoldArrowRight;
        public InputAction @PressNorthButton => m_Wrapper.m_MyPlayerControls_PressNorthButton;
        public InputAction @PressSouthButton => m_Wrapper.m_MyPlayerControls_PressSouthButton;
        public InputAction @PressWestButton => m_Wrapper.m_MyPlayerControls_PressWestButton;
        public InputAction @PressEastButton => m_Wrapper.m_MyPlayerControls_PressEastButton;
        public InputAction @ReleaseNorthButton => m_Wrapper.m_MyPlayerControls_ReleaseNorthButton;
        public InputAction @ReleaseSouthButton => m_Wrapper.m_MyPlayerControls_ReleaseSouthButton;
        public InputAction @ReleaseWestButton => m_Wrapper.m_MyPlayerControls_ReleaseWestButton;
        public InputAction @ReleaseEastButton => m_Wrapper.m_MyPlayerControls_ReleaseEastButton;
        public InputAction @HoldNorthButton => m_Wrapper.m_MyPlayerControls_HoldNorthButton;
        public InputAction @HoldSouthButton => m_Wrapper.m_MyPlayerControls_HoldSouthButton;
        public InputAction @HoldWestButton => m_Wrapper.m_MyPlayerControls_HoldWestButton;
        public InputAction @HoldEastButton => m_Wrapper.m_MyPlayerControls_HoldEastButton;
        public InputAction @LeftTrigger => m_Wrapper.m_MyPlayerControls_LeftTrigger;
        public InputAction @LeftShoulder => m_Wrapper.m_MyPlayerControls_LeftShoulder;
        public InputAction @RightTrigger => m_Wrapper.m_MyPlayerControls_RightTrigger;
        public InputAction @RightShoulder => m_Wrapper.m_MyPlayerControls_RightShoulder;
        public InputActionMap Get() { return m_Wrapper.m_MyPlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MyPlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMyPlayerControlsActions instance)
        {
            if (m_Wrapper.m_MyPlayerControlsActionsCallbackInterface != null)
            {
                @AnyKey.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnAnyKey;
                @AnyKey.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnAnyKey;
                @AnyKey.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnAnyKey;
                @Move.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLook;
                @Submit.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnSubmit;
                @GetItem.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnGetItem;
                @GetItem.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnGetItem;
                @GetItem.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnGetItem;
                @ActionItem.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnActionItem;
                @ActionItem.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnActionItem;
                @ActionItem.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnActionItem;
                @PressArrowUp.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowUp;
                @PressArrowUp.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowUp;
                @PressArrowUp.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowUp;
                @PressArrowDown.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowDown;
                @PressArrowDown.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowDown;
                @PressArrowDown.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowDown;
                @PressArrowLeft.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowLeft;
                @PressArrowLeft.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowLeft;
                @PressArrowLeft.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowLeft;
                @PressArrowRight.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowRight;
                @PressArrowRight.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowRight;
                @PressArrowRight.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressArrowRight;
                @ReleaseArrowUp.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowUp;
                @ReleaseArrowUp.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowUp;
                @ReleaseArrowUp.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowUp;
                @ReleaseArrowDown.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowDown;
                @ReleaseArrowDown.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowDown;
                @ReleaseArrowDown.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowDown;
                @ReleaseArrowLeft.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowLeft;
                @ReleaseArrowLeft.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowLeft;
                @ReleaseArrowLeft.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowLeft;
                @ReleaseArrowRight.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowRight;
                @ReleaseArrowRight.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowRight;
                @ReleaseArrowRight.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseArrowRight;
                @HoldArrowUp.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowUp;
                @HoldArrowUp.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowUp;
                @HoldArrowUp.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowUp;
                @HoldArrowDown.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowDown;
                @HoldArrowDown.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowDown;
                @HoldArrowDown.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowDown;
                @HoldArrowLeft.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowLeft;
                @HoldArrowLeft.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowLeft;
                @HoldArrowLeft.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowLeft;
                @HoldArrowRight.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowRight;
                @HoldArrowRight.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowRight;
                @HoldArrowRight.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldArrowRight;
                @PressNorthButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressNorthButton;
                @PressNorthButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressNorthButton;
                @PressNorthButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressNorthButton;
                @PressSouthButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressSouthButton;
                @PressSouthButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressSouthButton;
                @PressSouthButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressSouthButton;
                @PressWestButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressWestButton;
                @PressWestButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressWestButton;
                @PressWestButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressWestButton;
                @PressEastButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressEastButton;
                @PressEastButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressEastButton;
                @PressEastButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnPressEastButton;
                @ReleaseNorthButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseNorthButton;
                @ReleaseNorthButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseNorthButton;
                @ReleaseNorthButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseNorthButton;
                @ReleaseSouthButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseSouthButton;
                @ReleaseSouthButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseSouthButton;
                @ReleaseSouthButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseSouthButton;
                @ReleaseWestButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseWestButton;
                @ReleaseWestButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseWestButton;
                @ReleaseWestButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseWestButton;
                @ReleaseEastButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseEastButton;
                @ReleaseEastButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseEastButton;
                @ReleaseEastButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnReleaseEastButton;
                @HoldNorthButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldNorthButton;
                @HoldNorthButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldNorthButton;
                @HoldNorthButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldNorthButton;
                @HoldSouthButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldSouthButton;
                @HoldSouthButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldSouthButton;
                @HoldSouthButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldSouthButton;
                @HoldWestButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldWestButton;
                @HoldWestButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldWestButton;
                @HoldWestButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldWestButton;
                @HoldEastButton.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldEastButton;
                @HoldEastButton.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldEastButton;
                @HoldEastButton.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnHoldEastButton;
                @LeftTrigger.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLeftTrigger;
                @LeftTrigger.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLeftTrigger;
                @LeftTrigger.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLeftTrigger;
                @LeftShoulder.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLeftShoulder;
                @LeftShoulder.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLeftShoulder;
                @LeftShoulder.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnLeftShoulder;
                @RightTrigger.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnRightTrigger;
                @RightTrigger.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnRightTrigger;
                @RightTrigger.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnRightTrigger;
                @RightShoulder.started -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnRightShoulder;
                @RightShoulder.performed -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnRightShoulder;
                @RightShoulder.canceled -= m_Wrapper.m_MyPlayerControlsActionsCallbackInterface.OnRightShoulder;
            }
            m_Wrapper.m_MyPlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AnyKey.started += instance.OnAnyKey;
                @AnyKey.performed += instance.OnAnyKey;
                @AnyKey.canceled += instance.OnAnyKey;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @GetItem.started += instance.OnGetItem;
                @GetItem.performed += instance.OnGetItem;
                @GetItem.canceled += instance.OnGetItem;
                @ActionItem.started += instance.OnActionItem;
                @ActionItem.performed += instance.OnActionItem;
                @ActionItem.canceled += instance.OnActionItem;
                @PressArrowUp.started += instance.OnPressArrowUp;
                @PressArrowUp.performed += instance.OnPressArrowUp;
                @PressArrowUp.canceled += instance.OnPressArrowUp;
                @PressArrowDown.started += instance.OnPressArrowDown;
                @PressArrowDown.performed += instance.OnPressArrowDown;
                @PressArrowDown.canceled += instance.OnPressArrowDown;
                @PressArrowLeft.started += instance.OnPressArrowLeft;
                @PressArrowLeft.performed += instance.OnPressArrowLeft;
                @PressArrowLeft.canceled += instance.OnPressArrowLeft;
                @PressArrowRight.started += instance.OnPressArrowRight;
                @PressArrowRight.performed += instance.OnPressArrowRight;
                @PressArrowRight.canceled += instance.OnPressArrowRight;
                @ReleaseArrowUp.started += instance.OnReleaseArrowUp;
                @ReleaseArrowUp.performed += instance.OnReleaseArrowUp;
                @ReleaseArrowUp.canceled += instance.OnReleaseArrowUp;
                @ReleaseArrowDown.started += instance.OnReleaseArrowDown;
                @ReleaseArrowDown.performed += instance.OnReleaseArrowDown;
                @ReleaseArrowDown.canceled += instance.OnReleaseArrowDown;
                @ReleaseArrowLeft.started += instance.OnReleaseArrowLeft;
                @ReleaseArrowLeft.performed += instance.OnReleaseArrowLeft;
                @ReleaseArrowLeft.canceled += instance.OnReleaseArrowLeft;
                @ReleaseArrowRight.started += instance.OnReleaseArrowRight;
                @ReleaseArrowRight.performed += instance.OnReleaseArrowRight;
                @ReleaseArrowRight.canceled += instance.OnReleaseArrowRight;
                @HoldArrowUp.started += instance.OnHoldArrowUp;
                @HoldArrowUp.performed += instance.OnHoldArrowUp;
                @HoldArrowUp.canceled += instance.OnHoldArrowUp;
                @HoldArrowDown.started += instance.OnHoldArrowDown;
                @HoldArrowDown.performed += instance.OnHoldArrowDown;
                @HoldArrowDown.canceled += instance.OnHoldArrowDown;
                @HoldArrowLeft.started += instance.OnHoldArrowLeft;
                @HoldArrowLeft.performed += instance.OnHoldArrowLeft;
                @HoldArrowLeft.canceled += instance.OnHoldArrowLeft;
                @HoldArrowRight.started += instance.OnHoldArrowRight;
                @HoldArrowRight.performed += instance.OnHoldArrowRight;
                @HoldArrowRight.canceled += instance.OnHoldArrowRight;
                @PressNorthButton.started += instance.OnPressNorthButton;
                @PressNorthButton.performed += instance.OnPressNorthButton;
                @PressNorthButton.canceled += instance.OnPressNorthButton;
                @PressSouthButton.started += instance.OnPressSouthButton;
                @PressSouthButton.performed += instance.OnPressSouthButton;
                @PressSouthButton.canceled += instance.OnPressSouthButton;
                @PressWestButton.started += instance.OnPressWestButton;
                @PressWestButton.performed += instance.OnPressWestButton;
                @PressWestButton.canceled += instance.OnPressWestButton;
                @PressEastButton.started += instance.OnPressEastButton;
                @PressEastButton.performed += instance.OnPressEastButton;
                @PressEastButton.canceled += instance.OnPressEastButton;
                @ReleaseNorthButton.started += instance.OnReleaseNorthButton;
                @ReleaseNorthButton.performed += instance.OnReleaseNorthButton;
                @ReleaseNorthButton.canceled += instance.OnReleaseNorthButton;
                @ReleaseSouthButton.started += instance.OnReleaseSouthButton;
                @ReleaseSouthButton.performed += instance.OnReleaseSouthButton;
                @ReleaseSouthButton.canceled += instance.OnReleaseSouthButton;
                @ReleaseWestButton.started += instance.OnReleaseWestButton;
                @ReleaseWestButton.performed += instance.OnReleaseWestButton;
                @ReleaseWestButton.canceled += instance.OnReleaseWestButton;
                @ReleaseEastButton.started += instance.OnReleaseEastButton;
                @ReleaseEastButton.performed += instance.OnReleaseEastButton;
                @ReleaseEastButton.canceled += instance.OnReleaseEastButton;
                @HoldNorthButton.started += instance.OnHoldNorthButton;
                @HoldNorthButton.performed += instance.OnHoldNorthButton;
                @HoldNorthButton.canceled += instance.OnHoldNorthButton;
                @HoldSouthButton.started += instance.OnHoldSouthButton;
                @HoldSouthButton.performed += instance.OnHoldSouthButton;
                @HoldSouthButton.canceled += instance.OnHoldSouthButton;
                @HoldWestButton.started += instance.OnHoldWestButton;
                @HoldWestButton.performed += instance.OnHoldWestButton;
                @HoldWestButton.canceled += instance.OnHoldWestButton;
                @HoldEastButton.started += instance.OnHoldEastButton;
                @HoldEastButton.performed += instance.OnHoldEastButton;
                @HoldEastButton.canceled += instance.OnHoldEastButton;
                @LeftTrigger.started += instance.OnLeftTrigger;
                @LeftTrigger.performed += instance.OnLeftTrigger;
                @LeftTrigger.canceled += instance.OnLeftTrigger;
                @LeftShoulder.started += instance.OnLeftShoulder;
                @LeftShoulder.performed += instance.OnLeftShoulder;
                @LeftShoulder.canceled += instance.OnLeftShoulder;
                @RightTrigger.started += instance.OnRightTrigger;
                @RightTrigger.performed += instance.OnRightTrigger;
                @RightTrigger.canceled += instance.OnRightTrigger;
                @RightShoulder.started += instance.OnRightShoulder;
                @RightShoulder.performed += instance.OnRightShoulder;
                @RightShoulder.canceled += instance.OnRightShoulder;
            }
        }
    }
    public MyPlayerControlsActions @MyPlayerControls => new MyPlayerControlsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard And Mouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    public interface IMyPlayerControlsActions
    {
        void OnAnyKey(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnGetItem(InputAction.CallbackContext context);
        void OnActionItem(InputAction.CallbackContext context);
        void OnPressArrowUp(InputAction.CallbackContext context);
        void OnPressArrowDown(InputAction.CallbackContext context);
        void OnPressArrowLeft(InputAction.CallbackContext context);
        void OnPressArrowRight(InputAction.CallbackContext context);
        void OnReleaseArrowUp(InputAction.CallbackContext context);
        void OnReleaseArrowDown(InputAction.CallbackContext context);
        void OnReleaseArrowLeft(InputAction.CallbackContext context);
        void OnReleaseArrowRight(InputAction.CallbackContext context);
        void OnHoldArrowUp(InputAction.CallbackContext context);
        void OnHoldArrowDown(InputAction.CallbackContext context);
        void OnHoldArrowLeft(InputAction.CallbackContext context);
        void OnHoldArrowRight(InputAction.CallbackContext context);
        void OnPressNorthButton(InputAction.CallbackContext context);
        void OnPressSouthButton(InputAction.CallbackContext context);
        void OnPressWestButton(InputAction.CallbackContext context);
        void OnPressEastButton(InputAction.CallbackContext context);
        void OnReleaseNorthButton(InputAction.CallbackContext context);
        void OnReleaseSouthButton(InputAction.CallbackContext context);
        void OnReleaseWestButton(InputAction.CallbackContext context);
        void OnReleaseEastButton(InputAction.CallbackContext context);
        void OnHoldNorthButton(InputAction.CallbackContext context);
        void OnHoldSouthButton(InputAction.CallbackContext context);
        void OnHoldWestButton(InputAction.CallbackContext context);
        void OnHoldEastButton(InputAction.CallbackContext context);
        void OnLeftTrigger(InputAction.CallbackContext context);
        void OnLeftShoulder(InputAction.CallbackContext context);
        void OnRightTrigger(InputAction.CallbackContext context);
        void OnRightShoulder(InputAction.CallbackContext context);
    }
}
