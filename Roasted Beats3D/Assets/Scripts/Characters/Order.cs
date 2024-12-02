using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private OrderListSO orderList;

    [SerializeField] private int itemCount;
    [SerializeField] private GameObject OrderBubble;
    [SerializeField] public Transform BubblePos;

    [SerializeField] private float orderItemSpacing = 1f;

    public List<FoodItem> foodItems {  get; private set; }
    public List<int> foodOrderInt;

    public List<FoodItem> GetOrder(OrderListSO orderList)
    {
        this.orderList = orderList;

        foodItems = new List<FoodItem>();

        foodOrderInt = new List<int>();

        for (int i = 0; i < itemCount; i++)
        {
            int randNum = Random.Range(0, orderList.items.Count);

            foodOrderInt.Add(randNum);

            foodItems.Add(orderList.items[foodOrderInt[i]]);
        }

        SetOrderItems();

        return foodItems;
    }

    public List<FoodItem> GetOrder(int[] order, OrderListSO orderList)
    {
        this.orderList = orderList;

        foodItems = new List<FoodItem> ();

        foodOrderInt = new List<int> ();

        for (int i = 0; i < itemCount; i++)
        {
            foodOrderInt.Add(order[i]);

            foodItems.Add(orderList.items[foodOrderInt[i]]);
        }

        SetOrderItems();

        return foodItems;
    }

    private void SetOrderItems()
    {
        OrderBubble = Instantiate(OrderBubble, BubblePos);
        
        for (int i = 0;i < foodItems.Count;i++)
        {
            float itemOffset = orderItemSpacing;
            float offset = -foodItems.Count / 2 * itemOffset;

            GameObject food = Instantiate(foodItems[i].Cooked, BubblePos.position + new Vector3(i * itemOffset + offset, 0, -.01f), Quaternion.identity, BubblePos);
            food.transform.localScale = Vector3.one * .5f;
        }

        BubblePos.gameObject.SetActive(false);
    }
}
