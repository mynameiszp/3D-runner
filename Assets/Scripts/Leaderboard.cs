using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    private Dictionary<string, int> _leaders;

    public static Leaderboard Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void ClearData(Transform table)
    {
        for (int i = 0; i < table.childCount; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);
        }
    }

    public void ShowLeaderboard(Transform table, GameObject rowPrefab)
    {
        ClearData(table);
        GameObject row;
        TextMeshProUGUI[] texts;
        FirebaseManager.Instance.GetUserHighscore((leaders) =>
        {
            _leaders = leaders;
            if (_leaders != null)
            {
                foreach (var leader in _leaders)
                {
                    row = Instantiate(rowPrefab, table);
                    texts = row.GetComponentsInChildren<TextMeshProUGUI>();
                    texts[0].text = leader.Key;
                    texts[1].text = leader.Value.ToString();
                }
            }
        });
    }
}
