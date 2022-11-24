using UnityEngine;

public class GameViewCamera : MonoBehaviour
{
    Camera gameView;
    public Vector2 aspectRatio;

    void Start() {
        gameView = GetComponent<Camera>();

        gameView.aspect = aspectRatio.x / aspectRatio.y;
        print(gameView.aspect);
    }
}