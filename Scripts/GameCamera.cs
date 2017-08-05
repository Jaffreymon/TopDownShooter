using UnityEngine;

public class GameCamera : MonoBehaviour {

    private Vector3 cameraTarget;
    private Player targetObj;
    private Transform target;
    private float camHeight = 8f;

    // Update is called once per frame
    void Update () {
        if (targetObj != null)
        {
            target = targetObj.transform;
            cameraTarget = new Vector3(target.position.x, target.position.y + camHeight, target.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 8);
        }
    }

    public void setPlayerTarget(Player _target)
    {
        targetObj = _target;
    }
}
