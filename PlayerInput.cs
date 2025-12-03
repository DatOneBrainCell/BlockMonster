using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float movSpd;
    [SerializeField] private float rotateSpd;
    [SerializeField] private float jumpForce;
    [SerializeField] private float cooldownTime;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider sc;

    int counter = 0;

    private bool canAtk;
    private float atkTimer = 0;

    private const string CREATURES = "Creatures";

    void Update()
    {
        MovePlayer();
        canAtk = Cooldown();

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAtk) {
            Attack();
            atkTimer = 0;
            counter++;
            Debug.Log(counter);
        }
    }

    private void MovePlayer() {

        Vector2 inputVector = Vector2.zero;

        if(Input.GetKey(KeyCode.W)) {
            inputVector.y = +1;
        }
        if(Input.GetKey(KeyCode.A)) {
            inputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.S)) {
            inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.D)) {
            inputVector.x = +1;
        }

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        transform.position += moveDir * Time.deltaTime * movSpd;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpd * Time.deltaTime);
    }

    private void Attack() {
        Collider[] colliderArray = Physics.OverlapSphere(sc.center, sc.radius);

        foreach(Collider collider in colliderArray) {
            if(collider.tag == CREATURES)
            Debug.Log(collider.name);
        }
    }

    private bool Cooldown() {

        atkTimer += Time.deltaTime;

        if (atkTimer >= cooldownTime) {
            return true;
        }
        return false;
    }
}
