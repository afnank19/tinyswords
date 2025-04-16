using UnityEngine;

public class FollowY : MonoBehaviour
{

    public Transform player;
    readonly float fixedX = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new(fixedX, playerPos.y, playerPos.z);
    }
}
