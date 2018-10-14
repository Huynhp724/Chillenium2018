using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour {

    Camera mainCamera;
    public float zoomSpeed;
    public float maxZoom = 6f;
    public float minZoom = 3f;
    public float borderWidth;
    public float borderHeight;
    float camHeight;
    float camWidth;
    public float size;
    Vector2 prevDistance;
    Vector2 currDistance;
    public GameObject[] players;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camHeight = 2f * mainCamera.orthographicSize;
        camWidth = camHeight * mainCamera.aspect;
        prevDistance = maxSizeCalc();
    }
	
	// Update is called once per frame
	void Update () {
        
        size = mainCamera.orthographicSize;
        //Find max bounds x and y of all players
        currDistance = maxSizeCalc();
        mainCamera.gameObject.transform.parent.gameObject.transform.position = 
        new Vector3((players[0].transform.position.x + players[1].transform.position.x)/2, (players[0].transform.position.y + players[1].transform.position.y) / 2, mainCamera.gameObject.transform.parent.gameObject.transform.position.z);
        float prevArea = prevDistance.x * prevDistance.y;
        float currArea = currDistance.x * currDistance.y;
        if(currArea - prevArea > 0)
        {
            mainCamera.orthographicSize += Time.deltaTime * zoomSpeed;
        }
        else if (currArea - prevArea < 0)
        {
            mainCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
        }
        if (mainCamera.orthographicSize < 3) mainCamera.orthographicSize = 3;
        if (mainCamera.orthographicSize > 6) mainCamera.orthographicSize = 6;
        prevDistance = currDistance;
	}

    //returns biggest x distance and y distance between players
    Vector2 maxSizeCalc()
    {
        float x = 0;
        float y = 0;
        x = players[0].transform.position.x - players[1].transform.position.x;
        y = players[0].transform.position.y - players[1].transform.position.y;
        return new Vector2(x, y);
    }
}
