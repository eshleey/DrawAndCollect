using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameManager GameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bucket Lath"))
        {
            gameObject.SetActive(false);
            GameManager.Continue(transform.position);
        }

        if (collision.gameObject.CompareTag("Edge Bottom"))
        {
            gameObject.SetActive(false);
            GameManager.GameOver();
        }
    }
}
