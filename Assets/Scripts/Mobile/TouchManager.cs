using UnityEngine;

public enum SwipeDirection { None, Up, Down, Left, Right }

public class TouchInputManager : MonoBehaviour
{
    public static SwipeDirection LastSwipe { get; private set; } = SwipeDirection.None;
    public static bool TapDetected { get; private set; } = false;
    public static bool TapHeld { get; private set; } = false;
    public static bool TapStarted { get; private set; } = false;

    public static Vector2 SwipeStart { get; private set; }
    public static Vector2 SwipeEnd { get; private set; }
    public static Vector2 SwipeDelta => SwipeEnd - SwipeStart;
    public static Vector2 LastSwipeDelta { get; private set; } = Vector2.zero;
    public static bool SwipeHeld { get; private set; } = false;

    [SerializeField] private float swipeThreshold = 50f;
    [SerializeField] private float swipeResetTime = 1.5f;  // time in seconds to reset swipe delta
    private float _swipeDeltaTimer = 0f;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    void Update()
    {
        LastSwipe = SwipeDirection.None;
        TapDetected = false;
        TapStarted = false;
        TapHeld = false;

        float screenMiddle = Screen.width / 2f;

    #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;

            if (_startTouchPosition.x < screenMiddle)
                SwipeStart = _endTouchPosition;
            if (_startTouchPosition.x >= screenMiddle)
            {
                TapStarted = true;
                TapHeld = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;
            if (currentPos.x >= screenMiddle)
            {
                TapHeld = true;
            }
            else
            {
                SwipeHeld = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosition = Input.mousePosition;

            if (_startTouchPosition.x < screenMiddle)
                SwipeEnd = _endTouchPosition;

            // Check if started on right side for tap detection
            if (_startTouchPosition.x >= screenMiddle)
            {
                Vector2 delta = _endTouchPosition - _startTouchPosition;
                if (delta.magnitude < swipeThreshold)
                {
                    TapDetected = true;
                }
            }

            // Check if started on left side for swipe detection
            if (_startTouchPosition.x < screenMiddle)
            {
                DetectTouch();
            }

            TapHeld = SwipeHeld = false;
        }
    #else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouchPosition = touch.position;

                if (_startTouchPosition.x < screenMiddle)
                    SwipeStart = _startTouchPosition;

                if (_startTouchPosition.x >= screenMiddle)
                {
                    TapStarted = true;
                    TapHeld = true;
                }
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x >= screenMiddle)
                {
                    TapHeld = true;
                }
                else
                {
                    SwipeHeld = true;
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _endTouchPosition = touch.position;

                if (_startTouchPosition.x < screenMiddle)
                    SwipeEnd = _startTouchPosition;

                if (_startTouchPosition.x >= screenMiddle)
                {
                    Vector2 delta = _endTouchPosition - _startTouchPosition;
                    if (delta.magnitude < swipeThreshold)
                    {
                        TapDetected = true;
                    }
                }

                if (_startTouchPosition.x < screenMiddle)
                {
                    DetectTouch();
                }

                TapHeld = SwipeHeld = false;
            }
        }
    #endif
    }

    private void FixedUpdate()
    {
        if (_swipeDeltaTimer > 0f)
        {
            _swipeDeltaTimer -= Time.fixedDeltaTime;
            if (_swipeDeltaTimer <= 0f)
            {
                LastSwipeDelta = Vector2.zero;
                LastSwipe = SwipeDirection.None;
            }
        }
    }


    private void DetectTouch()
    {
        Vector2 delta = _endTouchPosition - _startTouchPosition;
        float screenMiddle = Screen.width / 2f;
        bool isLeftSide = _startTouchPosition.x < screenMiddle;

        if (delta.magnitude < swipeThreshold)
        {
            if (!isLeftSide)
            {
                TapDetected = true;
            }
            return;
        }

        if (isLeftSide)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                LastSwipe = delta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
            }
            else
            {
                LastSwipe = delta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
            }

            LastSwipeDelta = delta;
        }
    }
}
