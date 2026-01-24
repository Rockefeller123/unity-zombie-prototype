using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float gravity = -20f;
    public float groundedForce = -2f;
    public float steeringStrength = 0.5f;
    public float damage = 10f;
    public float attackCooldown = 1.2f;
    private float nextAttackTime;
    private AudioSource audioSource;





    private float verticalVelocity;


    private Transform player;
    private CharacterController controller;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        controller = GetComponent<CharacterController>();

        audioSource = GetComponent<AudioSource>();

    }

private void Update()
{
    if (player == null || controller == null)
        return;

    // Horizontal movement toward player
    Vector3 toPlayer = player.position - transform.position;
    toPlayer.y = 0f;

    Vector3 direction = toPlayer.normalized;

    // Simple steering to avoid getting stuck
    Vector3 sideStep = Vector3.Cross(Vector3.up, direction);
    direction += sideStep * steeringStrength;
    direction = direction.normalized;


    // Ground check
    if (controller.isGrounded)
    {
        if (verticalVelocity < 0)
            verticalVelocity = groundedForce;
    }

    // Apply gravity
    verticalVelocity += gravity * Time.deltaTime;

    Vector3 velocity = direction * moveSpeed;
    velocity.y = verticalVelocity;

    controller.Move(velocity * Time.deltaTime);

    // Face player
    transform.LookAt(new Vector3(
        player.position.x,
        transform.position.y,
        player.position.z
    ));
}
private void OnControllerColliderHit(ControllerColliderHit hit)
{
    PlayerHealth playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
    if (playerHealth != null)
    {
    if (Time.time >= nextAttackTime)
{
    nextAttackTime = Time.time + attackCooldown;
    playerHealth.TakeDamage(damage);

    if (audioSource != null)
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

}

    }
}


}


