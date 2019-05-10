using UnityEngine;

public class shop : MonoBehaviour {

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;

        ShowCost();

    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    private void ShowCost()
    {
        standardTurret.showCost.text = "$ " + standardTurret.cost.ToString();

        missileLauncher.showCost.text = "$ " + missileLauncher.cost.ToString();

        laserBeamer.showCost.text = "$ " + laserBeamer.cost.ToString(); 

    }
}
