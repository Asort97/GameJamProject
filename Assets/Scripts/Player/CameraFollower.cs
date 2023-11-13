using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; // Цель, за которой следит камера
    public float smoothing = 5f; // Коэффициент сглаживания

    private void Update()
    {
        if (target != null)
        {
            FollowTarget();
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FollowTarget()
    {
        // Новая позиция для камеры, с учетом сглаживания
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
