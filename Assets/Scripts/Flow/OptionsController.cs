using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelPanda.ProcGen.Elements.Obstacle;
using VoxelPanda.ProcGen.Elements;
using VoxelPanda.Flow;

public class OptionsController : MonoBehaviour
{
    struct BtnWithIndex
    {
        public Button btn;
        public int index;

        public BtnWithIndex(Button btn, int index)
        {
            this.btn = btn;
            this.index = index;
        }
    }

    public Button optionsButton;
    public GameObject optionsOverlay;

    public ConstMoveData constMoveData;
    public SpawnData spawnData;

    public GameObject smallWall;
    public GameObject medWall;
    public GameObject largeWall;
    public GameObject train2;
    public GameObject train4;
    public GameObject sand;
    public GameObject cannon;
    public GameObject conveyor;
    public GameObject roboticArm;
    public BulletBehaviour bulletBehaviour;

    public GridData armSoloData;
    public GridData barrageData;
    public GridData crossFireData;
    public GridData doubleCannonData;
    public GridData sandTunnelData;
    public GridData sandyArmData;
    public GridData trainStationData;

    private GridData smallWallData;
    private GridData medWallData;
    private GridData largeWallData;
    private GridData train2Data;
    private GridData train4Data;
    private GridData sandData;
    private GridData cannonData;
    private GridData conveyorData;
    private GridData roboticArmData;

    private TrainBehaviour trainBehaviour;
    private SandBehaviour sandBehaviour;
    private CannonBehaviour cannonBehaviour;
    private ConveyorBehaviour conveyorBehaviour;
    private RoboticArmBehaviour roboticArmBehaviour;

    private List<GameObject> menus = new List<GameObject>();
    private List<BtnWithIndex> indexedBtns = new List<BtnWithIndex>();

    private GameManager menager;

    private void Start()
    {
        InputField[] inputs = optionsOverlay.GetComponentsInChildren<InputField>(true);
        for(int i = 0; i < inputs.Length; i++)
        {
            inputs[i].keyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
        }


        trainBehaviour = (TrainBehaviour)GetBehaviour(train2);
        sandBehaviour = (SandBehaviour)GetBehaviour(sand);
        cannonBehaviour = (CannonBehaviour)GetBehaviour(cannon);
        conveyorBehaviour = (ConveyorBehaviour)GetBehaviour(conveyor);
        roboticArmBehaviour = (RoboticArmBehaviour)GetBehaviour(roboticArm);

        smallWallData = GetGridData(smallWall);
        medWallData = GetGridData(medWall);
        largeWallData = GetGridData(largeWall);
        train2Data = GetGridData(train2);
        train4Data = GetGridData(train4);
        sandData = GetGridData(sand);
        conveyorData = GetGridData(conveyor);
        cannonData = GetGridData(cannon);
        roboticArmData = GetGridData(roboticArm);

        for (int i = 0; i < optionsOverlay.transform.childCount; i++)
        {
            menus.Add(optionsOverlay.transform.GetChild(i).gameObject);
        }
       
        SetOptionValues();
        AttachEvents();
    }

    public void SetGameManager(GameManager menager)
    {
        this.menager = menager;
    }

    private void AttachEvents()
    {
        optionsButton.onClick.AddListener(OpenOptions);

       for(int k = 0; k < menus.Count; k++)
       {
          GameObject menu = menus[k];
          for(int i = 0; i < menu.transform.childCount; i++)
          {
                if(menu.transform.GetChild(i).name == "Buttons")
                {
                    GameObject buttonContainer = menu.transform.GetChild(i).gameObject;

                    for(int j = 0; j < buttonContainer.transform.childCount; j++)
                    {
                        GameObject btn = buttonContainer.transform.GetChild(j).gameObject;

                        if (btn.name == "PreviousButton")
                        {
                            if (k == 0)
                                btn.GetComponent<Button>().interactable = false;
                            else
                            {
                                AddButton(btn.GetComponent<Button>(), k);
                                btn.GetComponent<Button>().onClick.AddListener(delegate { OpenPrevious(btn.GetComponent<Button>()); });                               
                            }                               
                        }

                        if(btn.name == "NextButton")
                        {
                            if (k == menus.Count - 1)
                                btn.GetComponent<Button>().interactable = false;
                            else
                            {
                                AddButton(btn.GetComponent<Button>(), k);
                                btn.GetComponent<Button>().onClick.AddListener(delegate { OpenNext(btn.GetComponent<Button>()); });                               
                            }                             
                        }

                        if(btn.name == "ConfirmButton")
                        {
                            btn.GetComponent<Button>().onClick.AddListener(ConfirmOptions);
                        }
                    }
                }        
          }
       }
    }


    void SetOptionValues()
    {
        //Fling
        constMoveData.flingForce = PlayerPrefs.GetFloat("flingForceInp", constMoveData.flingForce);
        constMoveData.staminaPerFling = PlayerPrefs.GetFloat("staminaConsumedInp", constMoveData.staminaPerFling);
        constMoveData.staminaRegenDelay = PlayerPrefs.GetFloat("regenDelayInp", constMoveData.staminaRegenDelay);
        constMoveData.staminaRegenPerSecond = PlayerPrefs.GetFloat("staminaPerSecInp", constMoveData.staminaRegenPerSecond);
        constMoveData.vectorStaminaCost = PlayerPrefs.GetFloat("vectorStaminaCostInp", constMoveData.vectorStaminaCost);

        //curve
        constMoveData.curveForce = PlayerPrefs.GetFloat("curveForceInp", constMoveData.curveForce);
        constMoveData.accelerationDeadzone = PlayerPrefs.GetFloat("deadzoneInp", constMoveData.accelerationDeadzone);
        constMoveData.accelerationFunctionModifier = PlayerPrefs.GetFloat("accModifierInp", constMoveData.accelerationFunctionModifier);
        constMoveData.minVelocityForCurve = PlayerPrefs.GetFloat("minAccVelocityInp", constMoveData.minVelocityForCurve);

        //weights
        smallWallData.weight = (int)PlayerPrefs.GetFloat("smallWallWeightInp", smallWallData.weight);
        medWallData.weight = (int)PlayerPrefs.GetFloat("medWallWeightInp", medWallData.weight);
        largeWallData.weight = (int)PlayerPrefs.GetFloat("largeWallWeightInp", largeWallData.weight);
        train2Data.weight = (int)PlayerPrefs.GetFloat("train2WeightInp", train2Data.weight);
        train4Data.weight = (int)PlayerPrefs.GetFloat("train4WeightInp", train4Data.weight);
        sandData.weight = (int)PlayerPrefs.GetFloat("sandWeightInp", sandData.weight);
        roboticArmData.weight = (int)PlayerPrefs.GetFloat("roboticArmWeightInp", roboticArmData.weight);
        conveyorData.weight = (int)PlayerPrefs.GetFloat("conveyorWeightInp", conveyorData.weight);
        cannonData.weight = (int)PlayerPrefs.GetFloat("cannonWeightInp", cannonData.weight);
        spawnData.coinSpawnRiskyChance = PlayerPrefs.GetFloat("riskyCoinInp", spawnData.coinSpawnRiskyChance);
        spawnData.coinSpawnDangerousChance = PlayerPrefs.GetFloat("dangerousCoinInp", spawnData.coinSpawnDangerousChance);
        spawnData.coinSpawnCriticalChance = PlayerPrefs.GetFloat("criticalCoinInp", spawnData.coinSpawnCriticalChance);
        armSoloData.weight = (int)PlayerPrefs.GetFloat("armSoloWeightInp", armSoloData.weight);
        barrageData.weight = (int)PlayerPrefs.GetFloat("barrageWeightInp", barrageData.weight);
        crossFireData.weight = (int)PlayerPrefs.GetFloat("crossFireWeightInp", crossFireData.weight);
        doubleCannonData.weight = (int)PlayerPrefs.GetFloat("doubleCannonWeightInp", doubleCannonData.weight);
        sandTunnelData.weight = (int)PlayerPrefs.GetFloat("sandTunnelWeightInp", sandTunnelData.weight);
        sandyArmData.weight = (int)PlayerPrefs.GetFloat("sandyArmWeightInp", sandyArmData.weight);
        trainStationData.weight = (int)PlayerPrefs.GetFloat("trainStationWeightInp", trainStationData.weight);

        //behaviours
        bulletBehaviour.speed = PlayerPrefs.GetFloat("bulletSpeedInp", bulletBehaviour.speed);
        bulletBehaviour.secondsActive = PlayerPrefs.GetFloat("bulletLifetimeInp", bulletBehaviour.secondsActive);
        bulletBehaviour.explosionRadius = PlayerPrefs.GetFloat("explosionRadiusInp", bulletBehaviour.explosionRadius);
        bulletBehaviour.explosionKnockback = PlayerPrefs.GetFloat("explosionForceInp", bulletBehaviour.explosionKnockback);
        cannonBehaviour.secondsBetweenShots = PlayerPrefs.GetFloat("cannonFreqInp", cannonBehaviour.secondsBetweenShots);
        conveyorBehaviour.speed = PlayerPrefs.GetFloat("conveyorForceInp", conveyorBehaviour.speed);
        sandBehaviour.slowdownMultiplier = PlayerPrefs.GetFloat("sandForceInp", sandBehaviour.slowdownMultiplier);
        roboticArmBehaviour.speed = PlayerPrefs.GetFloat("roboticArmSpeedInp", roboticArmBehaviour.speed);
        roboticArmBehaviour.pauseTime = PlayerPrefs.GetFloat("roboticArmPauseTimeInp", roboticArmBehaviour.pauseTime);
        roboticArmBehaviour.angleAmountCheckpoint = PlayerPrefs.GetFloat("roboticArmAngleInp", roboticArmBehaviour.angleAmountCheckpoint);
        trainBehaviour.trainSpeed = PlayerPrefs.GetFloat("trainSpeedInp", trainBehaviour.trainSpeed);
        trainBehaviour.trainStoppageTime = PlayerPrefs.GetFloat("trainStoppageInp", trainBehaviour.trainStoppageTime);
    }

    void SetInputFieldValues()
    {
        for(int i = 0; i < menus.Count; i++)
        {
            Component[] inputs = menus[i].GetComponentsInChildren(typeof(InputField));
            foreach (Component inp in inputs)
            {
                (inp as InputField).text = PlayerPrefs.GetFloat(inp.name).ToString();
            }
        }
    }

    void AddButton(Button btn, int index)
    {
        indexedBtns.Add(new BtnWithIndex(btn, index));
    }

    private void OpenOptions()
    {
        Time.timeScale = 0.0f;

        SetInputFieldValues();
        optionsOverlay.SetActive(true);
    }

    private void ConfirmOptions()
    {
        Time.timeScale = 1.0f;
        optionsOverlay.SetActive(false);

        SetPlayerPrefs();
        SetOptionValues();

        menager.OptionsReset();
    }

    void SetPlayerPrefs()
    {
        for (int i = 0; i < menus.Count; i++)
        {
            Component[] inputs = menus[i].GetComponentsInChildren(typeof(InputField));
            foreach (Component inp in inputs)
            {
                if((inp as InputField).text != "0")
                    PlayerPrefs.SetFloat(inp.name, float.Parse((inp as InputField).text));
            }
        }
    }

    private void OpenNext(Button button)
    {
        foreach (BtnWithIndex btn in indexedBtns)
        {
            if(btn.btn == button)
            {
                menus[btn.index].SetActive(false);
                menus[btn.index + 1].SetActive(true);
            }
        }
    }

    private void OpenPrevious(Button button)
    {
        foreach (BtnWithIndex btn in indexedBtns)
        {
            if (btn.btn == button)
            {
                menus[btn.index].SetActive(false);
                menus[btn.index - 1].SetActive(true);
            }
        }
    }

    private ObsBehaviour GetBehaviour(GameObject obstacle)
    {
        ObsBehaviour behaviour = (ObsBehaviour)obstacle.GetComponentInChildren(typeof(ObsBehaviour));

        if (behaviour != null)
            return behaviour;
        else
            Debug.LogError("Ne radi behaviour");
        return null;
    }

    private GridData GetGridData(GameObject obstacle)
    {
        GridData gridData = obstacle.GetComponent<GridData>();

        if (gridData != null)
            return gridData;
        else
            Debug.LogError("Ne radi grid");
        return null;
    }
}
