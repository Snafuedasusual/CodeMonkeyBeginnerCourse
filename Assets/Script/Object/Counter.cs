using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] Transform topOfCounter;
    [SerializeField] KitchenObjSO kitchenObjSO;

    private KitchenObject kitchenObject;

    public override void Interaction(Player player)
    {
        if (kitchenObject == null)
        { 
            Transform kitchenObjSOTrans = Instantiate(kitchenObjSO.prefab, topOfCounter);
            kitchenObjSOTrans.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetFollowTransform()
    {
        return topOfCounter;
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
