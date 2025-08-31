using UnityEngine;

public class ObjectRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;  // Arrastra aqu� el GameObject vac�o que marca la posici�n de respawn
    public float respawnDelay = 0.5f; // Peque�o delay opcional para evitar problemas

    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Si no se asign� un respawnPoint, usar la posici�n inicial
        if (respawnPoint == null)
        {
            GameObject tempPoint = new GameObject("TempRespawnPoint");
            tempPoint.transform.position = originalPosition;
            tempPoint.transform.rotation = originalRotation;
            respawnPoint = tempPoint.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnObject"))
        {
            Invoke("RespawnObject", respawnDelay);
        }
    }

    private void RespawnObject()
    {
        // Resetear velocidad si tiene Rigidbody
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teletransportar al punto de respawn
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
    }
}
