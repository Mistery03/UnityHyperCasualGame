using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    public Camera MainCamera;
    Vector2 _screenBounds;
    float _objectWidth, _objectHeight;
    // Start is called before the first frame update
    void Start()
    {
        _screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,MainCamera.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        _objectHeight= transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -_screenBounds.x  + _objectWidth , _screenBounds.x - _objectWidth) ;
        viewPos.y = Mathf.Clamp(viewPos.y, -_screenBounds.y  + _objectHeight, _screenBounds.y - _objectHeight);
      
        transform.position = viewPos;
    }
}
