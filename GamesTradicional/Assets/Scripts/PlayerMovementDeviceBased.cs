using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovementDeviceBased : MonoBehaviour
{
    public XRNode inputSource;
    public float speed = 1.0f;

    private Vector2 inputAxis;
    private CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);
        Vector3 movement = transform.TransformDirection(direction) * speed * Time.deltaTime;

        character.Move(movement);
    }
}