using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CafeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterPrefabs;
    [SerializeField] private float waitBetweenCustomers = 30;

    [SerializeField] private OrderListSO orderList;

    [SerializeField] private Transform charStandPos;
    [SerializeField] private Transform charEndPos;
    [SerializeField] private Transform charStartPos;

    [SerializeField] private float charSpacing = 5;

    [SerializeField] private List<string> levelSongNames;

    private List<GameObject> characters;
    private bool CharacterUpdate;

    [SerializeField] private RhythmManager rMan;

    private float lifePercent;

    private void Start()
    {
        characters = new List<GameObject>();

        // Drop all but one instance of manager
        bool n = RhythmManager.Instance.hasStarted;

        StartCoroutine(StartCafeScene());

        // Wake up globalvar
        GlobalVar.Instance.notesPassed = 0;

        RhythmManager.Instance.ChangeVolume(.1f);
        lifePercent=GlobalVar.Instance.lifePercent;

        StartCoroutine(SaveHeartbeat());

        if (GlobalVar.Instance.saveData.customers == null || GlobalVar.Instance.saveData.customers.Length == 0) return;

        LoadCustomers(GlobalVar.Instance.saveData.customers);

    }

    private void LoadCustomers(CustomerData[] customers)
    {
        for (int i = 0; i < customers.Length; i++)
        {
            Vector3 pos = customers[i].position;
            string name = customers[i].name;
            int[] order = customers[i].orderData;

            characters.Add(Instantiate(characterPrefabs[int.Parse(name)], charStandPos.position, Quaternion.identity, transform));
            Character temp = characters[characters.Count - 1].GetComponent<Character>();

            temp.CreateCharacter(order, orderList);
            temp.MIS.EndPos = charEndPos;
            temp.MIS.StandPos = charStandPos;

            StartCoroutine(temp.SpawnCustomer());

            CharacterUpdate = true;
        }
    }

    private IEnumerator SaveHeartbeat()
    {
        while (true)
        {
            CustomerData[] data = new CustomerData[characters.Count];

            for (int i = 0; i < data.Length; i++)
            {
                int[] orderData = characters[i].GetComponent<Character>().order.foodOrderInt.ToArray();

                data[i] = new CustomerData(i.ToString(), characters[i].transform.position, orderData);
            }

            GameSave.Instance.SaveCustomers(data);

            yield return new WaitForSecondsRealtime(2);
        }
    }

    /// <summary>
    /// Starts all cafe scene specific coroutines that need tro be cycled through
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCafeScene()
    {
        StartCoroutine(SpawnCharacter());

        while (true)
        {

            // Update each character (not 1st) position in line
            for (int i = 1; i < characters.Count; i++)
            {
                Character currentChar = characters[i].GetComponent<Character>();

                // If the character is heading to the end, don't do anything
                if (currentChar.MIS.currentTargetPos == charEndPos.position) continue;

                currentChar.MIS.SetTargetPosition(charStandPos.position + new Vector3(charSpacing * i, 0));
            }

            // Deletes current customer and then sets the next customer if there is one to come up to the cash register
            if (characters.Count > 1 && characters[0].GetComponent<Character>().MIS.IsMoving && characters[0].GetComponent<Character>().MIS.OrderRecieved)
            {
                characters[1].GetComponent<Character>().MIS.SetTargetPosition(charStandPos.position);
            }

            if (characters.Count > 0 && characters[0].GetComponent<Character>().ReadyToDelete)
            {
                Destroy(characters[0]);
                characters.RemoveAt(0);
            }

            yield return new WaitForNextFrameUnit();
        }
    }

    /// <summary>
    /// Will spawn customers continuously using a cooldown
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnCharacter()
    {
        while (true)
        {
            characters.Add(Instantiate(characterPrefabs[Random.Range(0, characterPrefabs.Count)], charStartPos.position, Quaternion.identity, transform));
            Character temp = characters[characters.Count - 1].GetComponent<Character>();

            temp.CreateCharacter(orderList);
            temp.MIS.EndPos = charEndPos;
            temp.MIS.StandPos = charStandPos;

            StartCoroutine(temp.SpawnCustomer());

            CharacterUpdate = true;

            yield return new WaitForSeconds(waitBetweenCustomers);
        }
    }

    public void SetOrderTaken(bool value)
    {
        if (characters.Count != 0)
            characters[0].GetComponent<Character>().SetOrderTaken(value);
    }
}
