using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ruler : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceFactor;
    [SerializeField] private float _positionZRuler;

    private Rigidbody _rigibody;
    private Vector3 _direction;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _rigibody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 pointTouch = new Vector3(touch.position.x, 0, 0);
            pointTouch.z = _mainCamera.nearClipPlane * _distanceFactor;
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(pointTouch);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    transform.position = new Vector3(worldPosition.x, transform.position.y, _positionZRuler);
                    break;
                case TouchPhase.Moved:
                    _direction = new Vector3(worldPosition.x, 0, 0);
                    _rigibody.MovePosition(transform.position + _speed * Time.deltaTime * _direction);
                    break;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, _startPosition.y, _startPosition.z);
        }
    }
}
