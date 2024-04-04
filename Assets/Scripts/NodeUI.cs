using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;

    public TextMeshProUGUI upgradeCost;

    public void SetTarget(Node _target)
    {
        target = _target;
        
        transform.position = target.GetBuildPosition();

        upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
        
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
}
