using UnityEngine;

namespace Dev.Evan.Scripts
{
    public class CamMouvement : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
    
        float _xRotation;
        float _yRotation;
    
        public Transform orientation;
 
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
 
        private void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
            _xRotation -= mouseY;
            _yRotation += mouseX;
        
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
 
        }
    }
}
