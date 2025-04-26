using UnityEngine;

public class EnemyRange : MonoBehaviour
{

    [SerializeField] Enemy enemy;

    void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null) {
            // Debug.Log("Player in range");
            // Move towards player here
            // Debug.Log(player.transform.position);
            
            Vector3 dir = player.transform.position - transform.position;

            if (dir.magnitude > 0.5f) {
                enemy.Move(dir.normalized);
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        enemy.Move(Vector2.zero);
    }
}
