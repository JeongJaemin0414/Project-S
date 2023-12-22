using UnityEngine;
using System;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class PlayerInputs : MonoBehaviour
{

	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool isActioning;
	public float number;

	[Header("Movement Settings")]
	public bool analogMovement;


#if !UNITY_IOS || !UNITY_ANDROID
	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;
#endif

	public event Action OnChangeStateInputValue;
	public event Action OnChangeNumberInputValue;

	public void Start()
    {

	}

	public void ClearStateValue()
    {
		move = Vector2.zero;
		look = Vector2.zero;
		jump = false;
		sprint = false;
		isActioning = false;
	}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnNumber(InputValue value)
    {
		NumberInput(value.Get<float>());
    }

	public void OnAction(InputValue value)
    {
		ActionInput(value.isPressed);
	}

#else
// old input sys if we do decide to have it (most likely wont)...
#endif


	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;

		if (!isActioning)
			OnChangeStateInputValue?.Invoke();
	} 

	public void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;

		if (!isActioning)
			OnChangeStateInputValue?.Invoke();
	}

	public void ActionInput(bool newActionState)
	{
		if (!isActioning)
        {
			isActioning = newActionState;
			Debug.Log(isActioning);

			OnChangeStateInputValue?.Invoke();
		}
	}

	public void NumberInput(float newNumber)
	{
		number = newNumber;

		if (!isActioning)
			OnChangeNumberInputValue?.Invoke();
	}



#if !UNITY_IOS || !UNITY_ANDROID

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}

#endif

}