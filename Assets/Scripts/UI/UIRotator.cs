using UnityEngine;

public class UIRotator : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private RectTransform rectTransform = null;

    void Update()
    {
        if (rectTransform == null)
            return;

        rectTransform.Rotate(Vector3.back, speed * Time.deltaTime);
    }
}
