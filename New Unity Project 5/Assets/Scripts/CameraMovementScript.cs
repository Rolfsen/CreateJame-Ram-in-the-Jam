using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour {
    [Header("Player Scripts")]
    [SerializeField]
    private PlayerControl player1;
    [SerializeField]
    private PlayerControl player2;
    [SerializeField]
    private const float DISTANCE_MARGIN = 2.0f;

    private Vector3 middlePoint;
    private float distanceFromMiddlePoint;
    private float distanceBetweenPlayers;
    private float cameraDistance;
    private float aspectRatio;
    private float fov;
    private float tanFov;

    void Start()
    {
        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * this.GetComponent<Camera>().fieldOfView / 2.0f);
    }

    void Update()
    {

        // Position the camera in the center.
        Vector3 newCameraPos = this.transform.position;
        newCameraPos.x = middlePoint.x;
        this.transform.position = newCameraPos;

        // Find the middle point between players.
        Vector3 vectorBetweenPlayers = player2.transform.position - player1.transform.position;
        middlePoint = player1.transform.position + 0.5f * vectorBetweenPlayers;

        // Calculate the new distance.
        distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
        cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

        // Set camera to new position.
        Vector3 dir = (this.transform.position - middlePoint).normalized;
        this.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
        Vector3 tempVector = this.transform.position;
        tempVector.x = middlePoint.x;
        tempVector.y = middlePoint.y;
        transform.position = tempVector;

        this.GetComponent<Camera>().orthographicSize = cameraDistance + DISTANCE_MARGIN;
    }
}
