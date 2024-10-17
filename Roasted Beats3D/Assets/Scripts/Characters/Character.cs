using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MoveIntoScene), typeof(Order))]
public class Character : MonoBehaviour
{
    [SerializeField] private MoveIntoScene MIS;
    [SerializeField] private Order order;

    public bool OrderTaken {  get; set; }

    private List<FoodItem> orderList;


    void Start()
    {
        orderList = order.GetOrder();
        OrderTaken = false;
    }

    private IEnumerator SpawnCustomer()
    {
        yield return StartWalkIn();

        yield return WaitForOrder();

        yield return StartWalkOut();
    }

    private IEnumerator StartWalkIn()
    {
        while (!MIS.IsStill)
        {
            yield return new WaitForNextFrameUnit();
        }
    }

    private IEnumerator StartWalkOut()
    {
        while (!MIS.IsStill)
        {
            yield return new WaitForNextFrameUnit();
        }

        Destroy(gameObject);
    }

    private IEnumerator WaitForOrder()
    {


        while (!OrderTaken)
        {
            yield return new WaitForNextFrameUnit();
        }
    }

    private void DisplayOrder()
    {

    }
}
