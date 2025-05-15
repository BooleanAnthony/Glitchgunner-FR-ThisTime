using System;
using TMPro;
using UnityEngine;

public enum SwipeDirection { None, Up, Down, Left, Right }

public class TouchInputManager : MonoBehaviour
{
    public static SwipeDirection LastSwipe { get; private set; } = SwipeDirection.None;
    public static bool TapDetected { get; private set; } = false;
    public static bool TapHeld { get; private set; } = false;
    public static bool TapStarted { get; private set; } = false;

    [SerializeField] private float swipeThreshold = 50f;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private bool _isTouchingRight = false;

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
            if (_startTouchPosition.x >= screenMiddle)
            {
                TapStarted = true;
                TapHeld = true;
                Debug.Log("[Touch] Tap START detected on RIGHT side.");
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;
            if (currentPos.x >= screenMiddle)
                TapHeld = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosition = Input.mousePosition;

            // Check if started on right side for tap detection
            if (_startTouchPosition.x >= screenMiddle)
            {
                Vector2 delta = _endTouchPosition - _startTouchPosition;
                if (delta.magnitude < swipeThreshold)
                {
                    TapDetected = true;
                    Debug.Log("[Touch] Tap DETECTED on RIGHT side.");
                }
            }

            // Check if started on left side for swipe detection
            if (_startTouchPosition.x < screenMiddle)
            {
                DetectTouch();
            }

            TapHeld = false;
        }
    #else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouchPosition = touch.position;
                if (_startTouchPosition.x >= screenMiddle)
                {
                    TapStarted = true;
                    TapHeld = true;
                    Debug.Log("[Touch] Tap START detected on RIGHT side.");
                }
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touch.position.x >= screenMiddle)
                    TapHeld = true;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _endTouchPosition = touch.position;

                if (_startTouchPosition.x >= screenMiddle)
                {
                    Vector2 delta = _endTouchPosition - _startTouchPosition;
                    if (delta.magnitude < swipeThreshold)
                    {
                        TapDetected = true;
                        Debug.Log("[Touch] Tap DETECTED on RIGHT side.");
                    }
                }

                if (_startTouchPosition.x < screenMiddle)
                {
                    DetectTouch();
                }

                TapHeld = false;
            }
        }
    #endif
    }


    private void DetectTouch()
    {
        Vector2 delta = _endTouchPosition - _startTouchPosition;
        float screenMiddle = Screen.width / 2f;
        bool isLeftSide = _startTouchPosition.x < screenMiddle;

        Debug.Log($"[Touch] Delta: {delta.magnitude}, Is Left Side: {isLeftSide}");

        if (delta.magnitude < swipeThreshold)
        {
            if (!isLeftSide)
            {
                TapDetected = true;
                Debug.Log("[Touch] TAP detected on RIGHT side.");
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
            Debug.Log($"[Touch] SWIPE detected on LEFT side: {LastSwipe}");
        }
    }
}
