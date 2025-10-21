using UnityEditor.TextCore.Text;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [Header ("Collider")]
    [SerializeField] private Transform atkArea;
    [Header ("Numbers")]
    [SerializeField] private int dmgAmt;
    [SerializeField] private float atkDist;
    [SerializeField] private float sphereRad;
    [Header ("LayerMask")]
    [SerializeField] LayerMask creatureLayer;

    private RaycastHit centrePt; //First hit

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift)) {
            SpecialAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Space)) {
            BasicAttack();
        }
    }

    void BasicAttack() {
        AreaDamage(atkArea.position, 1.5f, creatureLayer);
    }

    private void SpecialAttack() {
        Debug.Log("Special");
    }

    private void AreaDamage(Vector3 origin, float radius, int layerMask) {

        Collider[] colliderArray = Physics.OverlapSphere(origin, radius, layerMask);
        foreach(Collider collider in colliderArray) {

            if (collider.TryGetComponent<Stats>(out Stats stats)) {

                Debug.Log(collider.transform.name);

                stats.DecreaseHealth(
                    Random.Range(dmgAmt - 5, dmgAmt + 7)
                );
            }
        }
    }
    private void OldAtk() {

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f,
            -transform.up,
            out centrePt,
            atkDist)) {

            Debug.DrawRay(transform.position, -transform.up * centrePt.distance, Color.red);
            Debug.Log("Hit");
            atkArea.position = centrePt.point;

            AreaDamage(centrePt.point, sphereRad, 7);
        } //3 is the ground layer

        Debug.Log("trans Pos: " + transform.position + "-atk: " + -transform.up);

        //sphereHit.transform.gameObject.TryGetComponent<Stats>(out Stats stats);
        //stats.DecreaseHealth(Random.Range(dmgAmt - 5, dmgAmt + 7));
    }
}
