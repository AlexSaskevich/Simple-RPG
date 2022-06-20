using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 2.0f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 4.0f, 0);
    [SerializeField] private Vector3 _randomizeIntensity = new Vector3(0.5f, 0, 0);

    private void Start()
    {
        Destroy(gameObject, _destroyTime);

        transform.localPosition += _offset;

        transform.localPosition += new Vector3(Random.Range(-_randomizeIntensity.x, _randomizeIntensity.x), 0, 0);
    }
}