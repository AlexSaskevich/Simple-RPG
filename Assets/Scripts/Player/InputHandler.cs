using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public Vector2 InputVector { get; private set; }

    private void Update()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        InputVector = new Vector2(horizontal, vertical);
    }
}