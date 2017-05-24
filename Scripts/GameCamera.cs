using UnityEngine;

public class GameCamera : MonoBehaviour {

    private Vector3 cameraTarget;
    private Camera cam;
    private GameObject targetObj;
    private Transform target; 

	// Use this for initialization
	void Start () {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (targetObj != null)
        {
            target = targetObj.transform;
            cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 8);
        }
        else
        {
            cam.transform.LookAt(new Vector3(0,0,10));
        }
    }
}
