using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerInput : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float moveSpd; //Def = 10
    //[SerializeField] private float turnSpd; //Def = 150

    //[Header("LayerMasks")]
    //[SerializeField] private LayerMask groundLayer;

    [Header("Collider")]
    [SerializeField] private BoxCollider grndCheck;

    private Vector3 moveHorizontal;
    private Vector3 moveVertical;
    private float horizontalIp;
    private float verticalIp;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string GROUND = "Ground";

    void Update()
    {
        MovePlayer();
        if(Input.GetKeyDown(KeyCode.E)) {
            Jump();
        }
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
    }

    private void Jump() {
        IsGrounded();
    }

    private bool IsGrounded() {
        Collider[] colliderArray = Physics.OverlapBox(grndCheck.center, grndCheck.size);
        foreach(Collider collider in colliderArray) {
            if(collider.tag == GROUND) {
                Debug.Log("Grounded");
                return true;
            }
        }
        return false;
    }
}
