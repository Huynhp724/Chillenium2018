using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZooming : MonoBehaviour {

    Camera mainCamera;
    public float zoomSpeed;
    public float maxZoom;
    public float minZoom;
    public GameObject[] players;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        //Find max bounds x and y of all players
        Vector2 max2D = maxSizeCalc();
        mainCamera.gameObject.transform.parent.gameObject.transform.position = new Vector3(max2D.x, max2D.y, 0);
        mainCamera.orthographicSize = 4;
	}

    //returns biggest x distance and y distance between players
    Vector2 maxSizeCalc()
    {
        float x = 0;
        float y = 0;
        x = players[0].transform.position.x + players[1].transform.position.x;
        y = players[0].transform.position.y + players[1].transform.position.y;
        return new Vector2(x/2, y/2);
    }
}
