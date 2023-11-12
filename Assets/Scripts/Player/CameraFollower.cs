using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private Transform Player;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
    }
}
