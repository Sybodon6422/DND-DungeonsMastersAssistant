using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private DefaultControls controls;
    [SerializeField] private Transform debugSphere;
    private Vector2 mPos;

    public float camspeed;

    void Awake()
    {
    controls = new DefaultControls();
    }
    Vector2 moveDir;
    float scroll;
    void Start()
    {
        controls.BaseMap.mousePos.performed += ctx => mPos = ctx.ReadValue<Vector2>();
        controls.BaseMap.mousePos.canceled += ctx => mPos = Vector2.zero;

        controls.BaseMap.LeftClick.performed += ctx => MouseClicked();
        controls.BaseMap.RightClick.performed += ctx => RightClick();

        controls.BaseMap.wasd.performed += ctx => moveDir = ctx.ReadValue<Vector2>();
        controls.BaseMap.wasd.canceled += ctx => moveDir = Vector2.zero;

        controls.BaseMap.scroll.performed += ctx => Zoom(ctx.ReadValue<float>());
        //controls.BaseMap.scroll.canceled += ctx => Zoom(ctx.ReadValue<float>());
    }

    void MouseClicked()
    {
        Vector3 fixedMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, 1f));
        debugSphere.position = fixedMousePos;

        if (Physics.Raycast(fixedMousePos, debugSphere.forward, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3Int tilePos = GameBoard.Instance.GetCellPositionFromWorld(hit.point);
            PlayerManager.Instance.LeftClick(tilePos);
        }
    }

    void Zoom(float ammount)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + ammount*Time.deltaTime,1,25);
    }

    private void MoveCamera(Vector2 moveDir)
    {
        var camDirection = new Vector3(moveDir.y,0,moveDir.x);
        transform.position += camDirection * Time.deltaTime * camspeed;
    }

    void RightClick()
    {
        PlayerManager.Instance.RightClick(mPos);
    }

    void LateUpdate()
    {
        var camDirection = new Vector3(moveDir.x,0,moveDir.y);
        transform.position += camDirection * Time.deltaTime * camspeed;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}
