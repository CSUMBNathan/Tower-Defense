using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;
    
    public GameObject buildEffect;
    public GameObject sellEffect;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of GameManager");
            return;
        }
        instance = this;
    }
    
    
    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }

    }

   
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
