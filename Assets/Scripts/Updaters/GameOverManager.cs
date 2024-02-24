using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TrainManager))]
public class GameOverManager : MonoBehaviour
{
    public static TrainManager trainManager;

    [Space(10)]
    [Header("Top")]
    public TMP_Text dayCount;
    [Header("Train")]
    public TMP_Text passengerCount;
    public TMP_Text deathCount;
    public TMP_Text cartsCount;
    [Header("Carts")]
    public TMP_Text housingCount;
    public TMP_Text farmCount;
    public TMP_Text batteryCount;
    public TMP_Text reactorCount;
    public TMP_Text extraEngineCount;
    public TMP_Text specialCartCount;
    public TMP_Text cartsDecoupled;

    [Space(10)]
    [Header("Map")]
    public TMP_Text distanceTravelled;
    public TMP_Text placesVisited;
    public TMP_Text destinationsReached;
    [Header("Energy")]
    public TMP_Text energyStorage;
    public TMP_Text energyOutput;
    [Header("Food")]
    public TMP_Text foodStorage;
    public TMP_Text foodOutput;
    [Header("Population")]
    public TMP_Text maxSpeed;
    public TMP_Text engineUpgrades;

    void Start()
    {
        trainManager = GetComponent<TrainManager>();
    }

    void Update()
    {
        dayCount.SetText(string.Format("Day count <b><color=#A55757>{0:N0}</color></b>", trainManager.dayCount));

        // Train 
        maxSpeed.SetText(string.Format("Max speed <b><color=#A55757>{0:N2}</color></b>", trainManager.maxSpeed));
        engineUpgrades.SetText(string.Format("Engine upgrades <b><color=#A55757>{0:N0}</color></b>", trainManager.engineUpgrades));
        cartsCount.SetText(string.Format("Carts <b><color=#A55757>{0:N0}</color></b>", trainManager.GetCartsTotal()));
        housingCount.SetText(string.Format("Housing <b><color=#A55757>{0:N0}</color></b>", trainManager.GetHousingUnits().Count));
        farmCount.SetText(string.Format("Farm <b><color=#A55757>{0:N0}</color></b>", trainManager.GetFarms().Count));
        batteryCount.SetText(string.Format("Battery <b><color=#A55757>{0:N0}</color></b>", trainManager.GetBatteries().Count));
        reactorCount.SetText(string.Format("Reactor <b><color=#A55757>{0:N0}</color></b>", trainManager.GetReactors().Count));
        extraEngineCount.SetText(string.Format("Extra engine <b><color=#A55757>{0:N0}</color></b>", trainManager.GetExtraEngines().Count));
        specialCartCount.SetText(string.Format("Special cart <b><color=#A55757>{0:N0}</color></b>", trainManager.specialCartCount));
        cartsDecoupled.SetText(string.Format("Carts decoupled <b><color=#A55757>{0:N0}</color></b>", trainManager.cartsDecoupled));

        // World Map
        distanceTravelled.SetText(string.Format("Distance Travelled <b><color=#A55757>{0:N2}</color></b>", trainManager.distanceTravelled));
        placesVisited.SetText(string.Format("Places visited <b><color=#A55757>{0:N0}</color></b>", trainManager.placesVisited));
        destinationsReached.SetText(string.Format("Destinations reached <b><color=#A55757>{0:N0}</color></b>", trainManager.destinationsReached));

        // Energy
        energyOutput.SetText(string.Format("Storage <b><color=#A55757>{0:N1}</color></b> / max <b><color=#A55757>{1:N1}</color></b>", trainManager.CurrentEnergy, trainManager.GetEnergyStorage()));
        energyStorage.SetText(string.Format("Output <b><color=#A55757>{0:N1}</color></b> / max <b><color=#A55757>{1:N1}</color></b>", trainManager.GetEnergyProduction(), trainManager.maxEnergyProduction));

        // Food
        foodOutput.SetText(string.Format("Storage <b><color=#A55757>{0:N1}</color></b> / max <b><color=#A55757>{1:N1}</color></b>", trainManager.CurrentFood, trainManager.GetFoodStorage()));
        foodStorage.SetText(string.Format("Output <b><color=#A55757>{0:N1}</color></b> / max <b><color=#A55757>{1:N1}</color></b>", trainManager.GetFoodProduction(), trainManager.maxFoodProduction));

        // Population
        passengerCount.SetText(string.Format("Passengers <b><color=#A55757>{0:N0}</color></b> / max <b><color=#A55757>{1:N0}</color></b>", trainManager.CurrentPassenger, trainManager.maxPassengers));
        deathCount.SetText(string.Format("Death count <b><color=#A55757>{0:N0}</color></b>", trainManager.deadCount));
    }
}
