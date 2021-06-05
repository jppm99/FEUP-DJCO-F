using UnityEngine;

public class DisableCanvasOnPaused : MonoBehaviour
{
    private Canvas canvas;
    private void Awake() {
        canvas = GetComponent<Canvas>();
    }

    private void Update() {
        canvas.enabled = Time.timeScale > 0;
    }
}
