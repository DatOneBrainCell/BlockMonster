using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float movSpd;
    [SerializeField] private float jumpForce;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider sc;

    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) {
            Attack();
            Jump();
        }
    }

    private void MovePlayer() {
        transform.Translate(new Vector3(horizontalInput, 0f, verticalInput).normalized * movSpd * Time.deltaTime);
    }

    private void Jump() {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Attack() {
        Collider[] colliderArray = Physics.OverlapSphere(sc.center, sc.radius);

        foreach(Collider collider in colliderArray) {
            Debug.Log(collider.name);
        }
    }
}
