using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private GameObject ThrowBallCentre;
    [SerializeField] private GameObject Bucket;
    [SerializeField] private GameObject[] BucketPoints;
    int ActiveBallIndex;
    int RandomBucketPointIndex;
    bool Lock;

    public void StartGame()
    {
        StartCoroutine(ThrowBallSystem());
    }

    IEnumerator ThrowBallSystem()
    {
        while (true)
        {
            if (!Lock)
            {
                yield return new WaitForSeconds(0.5f);
                Balls[ActiveBallIndex].transform.position = ThrowBallCentre.transform.position;
                Balls[ActiveBallIndex].SetActive(true);

                float Angle = Random.Range(70f, 110f);
                Vector3 Pos = Quaternion.AngleAxis(Angle, Vector3.forward) * Vector3.right;

                Balls[ActiveBallIndex].GetComponent<Rigidbody2D>().AddForce(Pos * 750);

                if (ActiveBallIndex != Balls.Length - 1)
                {
                    ActiveBallIndex++;
                }
                else
                {
                    ActiveBallIndex = 0;
                }

                yield return new WaitForSeconds(0.7f);
                RandomBucketPointIndex = Random.Range(0, BucketPoints.Length - 1);
                Bucket.transform.position = BucketPoints[RandomBucketPointIndex].transform.position;
                Bucket.SetActive(true);
                Lock = true;
                Invoke("CheckBall", 6f);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void Continue()
    {
        Lock = false;
        Bucket.SetActive(false);
        CancelInvoke();
    }

    public void StopThrowBall()
    {
        StopAllCoroutines();
    }

    void CheckBall()
    {
        if (Lock)
        {
            GetComponent<GameManager>().GameOver();
        }
    }
}
