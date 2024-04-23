using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int _penSize = 5;
    [SerializeField] private Texture2D _paintTexture;

    private Renderer _renderer;
    private float _tipHeight;

    private RaycastHit _touch;
    private Vector2 _touchPos, _lastTouchPos;
    private Whiteboard _whiteboard;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    void Start()
    {
        _renderer = tip.GetComponent<Renderer>();
        _tipHeight = tip.localScale.y;

        Texture2D texture = Instantiate(_renderer.material.mainTexture) as Texture2D;
        _renderer.material.mainTexture = texture;
    }

    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(tip.position - transform.up * _tipHeight, transform.up, out _touch, _tipHeight * 2))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
                {
                    return;
                }

                if (_touchedLastFrame)
                {
                    // Get the texture's pixels
                    Color[] texturePixels = _paintTexture.GetPixels(_paintTexture.width / 2 - _penSize / 2, _paintTexture.height / 2 - _penSize / 2, _penSize, _penSize);

                    // Set the whiteboard's texture pixels to match
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, texturePixels);

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
}

