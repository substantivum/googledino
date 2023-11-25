using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class Player : Agent
{
    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    [SerializeField]
    PlayerController pc;

    private bool wasGroundedLastFrame = true;

    public override void Initialize()
    {
        pc = GetComponent<PlayerController>();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (Mathf.FloorToInt(actions.DiscreteActions[0]) == 1)
        {
            pc.Jump();
            Debug.Log("Agent is jumping");
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0;

        if (Input.GetButton("Jump"))
        {
            discreteActions[0] = 1;
        }
    }

    public override void OnEpisodeBegin()
    {
        GameManager.Instance.Start();
        pc.readyJump = false;
        wasGroundedLastFrame = true; // Initialize to true when the episode begins
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && GameManager.Instance.isRunning)
        {
            SetReward(-10.0f);
            GameManager.Instance.GameOver();
            EndEpisode();
        }
    }

    public void GiveReward(float r)
    {
        AddReward(r);
    }

    private void FixedUpdate()
    {
        bool isGrounded = pc.IsGrounded();

        // If the agent was grounded last frame but is not grounded this frame, request a decision
        if (wasGroundedLastFrame && !isGrounded)
        {
            RequestDecision();
        }

        wasGroundedLastFrame = isGrounded;
    }
}
