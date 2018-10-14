using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour {

    Camera mainCamera;
    public GameObject[] players;
    public Vector3 offset; //offset of camera
    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 3f;
    public float maxZoom = 6f;
    public float zoomLimit = 20f;
    public float zoomSpeed;
    // Use this for initialization
    void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }


    private void LateUpdate()
    {
        Move();
        Zoom();
    }

    //returns x distance and y distance between players
    Vector2 maxSizeCalc()
    {
        float x = 0;
        float y = 0;
        x = players[0].transform.position.x - players[1].transform.position.x;
        y = players[0].transform.position.y - players[1].transform.position.y;
        return new Vector2(Mathf.Abs(x), Mathf.Abs(y));
    }

    void Move()
    {
        //Gets midpoint between camera and centers it there
        Vector3 newPos =
        new Vector3((players[0].transform.position.x + players[1].transform.position.x) / 2, (players[0].transform.position.y + players[1].transform.position.y) / 2,
        mainCamera.gameObject.transform.parent.gameObject.transform.position.z);

        mainCamera.gameObject.transform.parent.gameObject.transform.position = Vector3.SmoothDamp(mainCamera.gameObject.transform.parent.gameObject.transform.position,
            newPos + offset, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, maxSizeCalc().x/ zoomLimit);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newZoom, Time.deltaTime * zoomSpeed);
    }
}
