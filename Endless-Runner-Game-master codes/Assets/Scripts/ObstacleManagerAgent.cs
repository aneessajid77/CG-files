using UnityEngine; // Core Unity functionality
using Unity.MLAgents; // Base Agent functionality
using Unity.MLAgents.Sensors; // Observations
using Unity.MLAgents.Actuators; // Actions and ActionBuffers

public class ObstacleManagerAgent : Agent
{
    public Transform player; // Reference to the player GameObject
    public GameObject[] obstaclePrefabs; // Array for multiple obstacle prefabs
    public float spawnRange = 10f; // Range for spawning obstacles

    public override void CollectObservations(VectorSensor sensor)
    {
        if (player != null)
        {
            sensor.AddObservation(player.position.x); // Player X position
            sensor.AddObservation(player.position.y); // Player Y position
            var rb2D = player.GetComponent<Rigidbody2D>();
            sensor.AddObservation(rb2D != null ? rb2D.velocity.x : 0f); // Player velocity
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float spawnX = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f) * spawnRange;
        float spawnY = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f) * spawnRange;

        // Randomly select one obstacle from the array
        GameObject selectedObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        SpawnObstacle(new Vector2(spawnX, spawnY), selectedObstacle);
    }

    private void SpawnObstacle(Vector2 position, GameObject obstacle)
    {
        if (obstacle != null)
        {
            Instantiate(obstacle, position, Quaternion.identity);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Random.Range(-1f, 1f);
        continuousActions[1] = Random.Range(-1f, 1f);
    }
}
