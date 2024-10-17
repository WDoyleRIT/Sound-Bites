using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private OrderListSO orderList;

    [SerializeField] private int itemCount;
    [SerializeField] private GameObject OrderBubble;

    public List<FoodItem> foodItems {  get; private set; }

    public List<FoodItem> GetOrder()
    {
        //CafeManager currentLevel = GameManager.Instance.CurrentLevel;

        //orderList = currentLevel.OrderList;

        foodItems = new List<FoodItem>();

        for (int i = 0; i < itemCount; i++)
        {
            foodItems.Add(orderList.items[Random.Range(0, orderList.items.Count)]);
        }

        SetOrderItems();

        return foodItems;
    }

    private void SetOrderItems()
    {
        OrderBubble = Instantiate(OrderBubble, transform);
        
        for (int i = 0;i < foodItems.Count;i++)
        {
            float itemOffset = 1;
            float offset = -foodItems.Count / 2 * itemOffset;

            Instantiate(foodItems[i].Cooked, OrderBubble.transform.position + new Vector3(i * itemOffset + offset, 0, -.01f), Quaternion.identity, OrderBubble.transform);
        }
    }
}
