using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TrainManager))]
public class GameOverManager : MonoBehaviour
{
    public static TrainManager trainManager;

    [Space(10)]
    [Header("Left")]
    [Header("Carts labels")]
    public TMP_Text passengerCount;
    public TMP_Text deathCount;
    public TMP_Text cartsCount;
    // Carts
    public TMP_Text housingCount;
    public TMP_Text farmCount;
    public TMP_Text batteryCount;
    public TMP_Text reactorCount;
    public TMP_Text extraEngineCount;
    public TMP_Text specialCartCount;
    public TMP_Text cartsDecoupled;

    [Space(10)]
    [Header("Right")]
    public TMP_Text distanceTravelled;
    public TMP_Text placesVisited;
    public TMP_Text destinationsReached;
    public TMP_Text dayCount;
    public TMP_Text maxSpeed;
    public TMP_Text engineUpgrades;

    void Start()
    {
        trainManager = GetComponent<TrainManager>();
    }

    void Update()
    {
        passengerCount.SetText(string.Format("Passengers <b><color=#A55757>{0:N0}</color></b> / max  <b><color=#A55757>{1:N0}</color></b>", trainManager.CurrentPassenger, trainManager.maxPassengers));
        deathCount.SetText(string.Format("Death count <b><color=#A55757>{0:N0}</color></b>", trainManager.deadCount));
        cartsCount.SetText(string.Format("Carts <b><color=#A55757>{0:N0}</color></b>", trainManager.GetCartsTotal()));
        housingCount.SetText(string.Format("Housing <b><color=#A55757>{0:N0}</color></b>", trainManager.GetHousingUnits().Count));
        farmCount.SetText(string.Format("Farm <b><color=#A55757>{0:N0}</color></b>", trainManager.GetFarms().Count));
        batteryCount.SetText(string.Format("Battery <b><color=#A55757>{0:N0}</color></b>", trainManager.GetBatteries().Count));
        reactorCount.SetText(string.Format("Reactor <b><color=#A55757>{0:N0}</color></b>", trainManager.GetReactors().Count));
        extraEngineCount.SetText(string.Format("Extra engine <b><color=#A55757>{0:N0}</color></b>", trainManager.GetExtraEngines().Count));
        specialCartCount.SetText(string.Format("Special carts <b><color=#A55757>{0:N0}</color></b>", trainManager.specialCartCount));
        cartsDecoupled.SetText(string.Format("Carts decoupled <b><color=#A55757>{0:N0}</color></b>", trainManager.cartsDecoupled));

        distanceTravelled.SetText(string.Format("Battery <b><color=#A55757>{0:N0}</color></b>", trainManager.distanceTravelled));
        placesVisited.SetText(string.Format("Reactor <b><color=#A55757>{0:N0}</color></b>", trainManager.placesVisited));
        destinationsReached.SetText(string.Format("Extra engine <b><color=#A55757>{0:N0}</color></b>", trainManager.destinationsReached));
        dayCount.SetText(string.Format("Day count <b><color=#A55757>{0:N0}</color></b>", trainManager.dayCount));
        maxSpeed.SetText(string.Format("Max speed <b><color=#A55757>{0:N0}</color></b>", trainManager.maxSpeed));
        engineUpgrades.SetText(string.Format("Engineupgrades <b><color=#A55757>{0:N0}</color></b>", trainManager.engineUpgrades));
    }
}
