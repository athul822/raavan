using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;
		public bool isSprintToggled = false;
		public bool isCrouching = false;
		public bool isInCover = false;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;


#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnSprintToggle(InputValue value)
		{
			if (value.isPressed)
			{
				isSprintToggled = !isSprintToggled;
			}
		}
		
		public void OnCrouchToggle(InputValue value)
		{
			if (value.isPressed)
			{
				isCrouching = !isCrouching;
				Debug.Log($"[Input] Crouch toggled: {isCrouching}");
			}
		}

		public void OnCoverToggle(InputValue value)
		{
			if (value.isPressed)
			{
				var controller = GetComponent<ThirdPersonController>();
				if (controller != null)
				{
					Debug.Log("[Input] Space pressed for cover toggle.");
					controller.OnCoverToggleInput();
				}
				else
				{
					Debug.LogWarning("[Input] ThirdPersonController not found on player.");
				}
			}
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}