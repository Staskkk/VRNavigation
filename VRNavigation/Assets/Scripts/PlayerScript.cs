using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float mouseSensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        float translationZ = Input.GetAxis("Vertical");
        float translationX = Input.GetAxis("Horizontal");
        var direction = transform.TransformDirection(new Vector3(translationX, 0, translationZ));
        direction = new Vector3(direction.x, 0, direction.z).normalized * speed * Time.deltaTime;
        transform.position += direction;

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.visible = false;
            float h = mouseSensitivity * Input.GetAxis("Mouse X");
            float v = mouseSensitivity * Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - v, transform.rotation.eulerAngles.y + h, 0);
        }
        else
        {
            Cursor.visible = true;
        }
    }


}
