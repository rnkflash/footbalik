using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.player
{
    interface IPlayerControl
    {
        Vector2 GetMoveDirection();
        Vector3 GetMousePosition();
        bool IsRunning();
        bool IsKick();
    }
    public class ManualControl: MonoBehaviour, IPlayerControl
    {
        [SerializeField] InputActionReference inputMove;
        [SerializeField] InputActionReference inputRunButton;
        [SerializeField] InputActionReference inputKickButton;
        [SerializeField] LayerMask groundLayer;
        
        public Vector2 GetMoveDirection()
        {
            return inputMove.action.ReadValue<Vector2>();
        }
        public bool IsRunning()
        {
            return inputRunButton.action.IsPressed();
        }
        public bool IsKick()
        {
            return inputKickButton.action.WasPressedThisFrame();
        }
        
        public Vector3 GetMousePosition()
        {
            var mousePosition = Vector3.zero;
            if (Camera.main == null)
                return mousePosition;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
            {
                mousePosition = hit.point;
            }
            return mousePosition;
        }
    }
}