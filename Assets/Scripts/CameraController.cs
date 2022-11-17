using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}
