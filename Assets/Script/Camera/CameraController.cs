using UnityEngine;


public enum TypeCam
{
    TopDownPlayer,
    TopDownClassic
}

public class CameraController : MonoBehaviour {

    public TypeCam typeCam = TypeCam.TopDownClassic;

    public CameraControllerClassic cameraControllerClassic;
    public CameraFollowPlayer cameraFollowPlayer;

    void Update () {

        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (typeCam == TypeCam.TopDownClassic)
        {

            //Déplacement vert l'avant
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - cameraControllerClassic.panBroder)
            {
                transform.Translate(Vector3.forward * cameraControllerClassic.panSpeed * Time.deltaTime, Space.World);
            }
            //Déplacement vert l'arrière
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.mousePosition.y <= cameraControllerClassic.panBroder)
            {
                transform.Translate(Vector3.back * cameraControllerClassic.panSpeed * Time.deltaTime, Space.World);
            }
            //Déplacement vert la gauche
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= cameraControllerClassic.panBroder)
            {
                transform.Translate(Vector3.left * cameraControllerClassic.panSpeed * Time.deltaTime, Space.World);
            }
            //Déplacement vert la droit
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - cameraControllerClassic.panBroder)
            {
                transform.Translate(Vector3.right * cameraControllerClassic.panSpeed * Time.deltaTime, Space.World);
            }

            //Zoome de la caméra
            ZoomCam();
        }

        else if (typeCam == TypeCam.TopDownPlayer)
        {
            MoveCamera();
        }

    }
    void ZoomCam()
    {
        float scoll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scoll * 1000 * cameraControllerClassic.scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, cameraControllerClassic.minY, cameraControllerClassic.maxY);
        transform.position = pos;

    }

    void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, 
                                          cameraFollowPlayer.target.position + cameraFollowPlayer.targetOffset, 
                                          cameraFollowPlayer.movementSpeed * Time.deltaTime);
    }
}

[System.Serializable]
public class CameraControllerClassic
{
    public float panSpeed = 30f;
    public float panBroder = 10f;

    public float scrollSpeed = 5f;

    public float minY = 10;
    public float maxY = 80;
}

[System.Serializable]
public class CameraFollowPlayer
{
     public Transform target;
     public Vector3 targetOffset;
     public float movementSpeed;
}