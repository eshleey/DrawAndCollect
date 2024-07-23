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

    public static int BallsThrownCount;
    public static int ThrowBallCount;

    private void Start()
    {
        BallsThrownCount = 0;
        ThrowBallCount = 0;
    }

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

                if (ThrowBallCount != 0 && ThrowBallCount % 5 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ThrowBallAndAdjustment();
                    }
                    BallsThrownCount = 2;
                    ThrowBallCount++;
                }
                else
                {
                    ThrowBallAndAdjustment();
                    BallsThrownCount = 1;
                    ThrowBallCount++;
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
        if (BallsThrownCount == 1)
        {
            Lock = false;
            Bucket.SetActive(false);
            CancelInvoke();
            BallsThrownCount--;
        }
        else
        {
            BallsThrownCount--;
        }
    }

    float GiveAngle(float value1, float value2)
    {
        return Random.Range(value1, value2);
    }

    Vector3 GivePosition(float IncomingAngle)
    {
        return Quaternion.AngleAxis(IncomingAngle, Vector3.forward) * Vector3.right;
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

    void ThrowBallAndAdjustment()
    {
        Balls[ActiveBallIndex].transform.position = ThrowBallCentre.transform.position;
        Balls[ActiveBallIndex].SetActive(true);
        Balls[ActiveBallIndex].GetComponent<Rigidbody2D>().AddForce(GivePosition(GiveAngle(70f, 110f)) * 750);
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
