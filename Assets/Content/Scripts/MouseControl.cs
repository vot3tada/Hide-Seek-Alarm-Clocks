using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Transform player;
    private float sensetive;
    private float rotation = 0f;

    private float mouseX;
    private float mouseY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sensetive = PlayerPrefs.GetFloat("Sensetivity");
    }
    void Update() => MouseMove();

    private void MouseMove()
    {
        mouseX = Input.GetAxis("Mouse X") * sensetive * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensetive * Time.deltaTime;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
