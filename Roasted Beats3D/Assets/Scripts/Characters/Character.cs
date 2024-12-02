using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MoveIntoScene), typeof(Order))]
public class Character : MonoBehaviour
{
    [SerializeField] public MoveIntoScene MIS;
    [SerializeField] public Order order;

    public bool OrderTaken {  get; set; }

    private List<FoodItem> orderList;

    public bool ReadyToDelete {  get; private set; }

    public void CreateCharacter(OrderListSO orderList)
    {
        this.orderList = order.GetOrder(orderList);
        OrderTaken = false;
    }
    public void CreateCharacter(int[] order, OrderListSO orderList)
    {
        this.orderList = this.order.GetOrder(order, orderList);
        OrderTaken = false;
    }

    public IEnumerator SpawnCustomer()
    {
        yield return StartWalkIn();

        yield return WaitForOrder();

        yield return StartWalkOut();
    }

    private IEnumerator StartWalkIn()
    {
        while (!MIS.IsReadyToOrder)
        {
            yield return new WaitForNextFrameUnit();
        }
    }

    private IEnumerator StartWalkOut()
    {
        //Debug.Log(MIS.EndPos.position);
        MIS.OrderRecieved = true;

        while (!MIS.WalkedOut)
        {
            yield return new WaitForNextFrameUnit();
        }

        ReadyToDelete = true;
    }

    private IEnumerator WaitForOrder()
    {
        DisplayOrder(true);
        GlobalVar.Instance.Ordering = true;

        while (!OrderTaken)
        {
            yield return new WaitForNextFrameUnit();
        }

        DisplayOrder(false);
    }

    public void SetOrderTaken(bool value)
    {
        if (!MIS.IsReadyToOrder) return;
        OrderTaken = value;
    }

    private void DisplayOrder(bool value)
    {
        order.BubblePos.gameObject.SetActive(value);
    }
}
