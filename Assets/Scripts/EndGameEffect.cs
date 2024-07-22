using UnityEngine;

public class EndGameEffect : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Time.timeScale = 0;
    }
}
