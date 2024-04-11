using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private PlayerInputs _plrInput;
    private bool isWalk;

    private KitchenObject kitchenObject;
    [SerializeField] Transform kitchenObjectHoldPoint;

    private BaseCounter selectedCounter;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one player!");
        }
        Instance = this;
    }

    private void Start()
    {
        _plrInput.OnInteractAction += _plrInput_OnInteractAction;
    }

    private void _plrInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interaction(this);
        }

    }


    private void Update()
    {
        PlayerMovement();
        Selecting();
    }

    private void PlayerMovement()
    {
        //Get player inputs from PlayerInputs.cs
        Vector2 inputVector = _plrInput.PlrInputVectorNormalized();

       
        Vector3 moveDir = new Vector3(inputVector.x, transform.position.y, inputVector.y);
        
        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
       
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, _moveSpeed * Time.deltaTime);

        if (canMove)
        {
            transform.position += moveDir * _moveSpeed * Time.deltaTime;
        }
        
        else if (!canMove)
        {
            //Check if player can move in X axis.
            Vector3 moveDirX = new Vector3 (moveDir.x,0,0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, _moveSpeed * Time.deltaTime);

            if (canMove)
            {
                transform.position += moveDirX * _moveSpeed * Time.deltaTime;

            }

            //Check if player can move in Z axis.
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, _moveSpeed * Time.deltaTime);

                if (canMove)
                {
                    transform.position += moveDirZ * _moveSpeed * Time.deltaTime;
                    
                }
                else
                {
                    //Cannot Move
                }
            }
        }
        

        isWalk = moveDir != Vector3.zero;
        float rotateSpeed = 7.5f;
        transform.forward = Vector3.Slerp (transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    //Function that is responsible for player counter selecting
    private void Selecting()
    {

        float interactDistance = 1f;


        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance))
        {
            //Debug.Log("I hit " + hit.transform.name);
            if (hit.transform.TryGetComponent(out BaseCounter slct))
            {
                if (slct != selectedCounter)
                {
                    SelectedCounterFunc(slct);
                }
            }
            else
            {
                SelectedCounterFunc(null);
            }
        }
        else
        {
            SelectedCounterFunc(null);
        }

    }

    public bool IsWalk()
    {
        return isWalk;
    }

    private void SelectedCounterFunc(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
        Debug.Log(selectedCounter);
    }

    public Transform GetFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
