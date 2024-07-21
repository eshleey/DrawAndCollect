using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject ThrowBallCentre;
    int ActiveBallIndex;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Balls[ActiveBallIndex].transform.position = ThrowBallCentre.transform.position;
            Balls[ActiveBallIndex].SetActive(true);

            float Angle = Random.Range(70f, 110f);
            Vector3 Pos = Quaternion.AngleAxis(Angle, Vector3.forward) * Vector3.right;

            Balls[ActiveBallIndex].gameObject.GetComponent<Rigidbody2D>().AddForce(Pos * 750);

            if (ActiveBallIndex != Balls.Length - 1)
            {
                ActiveBallIndex++;
            }
            else
            {
                ActiveBallIndex = 0;
            }
        }
    }
}
