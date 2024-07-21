using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject ThrowBallCentre;
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject[] BucketPoints;
    int ActiveBallIndex;
    int RandomBucketPointIndex;
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

        Invoke("RevealBucket", 0.5f);
    }

    void RevealBucket()
    {
        RandomBucketPointIndex = Random.Range(0, BucketPoints.Length - 1);
        Bucket.transform.position = BucketPoints[RandomBucketPointIndex].transform.position;
        Bucket.SetActive(true);
    }
}
