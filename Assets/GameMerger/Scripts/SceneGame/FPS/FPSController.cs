using UnityEngine;
public class FPSController : MonoBehaviour
{
    private void Awake()
    {
        InvokeRepeating(nameof(UpdateFPS), 0f, 1f);
        Application.targetFrameRate = 90;
    }
    void Update()
    {
        this.UpdateFPS();
    }

    private void UpdateFPS()
    {
        var fps = 1 / Time.deltaTime;
    }
}
