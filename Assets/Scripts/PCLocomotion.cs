using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCLocomotion : MonoBehaviour
{

    /// <summary>
    /// True: use the mouse to move the camera on the X axis
    /// False: use the Q and E keys to rotate the camera on the X axis
    /// </summary>
    [SerializeField]
    private bool useMouseInput  = true;
    
    /// <summary>
    /// Movement speed of the player
    /// </summary>
    [SerializeField]
    private float moveSpeed     = 10;       
    /// <summary>
    /// Rotation speed of the player
    /// </summary>
    [SerializeField]
    private float rotationSpeed = 180;

    public static Vector3 PlayerPosition;
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float z = Input.GetAxis("Vertical")   * Time.deltaTime * moveSpeed;
        float y;

        if(useMouseInput)
        {
            y = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed * 0.5f;
        }
        else
        {
            y = Input.GetAxis("Rotate") * Time.deltaTime * rotationSpeed;
        }

        transform.Rotate(0, y, 0);
        transform.Translate(x, 0, z);

        PlayerPosition = transform.position;
    }
}
