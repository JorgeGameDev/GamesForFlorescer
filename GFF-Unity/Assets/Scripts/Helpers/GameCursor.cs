using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used for displaying the Cursor in Game.
public class GameCursor : MonoBehaviour {

    [Header("Changing Cursor")]
    public Sprite pickingCursor;
    public Sprite normalCursor;

    // Internal.
    private Camera _camera;
    private Vector2 _mousePixel;
    private Vector2 _mouseWorld;
    private Vector2 _mouseRectTransform;
    private RectTransform _cursorRectTransform;
    private Image _imageCursor;

    // Use this for early initialization
    void Awake()
    {
        _cursorRectTransform = GetComponent<RectTransform>();
        _imageCursor = GetComponent<Image>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the mouse not visible.
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }

        // Checks if the camera is null or not.
        if (_camera != null)
        {
            _camera = Camera.main;
        }

        CalculateMousePositions();
    }

    // Calculates the Mouse Positions.
    void CalculateMousePositions()
    {
        // Gets the mouse in pixel size.
        _mousePixel = Input.mousePosition;

        // Convert it to world size.
        _mouseWorld = _camera.ScreenToWorldPoint(_mousePixel);

        // Converts it in Rect Transform size.
        //_mouseRectTransform = RectTransformUtility.WorldToScreenPoint(_camera, _mouseWorld);

        // Updates the UI to reflect the new Mouse Rect.
        _cursorRectTransform.position = _mouseWorld;
    }

    /// <summary>
    /// Changes the cursor to a specific value,.
    /// </summary>
    public void ChangeCursor(int cursorValue)
    {
        if(cursorValue == 0)
        {
            _imageCursor.sprite = normalCursor;
        }
        else if(cursorValue == 1)
        {
            _imageCursor.sprite = pickingCursor;
        }
    }
}
