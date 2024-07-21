using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bucket Lath"))
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Edge Bottom"))
        {
            gameObject.SetActive(false);
        }
    }
}
