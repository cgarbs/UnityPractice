using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Transform relates to positioning
    public Transform target;
    public Vector3 offset;
    
    [Range(1,10)]
    public float smoothFactor;

    // Smoother and less buggy than Update
    private void FixedUpdate()
    {
        // transform.position = target.position + offset;
        Follow();
    }

    // Custom method
    void Follow()
    {
        // smoothPosition allows slight, smooth delay on following character (Lerp)
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}