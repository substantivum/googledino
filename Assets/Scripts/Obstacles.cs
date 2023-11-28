using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public GameManager gameManager = null;
    public Spawner spawner = null;

    private float leftEdge;
    private void Start() {
        leftEdge = gameManager.GetPlayerXPos() - 3;
    }
    private void Update() {
        if (spawner == null || gameManager == null)
            return;

        transform.position += Vector3.left * gameManager.gameSpeed * Time.deltaTime;

        if (transform.position.x < leftEdge) {
            spawner.RemoveObs(gameObject);
            Destroy(gameObject);
        }
    }

}
