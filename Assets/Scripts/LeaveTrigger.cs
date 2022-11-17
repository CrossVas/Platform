using UnityEngine;

public class LeaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        LevelGenerator.instance.AddChunk();
        LevelGenerator.instance.removeOldChunk();
    }
}
