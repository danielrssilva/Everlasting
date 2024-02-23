using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TrainManager))]
public class HudManager : MonoBehaviour
{
    public static TrainManager trainManager;


    [Space(10)]
    [Header("Top UI")]
    public TMP_Text dayCount;

    [Space(10)]
    [Header("Modals")]
    [Header("Carts labels")]
    public TMP_Text cartModalHousingUnits;
    public TMP_Text cartModalFarms;
    public TMP_Text cartModalBatteries;
    public TMP_Text cartModalReactors;
    public TMP_Text cartModalExtraEngines;
    // Special modifiers
    public TMP_Text cartModalWeightModifierCartCount;
    public TMP_Text cartModalWeightModifier;
    public TMP_Text cartModalSpeedReducerCartCount;
    public TMP_Text cartModalSpeedReducer;
    // Extra text
    public TMP_Text cartModalExtraText;
    // Bottom
    public TMP_Text cartModalTotalCart;

    [Space(5)]
    [Header("Food labels")]
    public TMP_Text foodModalOutput;
    public TMP_Text foodModalFarmCart;
    public TMP_Text foodModalPassenger;
    public TMP_Text foodModalModifiers;
    public TMP_Text foodModalTrackModifier;
    public TMP_Text foodModalEngineModifier;
    // Bottom
    public TMP_Text foodModalBottomCurrent;
    public TMP_Text foodModalBottomStorage;

    [Space(5)]
    [Header("Energy labels")]
    public TMP_Text energyModalStorage;
    public TMP_Text energyModalStorageEngine;
    public TMP_Text energyModalStorageBatteryCount;
    public TMP_Text energyModalStorageBattery;
    public TMP_Text energyModalOutput;
    public TMP_Text energyModalOutputMainEngine;
    public TMP_Text energyModalOutputReactorCount;
    public TMP_Text energyModalOutputReactor;
    public TMP_Text energyModalOutputProduction;
    public TMP_Text energyModalOutputCartWeight;
    public TMP_Text energyModalOutputSpeed;
    public TMP_Text energyModalModifiers;
    public TMP_Text energyModalModifiersEngine;
    public TMP_Text energyModalModifiersTrack;
    // Bottom
    public TMP_Text energyModalBottomCurrent;
    public TMP_Text energyModalBottomStorage;

    [Space(5)]
    [Header("Engine labels")]
    public TMP_Text engineModalBase;
    public TMP_Text engineModalCurrent;
    public TMP_Text engineModalModifiers;
    public TMP_Text engineModalModifiersWeight;
    public TMP_Text engineModalModifiersExtraEngines;
    public TMP_Text engineModalModifiersTrack;
    public TMP_Text engineModalModifiersEngine;
    public TMP_Text engineModalModifiersDestinations;
    // Bottom
    public TMP_Text engineModalBottomSpeedEnergy;

    [Space(5)]
    [Header("Passenger labels")]
    public TMP_Text passengerModalActiveWorkers;
    public TMP_Text passengerModalFarmCount;
    public TMP_Text passengerModalActiveWorkersFarm;
    public TMP_Text passengerModalActiveWorkersFarmTotal;
    public TMP_Text passengerModalReactorCount;
    public TMP_Text passengerModalActiveWorkersReactor;
    public TMP_Text passengerModalActiveWorkersReactorTotal;
    public TMP_Text passengerModalExtraEngineCount;
    public TMP_Text passengerModalActiveWorkersExtraEngine;
    public TMP_Text passengerModalActiveWorkersExtraEngineTotal;
    public TMP_Text passengerModalUnenployed;
    public TMP_Text passengerModalUnenployable;
    // Bottom
    public TMP_Text passengerModalBottomDead;
    public TMP_Text passengerModalBottomCurrent;
    public TMP_Text passengerModalBottomTotalHousing;

    [Space(10)]
    [Header("Cart manager")]
    public TMP_Text farmManufactureFoodCost;
    public TMP_Text farmManufactureEnergyCost;
    public TMP_Text housingManufactureFoodCost;
    public TMP_Text housingManufactureEnergyCost;
    public TMP_Text reactorManufactureFoodCost;
    public TMP_Text reactorManufactureEnergyCost;
    public TMP_Text batteryManufactureFoodCost;
    public TMP_Text batteryManufactureEnergyCost;
    public TMP_Text extraEngineManufactureFoodCost;
    public TMP_Text extraEngineManufactureEnergyCost;

    [Space(10)]
    [Header("Bottom UI")]
    public Image pauseButton;
    public Image playButton;
    public Image fastForwardButton;
    [Space(5)]
    public TMP_Text currentPassenger;
    public TMP_Text passengerHousingTotal;
    [Space(5)]
    public TMP_Text currentFood;
    public TMP_Text foodProduction;
    [Space(5)]
    public TMP_Text currentEnergy;
    public TMP_Text energyProduction;
    [Space(5)]
    public TMP_Text baseSpeed;
    public TMP_Text speedEnergyProduction;
    public TMP_Text currentSpeed;

    [Space(10)]
    [Header("Sprites")]
    public Sprite pauseButtonInactive;
    public Sprite pauseButtonActive;
    public Sprite playButtonInactive;
    public Sprite playButtonActive;
    public Sprite fastForwardButtonInactive;
    public Sprite fastForwardButtonActive;

    void Start()
    {
        trainManager = GetComponent<TrainManager>();
    }

    void Update()
    {
        // Top UI
        dayCount.SetText(trainManager.dayCount.ToString());

        // Modals  - Carts labels
        cartModalHousingUnits.SetText(trainManager.GetHousingUnits().Count.ToString());
        cartModalFarms.SetText(trainManager.GetFarms().Count.ToString());
        cartModalBatteries.SetText(trainManager.GetBatteries().Count.ToString());
        cartModalReactors.SetText(trainManager.GetReactors().Count.ToString());
        cartModalExtraEngines.SetText(trainManager.GetExtraEngines().Count.ToString());
        cartModalWeightModifierCartCount.SetText(string.Format("[{0:N0}]", trainManager.GetCartsTotal().ToString()));
        cartModalWeightModifier.SetText(string.Format("-{0:N1}", trainManager.cartsEnergyConsumption * trainManager.GetCartsTotal()));
        cartModalSpeedReducerCartCount.SetText(string.Format("[{0:N0}]", trainManager.GetCartsTotal() / 5));
        cartModalSpeedReducer.SetText(string.Format("-{0:N1}", (trainManager.GetCartsTotal() / 5) * trainManager.slowdownAmount));
        cartModalExtraText.SetText(string.Format("Carts consume <b>-{0:N1}<size=4>kw/s</size></b> due to their wheight (even when disabled)<br><br>every <b>{1:N0}</b> carts reduce engine speed by <b>-{2:N0}<size=4>km/h</size></b><br><br>Battery carts count as <b>2</b> carts", trainManager.cartsEnergyConsumption, trainManager.cartsToSlowdown, trainManager.slowdownAmount));
        cartModalTotalCart.SetText(trainManager.GetCartsTotal().ToString());
        // Modals  - Food labels
        foodModalOutput.SetText(string.Format("{0:N1}", trainManager.GetFoodProduction()));
        foodModalFarmCart.SetText(string.Format("{0:N1}", trainManager.GetFarmsOutputProduction()));
        foodModalPassenger.SetText(string.Format("-{0:N1}", (trainManager.passengerFoodConsumption * trainManager.CurrentPassenger)));
        //foodModalModifiers.SetText(string.Format("{0:N1}", (trainManager.farmOutput * trainManager.GetFarms().Count)));
        //foodModalTrackModifier.SetText(string.Format("{0:N1}", (trainManager.farmOutput * trainManager.GetFarms().Count)));
        //foodModalEngineModifier.SetText(string.Format("{0:N1}", (trainManager.farmOutput * trainManager.GetFarms().Count)));
        foodModalBottomCurrent.SetText(string.Format("{0:N1}", trainManager.CurrentFood));
        foodModalBottomStorage.SetText(string.Format("{0:N1}", trainManager.GetFoodStorage()));

        // Modals  - Energy labels
        energyModalStorage.SetText(string.Format("{0:N1}", trainManager.energyStorage));
        energyModalStorageEngine.SetText(string.Format("{0:N1}", trainManager.GetEnergyStorage()));
        energyModalStorageBatteryCount.SetText(string.Format("[{0:N0}]", trainManager.GetBatteries().Count));
        energyModalStorageBattery.SetText(string.Format("{0:N1}", trainManager.batteryModifier * trainManager.GetBatteries().Count));
        energyModalOutput.SetText(string.Format("{0:N1}", trainManager.GetEnergyProduction()));
        energyModalOutputMainEngine.SetText(string.Format("{0:N1}", trainManager.kwPerKmPerS));
        energyModalOutputReactorCount.SetText(string.Format("[{0:N0}]", trainManager.GetReactors().Count));
        energyModalOutputReactor.SetText(string.Format("{0:N1}", trainManager.GetReactorsOutputProduction()));
        energyModalOutputProduction.SetText(string.Format("-{0:N1}", trainManager.cartsEnergyConsumption * trainManager.GetCartsTotal() + trainManager.cartsEnergyConsumption * trainManager.GetBatteries().Count));
        energyModalOutputCartWeight.SetText(string.Format("-{0:N1}", trainManager.cartsEnergyConsumption * trainManager.GetCartsTotal()));
        energyModalOutputSpeed.SetText(string.Format("{0:N2}", trainManager.kwPerKmPerS));
        //energyModalModifiers.SetText(string.Format("{0:N1}%", trainManager.reactorOutput * trainManager.GetReactors.Count));
        //energyModalModifiersEngine.SetText(string.Format("{0:N1}%", trainManager.reactorOutput * trainManager.GetReactors.Count));
        //energyModalModifiersTrack.SetText(string.Format("{0:N1}%", trainManager.reactorOutput * trainManager.GetReactors.Count));
        energyModalBottomCurrent.SetText(string.Format("{0:N1}", trainManager.CurrentEnergy));
        energyModalBottomStorage.SetText(string.Format("{0:N1}", trainManager.GetEnergyStorage()));

        // Modals  - Engine labels
        engineModalBase.SetText(string.Format("{0:N}", trainManager.initialSpeed));
        engineModalCurrent.SetText(string.Format("{0:N}", trainManager.CurrentSpeed));
        engineModalModifiers.SetText(string.Format("{0:N}", trainManager.cartsEnergyConsumption * trainManager.GetCartsTotal() + trainManager.extraEngineSpeedOutput * trainManager.GetExtraEngines().Count));
        engineModalModifiersWeight.SetText(string.Format("-{0:N1}", trainManager.cartsEnergyConsumption * (trainManager.GetCartsTotal() / trainManager.cartsToSlowdown)));
        engineModalModifiersExtraEngines.SetText(string.Format("{0:N1}", trainManager.extraEngineSpeedOutput * trainManager.GetExtraEngines().Count));
        //engineModalModifiersTrack.SetText(string.Format("{0:N}%", trainManager.CurrentSpeed));
        //engineModalModifiersEngine.SetText(string.Format("{0:N}%", trainManager.CurrentSpeed));
        engineModalModifiersDestinations.SetText(string.Format("{0:N2}", trainManager.destinationsSpeedModifier * trainManager.destinationsReached));
        engineModalBottomSpeedEnergy.SetText(string.Format("{0:N}", trainManager.kwPerKmPerS));

        // Modals  - Passenger labels
        passengerModalActiveWorkers.SetText(trainManager.CurrentWorkers.ToString());
        passengerModalFarmCount.SetText(string.Format("[{0:N0}]", trainManager.GetFarms().Count.ToString()));
        passengerModalActiveWorkersFarm.SetText(trainManager.GetCurrentActiveFarmWorkers().ToString());
        passengerModalActiveWorkersFarmTotal.SetText(string.Format("{0:N0}", trainManager.farmWorkersCapacityPerCart * trainManager.GetFarms().Count));
        passengerModalReactorCount.SetText(string.Format("[{0:N0}]", trainManager.GetReactors().Count.ToString()));
        passengerModalActiveWorkersReactor.SetText(trainManager.GetCurrentActiveReactorWorkers().ToString());
        passengerModalActiveWorkersReactorTotal.SetText(string.Format("{0:N0}", trainManager.reactorWorkersRequiredPerCart * trainManager.GetReactors().Count));
        passengerModalExtraEngineCount.SetText(string.Format("[{0:N0}]", trainManager.GetExtraEngines().Count.ToString()));
        passengerModalActiveWorkersExtraEngine.SetText(trainManager.GetCurrentActiveExtraEngineWorkers().ToString());
        passengerModalActiveWorkersExtraEngineTotal.SetText(string.Format("{0:N0}", trainManager.extraEngineWorkersRequiredPerCart * trainManager.GetExtraEngines().Count));
        passengerModalUnenployed.SetText(trainManager.CurrentUnemployed.ToString());
        passengerModalUnenployable.SetText(trainManager.CurrentUnemployable.ToString());
        // Bottom
        passengerModalBottomDead.SetText(trainManager.deadCount.ToString());
        passengerModalBottomCurrent.SetText(trainManager.CurrentPassenger.ToString());
        passengerModalBottomTotalHousing.SetText((trainManager.GetHousingUnits().Count * 5).ToString());

        // Cart manager
        farmManufactureFoodCost.SetText(string.Format("{0:N0}", trainManager.CurrentFarmFoodCost));
        farmManufactureEnergyCost.SetText(string.Format("{0:N1}", trainManager.CurrentFarmEnergyCost));
        housingManufactureFoodCost.SetText(string.Format("{0:N1}", trainManager.CurrentHousingFoodCost));
        housingManufactureEnergyCost.SetText(string.Format("{0:N1}", trainManager.CurrentHousingEnergyCost));
        reactorManufactureFoodCost.SetText(string.Format("{0:N1}", trainManager.CurrentReactorFoodCost));
        reactorManufactureEnergyCost.SetText(string.Format("{0:N1}", trainManager.CurrentReactorEnergyCost));
        batteryManufactureFoodCost.SetText(string.Format("{0:N1}", trainManager.CurrentBatteryFoodCost));
        batteryManufactureEnergyCost.SetText(string.Format("{0:N1}", trainManager.CurrentBatteryEnergyCost));
        extraEngineManufactureFoodCost.SetText(string.Format("{0:N1}", trainManager.CurrentExtraEngineFoodCost));
        extraEngineManufactureEnergyCost.SetText(string.Format("{0:N1}", trainManager.CurrentExtraEngineEnergyCost));

        // Bottom UI
        if (trainManager.TimePaused)
        {
            playButton.sprite = playButtonInactive;
            pauseButton.sprite = pauseButtonActive;
            fastForwardButton.sprite = fastForwardButtonInactive;
        }
        else
        {
            playButton.sprite = playButtonActive;
            pauseButton.sprite = pauseButtonInactive;
            fastForwardButton.sprite = fastForwardButtonInactive;
        }
        if (trainManager.FastForward)
        {
            playButton.sprite = playButtonInactive;
            pauseButton.sprite = pauseButtonInactive;
            fastForwardButton.sprite = fastForwardButtonActive;
        }
        currentPassenger.SetText(trainManager.CurrentPassenger.ToString());
        passengerHousingTotal.SetText((5 * trainManager.GetHousingUnits().Count).ToString());

        currentFood.SetText(string.Format("{0:N1}", trainManager.CurrentFood));
        foodProduction.SetText(string.Format("{0:N1}", trainManager.GetFoodProduction()));

        currentEnergy.SetText(string.Format("{0:N1}", trainManager.CurrentEnergy));
        energyProduction.SetText(string.Format("{0:N1}", trainManager.GetEnergyProduction()));

        baseSpeed.SetText(trainManager.initialSpeed.ToString());
        speedEnergyProduction.SetText(string.Format("{0:N2}", trainManager.kwPerKmPerS));
        currentSpeed.SetText(string.Format("{0:N0}", trainManager.CurrentSpeed));
    }
}
