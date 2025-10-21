using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerInput : MonoBehaviour
{
    [Header("Multipliers")]
    [SerializeField] private float moveSpd; //Def = 7
    [SerializeField] private float rotateSpd; //Def = 10
    [SerializeField] private float jumpHeight; //Def = 5
    [SerializeField] private float gravityMultiplier; //Def = 5

    [SerializeField] private float grndHeight;

    [Header("Colliders")]
    [SerializeField] private BoxCollider grndCheck;

    private float velocity;
    private Collider currentCollider;

    private const string GROUND = "Ground";

    void Update()
    {
        MovePlayer();
        Jump();
    }

    private void MovePlayer() {
        Vector2 inputVector = new Vector2(0f, 0f);

        if(Input.GetKey(KeyCode.W)) {
            inputVector.y = +1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpd * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpd);
    }

    private bool IsGrounded() {
        //Physics.Raycast(grndCheck.size + transform.position, Vector3.down, grndCheck.size.y / 2);

        Collider[] colliderArray = Physics.OverlapBox(grndCheck.center + transform.position, grndCheck.size);

        foreach (Collider collider in colliderArray) {
            if (collider.CompareTag(GROUND)) {
                currentCollider = collider;
                return true;
            }
        }
        return false;
    }

    private void Jump() {
        Debug.Log(transform.position);
        velocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.E)) {

            velocity = Mathf.Sqrt(-2 * jumpHeight * (Physics.gravity.y * gravityMultiplier));

            Vector3 surface = Physics.ClosestPoint(
                transform.position,
                currentCollider,
                currentCollider.transform.position,
                currentCollider.transform.rotation) + Vector3.up * grndHeight;
            transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
        }

        if(velocity < 0f && IsGrounded()) {
            velocity = 0;
        }

        transform.Translate(new Vector3(0f, velocity, 0f) * Time.deltaTime);
    }
}
