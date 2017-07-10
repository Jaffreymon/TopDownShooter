using UnityEngine;

public class GameCamera : MonoBehaviour {

    private Vector3 cameraTarget;
    private Camera cam;
    private GameObject targetObj;
    private Transform target;
    private float camHeight = 8f;

	// Use this for initialization
	void OnEnable() {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponent<Camera>();
	}

    // Update is called once per frame
    void Update () {
        Debug.Log(Time.timeScale);
        if (targetObj != null)
        {
            target = targetObj.transform;
            cameraTarget = new Vector3(target.position.x, target.position.y + camHeight, target.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 8);
        }
        else
        {
//            transform.LookAt(new Vector3(0,0,10));
        }
    }
}
