using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int health;

    public void DecreaseHealth(int amt) {
        if(health <= amt) {
            health = 0;
            Debug.Log("Destroyed");
            Destroy(gameObject);
            return;
        }
        health -= amt;
        Debug.Log("Health: " + health);
        return;
    }
}
