using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UpgradeButtonGenerator : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public List<SONodeData> allNodeData;

    [Header("Prefabs & References")]
    public GameObject upgradeButtonPrefab; // Prefab with UpgradeButton component + TMP Text
    public Transform parentContainer; // Parent object to hold buttons
    public PlayerStats playerStats; // Reference to player stats

    public void GenerateButtons()
    {
        if (upgradeButtonPrefab == null || parentContainer == null || playerStats == null)
        {
            Debug.LogWarning("Assign prefab, parent container, and PlayerStats first!");
            return;
        }

        // Create a button for each ScriptableObject
        foreach (var nodeData in allNodeData)
        {
            // Check if a button for this node already exists
            bool exists = false;
            foreach (Transform child in parentContainer)
            {
                UpgradeButton existingBtn = child.GetComponent<UpgradeButton>();
                if (existingBtn != null && existingBtn.nodeInstance.data == nodeData)
                {
                    exists = true;
                    break;
                }
            }

            if (exists) continue; // Skip if already exists

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, parentContainer);
            buttonObj.name = nodeData.name;

            UpgradeButton btn = buttonObj.GetComponent<UpgradeButton>();
            if (btn != null)
            {
                NodeInstance nodeInstance = new NodeInstance();
                nodeInstance.data = nodeData;
                btn.nodeInstance = nodeInstance;

                btn.playerStats = playerStats;

                TMP_Text tmp = buttonObj.GetComponentInChildren<TMP_Text>();
                if (tmp != null)
                {
                    tmp.text = nodeData.name; // Show asset name
                }
            }
        }

        Debug.Log("Generated Upgrade Buttons! Total: " + parentContainer.childCount);
    }
}
