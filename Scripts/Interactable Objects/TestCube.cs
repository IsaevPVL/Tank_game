using UnityEngine;

public sealed class TestCube : MonoBehaviour, IPlayerInteractable
{
    public void OnPlayerInteraction()
    {
        print("Interacted with " + this.name);
    }
}