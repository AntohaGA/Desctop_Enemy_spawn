using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _directonToMove;

    public void SpawnEnemy(Enemy enemy)
    {
        enemy.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        enemy.GoToDirection(_directonToMove);
    }
}