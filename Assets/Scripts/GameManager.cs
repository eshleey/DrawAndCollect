using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ThrowBall ThrowBall;
    [SerializeField] DrawLine DrawLine;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Continue()
    {
        ThrowBall.Continue();
        DrawLine.Continue();
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
    }
}
