using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] private Camera cam;
    [SerializeField] private int size, zoomSize;
    [SerializeField] private float zoomSpeed, moveSpeed;

    private bool isZoomingIn;
    private Vector3 targetPosition;

    void Awake(){
        Instance = this;
        targetPosition = cam.transform.position;
    }
    void Update(){

        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (isZoomingIn && cam.orthographicSize >= zoomSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomSize, zoomSpeed * Time.deltaTime);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, size, zoomSpeed * Time.deltaTime);
        }
    }

    public void SetCameraToMember(BaseMember member){
        isZoomingIn = true;
        targetPosition = new Vector3(member.OccupiedTile.transform.position.x + 45f, member.OccupiedTile.transform.position.y + 60f, -10f);
    }

    public void ResetCamera(){
        isZoomingIn = false;
        targetPosition = new Vector3((GridManager.Instance.width - 1f) * 60, (GridManager.Instance.height - 1f) * 60, -10f);
    }

    
}
