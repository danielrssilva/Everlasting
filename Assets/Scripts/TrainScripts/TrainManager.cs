using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainManager : MonoBehaviour
{
    public static TrainManager Instance;

    [Header("General stats")]
    public int dayCount = 0;

    [Header("Speed stats")]
    public float initialSpeed = 100f;
    public float minimumSpeed = 0;
    public float kwPerKmPerS = 0.01f;
    public float trackSpeedModifier = 0f;
    public float destinationsSpeedModifier = 0.05f;

    [Header("Energy stats")]
    public float energyStorage = 8f;
    public float minimumEnergyStorage = -2f;
    public float reactorOutput = 1.8f;
    public float farmFoodProductionEnergyCost = 0.05f;
    public float batteryModifier = 1f;
    public float startingEnergy = 5f;
    public float engineEnergyModifier = 1f;
    public float stopsEnergyStorageModifier = 0f;

    [Header("Food stats")]
    public float foodStorage = 1f;
    public float minimumFoodStorage = -1f;
    public float startingFood = 0.6f;
    public float farmOutput = 0.3f;
    public float farmValueAtMaxWorkers = 0.1f;
    public float stopsFoodStorageModifier = 0f;
    public float farmStorageModifier = 0.1f;
    public float engineFoodModifier = 1f;

    [Header("GameOver stats")]
    public int maxPassengers = 0;
    public int maxCarts = 0;
    public int specialCartCount = 0;
    public int cartsDecoupled = 0;
    public float distanceTravelled = 0;
    public int placesVisited = 0;
    public int destinationsReached = 0;
    public float maxSpeed = 0;
    public int engineUpgrades = 0;
    public float maxEnergyProduction = 0;
    public float maxFoodProduction = 0;

    [Header("Passenger stats")]
    public int activeWorkers = 0;
    public int startingPassenger = 0;
    public float passengerFoodConsumption = 0.1f;
    public int timePassengerDieWithoutFood = 60;
    public int deadCount = 0;

    [Header("Farmer workers stats")]
    public int farmWorkers = 0;
    public int farmWorkersCapacityPerCart = 3;

    [Header("Reactor workers stats")]
    public int reactorWorkers = 0;
    public int reactorWorkersRequiredPerCart = 3;

    [Header("Extra engine workers stats")]
    public int extraEngineWorkers = 0;
    public int extraEngineWorkersRequiredPerCart = 2;
    public float extraEngineSpeedOutput = 60f;
    public float extraEngineEnergyConsumption = 50f;

    [Header("Carts stats")]
    public int cartsToSlowdown = 5;
    public float slowdownAmount = 10f;
    public float cartsEnergyConsumption = 0.35f;

    [Header("Carts manufacture stats")]
    public float farmManufactureFoodCost = 0f;
    public float farmManufactureEnergyCost = 0.2f;
    public float farmManufactureCostIncrease = 0.15f;

    public float housingManufactureFoodCost = 0f;
    public float housingManufactureEnergyCost = 0.1f;
    public float housingManufactureCostIncrease = 0.4f;

    public float reactorManufactureFoodCost = 0.2f;
    public float reactorManufactureEnergyCost = 0.8f;
    public float reactorManufactureCostIncrease = 0.3f;

    public float batteryManufactureFoodCost = 0.1f;
    public float batteryManufactureEnergyCost = 1f;
    public float batteryManufactureCostIncrease = 0.4f;

    public float extraEngineManufactureFoodCost = 2.5f;
    public float extraEngineManufactureEnergyCost = 13f;
    public float extraEngineManufactureCostIncrease = 10f;
    public float secondsWithoutFood = 0;
    public float seconds = 0;
    public float timeCount = 0;

    [Header("Carts manufacture stats")]
    public float weatherFoodModifier = 1f;
    public float weatherEnergyModifier = 1f;

    [Header("Objects")]
    public GameObject GameOverContainer;
    public DecoupleModal decoupleModal;

    public int cartsTotal = 0;
    private List<Cart> carts;
    private List<Cart> farms;
    private List<Cart> housingUnits;
    private List<Cart> reactors;
    private List<Cart> batteries;
    private List<Cart> extraEngines;

    public float CurrentSpeed { get; set; }
    public float CurrentEnergy { get; set; }
    public float CurrentEnergyStorage { get; set; }
    public float CurrentFood { get; set; }
    public float CurrentFoodStorage { get; set; }
    public int CurrentPassenger { get; set; }
    public int CurrentWorkers { get; set; }
    public int CurrentUnemployed { get; set; }
    public int CurrentUnemployable { get; set; }

    public float CurrentFarmFoodCost { get; set; }
    public float CurrentFarmEnergyCost { get; set; }
    public float CurrentHousingFoodCost { get; set; }
    public float CurrentHousingEnergyCost { get; set; }
    public float CurrentReactorFoodCost { get; set; }
    public float CurrentReactorEnergyCost { get; set; }
    public float CurrentBatteryFoodCost { get; set; }
    public float CurrentBatteryEnergyCost { get; set; }
    public float CurrentExtraEngineFoodCost { get; set; }
    public float CurrentExtraEngineEnergyCost { get; set; }


    public bool TimePaused { get; set; }
    public bool FastForward { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        CurrentWorkers = 0;
        CurrentUnemployed = 0;
        CurrentUnemployable = 0;
        CurrentPassenger = startingPassenger;
        CurrentEnergy = startingEnergy;
        CurrentEnergyStorage = energyStorage;
        CurrentFood = startingFood;
        CurrentFoodStorage = foodStorage;
        CurrentSpeed = initialSpeed;
        TimePaused = false;

        CurrentFarmFoodCost = farmManufactureFoodCost;
        CurrentFarmEnergyCost = farmManufactureEnergyCost;
        CurrentHousingFoodCost = housingManufactureFoodCost;
        CurrentHousingEnergyCost = housingManufactureEnergyCost;
        CurrentReactorFoodCost = reactorManufactureFoodCost;
        CurrentReactorEnergyCost = reactorManufactureEnergyCost;
        CurrentBatteryFoodCost = batteryManufactureFoodCost;
        CurrentBatteryEnergyCost = batteryManufactureEnergyCost;
        CurrentExtraEngineFoodCost = extraEngineManufactureFoodCost;
        CurrentExtraEngineEnergyCost = extraEngineManufactureEnergyCost;

        farms = new List<Cart>();
        housingUnits = new List<Cart>();
        reactors = new List<Cart>();
        batteries = new List<Cart>();
        extraEngines = new List<Cart>();
        carts = new List<Cart>();
    }

    void Update()
    {
        farms = GetFarms();
        housingUnits = GetHousingUnits();
        reactors = GetReactors();
        batteries = GetBatteries();
        extraEngines = GetExtraEngines();
        cartsTotal = carts.Count;
        if (CurrentSpeed <= minimumSpeed)
        {
            TimePaused = true;
            GameOverContainer.SetActive(true);
        }
        CurrentUnemployed = CurrentPassenger - CurrentWorkers - CurrentUnemployable;
        CurrentWorkers = GetCurrentActiveFarmWorkers() + GetCurrentActiveReactorWorkers() + GetCurrentActiveExtraEngineWorkers();
        if (CurrentPassenger > maxPassengers)
        {
            maxPassengers = CurrentPassenger;
        }

        if (CurrentPassenger - 5 * housingUnits.Count < 0)
        {
            CurrentUnemployable = 0;
        }
        else
        {
            CurrentUnemployable = CurrentPassenger - 5 * housingUnits.Count;
        }

        if (CurrentSpeed > maxSpeed)
        {
            maxSpeed = CurrentSpeed;
        }

        if (GetEnergyProduction() > maxFoodProduction)
        {
            maxFoodProduction = GetEnergyProduction();
        }

        if (GetFoodProduction() > maxEnergyProduction)
        {
            maxEnergyProduction = GetFoodProduction();
        }
        if (CurrentPassenger < 0)
        {
            CurrentPassenger = 0;
        }

        if (RoguelikeManager.Instance.CurrentDestination.weatherChanges.Count > 0)
        {
            foreach (DestinationWeatherData weatherChange in RoguelikeManager.Instance.CurrentDestination.weatherChanges)
            {
                switch (weatherChange.type)
                {
                    case DestinationWeatherType.FOOD:
                        weatherFoodModifier = weatherChange.value;
                        break;
                    default:
                        weatherEnergyModifier = weatherChange.value;
                        break;
                }
            }
        }
        else
        {
            weatherFoodModifier = 1f;
            weatherEnergyModifier = 1f;
        }

        if (TimePaused)
        {
            return;
        }
        if (RoguelikeManager.Instance.CurrentDestination.penalties.Count > 0)
        {
            DisableLastCarts(2);
        }
        AutoAddPassengerHousing();
        timeCount = Time.deltaTime;
        if (FastForward)
        {
            timeCount = Time.deltaTime * 2;
        }
        seconds += timeCount;
        if (seconds >= 60)
        {
            dayCount++;
            seconds = 0;
        }
        CurrentEnergyStorage = GetEnergyStorage();
        CurrentFoodStorage = GetFoodStorage();
        //CurrentFarmFoodCost = farmManufactureFoodCost + farmManufactureCostIncrease * farms.Count;
        CurrentFarmEnergyCost = farmManufactureEnergyCost + farmManufactureCostIncrease * farms.Count;
        CurrentHousingFoodCost = housingManufactureFoodCost + housingManufactureCostIncrease * housingUnits.Count;
        CurrentHousingEnergyCost = housingManufactureEnergyCost + housingManufactureCostIncrease * housingUnits.Count;
        CurrentReactorFoodCost = reactorManufactureFoodCost + reactorManufactureCostIncrease * reactors.Count;
        CurrentReactorEnergyCost = reactorManufactureEnergyCost + reactorManufactureCostIncrease * reactors.Count;
        CurrentBatteryFoodCost = batteryManufactureFoodCost + batteryManufactureCostIncrease * batteries.Count;
        CurrentBatteryEnergyCost = batteryManufactureEnergyCost + batteryManufactureCostIncrease * batteries.Count;
        CurrentExtraEngineFoodCost = extraEngineManufactureFoodCost + extraEngineManufactureCostIncrease * extraEngines.Count;
        CurrentExtraEngineEnergyCost = extraEngineManufactureEnergyCost + extraEngineManufactureCostIncrease * extraEngines.Count;

        if (CurrentEnergy < CurrentEnergyStorage || GetEnergyProduction() < 0)
        {
            CurrentEnergy += GetEnergyProduction() * timeCount;
        }
        if (CurrentEnergy < minimumEnergyStorage)
        {
            CurrentEnergy = minimumEnergyStorage;
        }
        if (CurrentFood < CurrentFoodStorage || GetFoodProduction() < 0)
        {
            CurrentFood += GetFoodProduction() * timeCount;
        }
        if (CurrentFood < minimumFoodStorage)
        {
            CurrentFood = minimumFoodStorage;
        }

        // Will progressivelly slowdown train based on the amount of energy missing
        if (CurrentEnergy < 0)
        {
            CurrentSpeed -= -(CurrentEnergy * timeCount);
        }
        if (CurrentSpeed < GetSpeedOutput() && CurrentEnergy >= 0)
        {
            CurrentSpeed += timeCount + (timeCount * extraEngines.Count);
        }
        if (CurrentSpeed > GetSpeedOutput() && CurrentEnergy > 0)
        {
            CurrentSpeed -= timeCount;
        }

        if (CurrentFood <= 0 && CurrentPassenger > 0)
        {
            secondsWithoutFood += timeCount;
            if (secondsWithoutFood > timePassengerDieWithoutFood)
            {
                secondsWithoutFood = 0;
                if (CurrentUnemployable <= 0)
                {
                    RemoveDeadWorker();
                }
                CurrentPassenger--;
                deadCount++;
            }
        }
    }
    // Game controllers
    public void Play()
    {
        FastForward = false;
        TimePaused = false;
    }

    public void Pause()
    {
        FastForward = false;
        TimePaused = true;
    }

    public void FastForwardTime()
    {
        TimePaused = false;
        FastForward = true;
    }

    // Cart controllers
    public void AddEmptyCart(Cart cart)
    {
        if (cartsTotal == 0 && cartsDecoupled == 0)
        {
            Pause();
            TutorialModalManager.Instance.RenderCartTutorial();
        }
        carts.Add(cart);
        AutoAssignWorkers();
    }

    public int GetCartsTotal()
    {
        return cartsTotal + batteries.Count;
    }

    public void ToggleDisableCart(Cart _cart)
    {
        int workers = _cart.activeWorkers;
        _cart.isDisabled = !_cart.isDisabled;
        if (_cart.isDisabled)
        {
            _cart.activeWorkers = 0;
        }
        AutoAssignWorkers();
    }

    public List<Cart> GetFarms()
    {
        List<Cart> farms = new List<Cart>();
        foreach (Cart cart in carts)
        {
            if (cart.info.type == CartType.FARM)
            {
                farms.Add(cart);
            }
        }
        return farms;
    }

    public List<Cart> GetHousingUnits()
    {
        List<Cart> housing = new List<Cart>();
        foreach (Cart cart in carts)
        {
            if (cart.info.type == CartType.HOUSING)
            {
                housing.Add(cart);
            }
        }
        return housing;
    }

    public List<Cart> GetReactors()
    {
        List<Cart> reactors = new List<Cart>();
        foreach (Cart cart in carts)
        {
            if (cart.info.type == CartType.REACTOR)
            {
                reactors.Add(cart);
            }
        }
        return reactors;
    }

    public List<Cart> GetBatteries()
    {
        List<Cart> batteries = new List<Cart>();
        foreach (Cart cart in carts)
        {
            if (cart.info.type == CartType.BATTERY)
            {
                batteries.Add(cart);
            }
        }
        return batteries;
    }

    public List<Cart> GetExtraEngines()
    {
        List<Cart> extraEngines = new List<Cart>();
        foreach (Cart cart in carts)
        {
            if (cart.info.type == CartType.EXTRA_ENGINE)
            {
                extraEngines.Add(cart);
            }
        }
        return extraEngines;
    }

    // Workers 
    public Cart AddWorkerToCart(Cart cart)
    {
        if (CurrentUnemployed <= 0 || cart.activeWorkers == cart.maxWorkers) { return cart; }
        cart.activeWorkers++;
        return cart;
    }
    public Cart RemoveWorkerFromCart(Cart cart)
    {
        cart.activeWorkers--;
        return cart;
    }

    public int GetCurrentActiveFarmWorkers()
    {
        int count = 0;
        foreach (Cart cart in farms)
        {
            count += cart.activeWorkers;
        }
        return count;
    }
    public int GetCurrentActiveReactorWorkers()
    {
        int count = 0;
        foreach (Cart _reactor in reactors)
        {
            count += _reactor.activeWorkers;
        }
        return count;
    }
    public int GetCurrentActiveExtraEngineWorkers()
    {
        int count = 0;
        foreach (Cart _extraEngine in extraEngines)
        {
            count += _extraEngine.activeWorkers;
        }
        return count;
    }

    public void AutoAssignWorkers()
    {
        foreach (Cart _farm in farms)
        {
            if (_farm.activeWorkers < farmWorkersCapacityPerCart && !_farm.isDisabled)
            {
                AddWorkerToCart(_farm);
            }
        }
        foreach (Cart _reactor in reactors)
        {
            if (_reactor.activeWorkers < reactorWorkersRequiredPerCart)
            {
                AddWorkerToCart(_reactor);
            }
        }
        foreach (Cart _extraEngine in extraEngines)
        {
            if (_extraEngine.activeWorkers < extraEngineWorkersRequiredPerCart)
            {
                AddWorkerToCart(_extraEngine);
            }
        }
    }

    public void AutoAddPassengerHousing()
    {
        int toBeHoused = CurrentUnemployed + CurrentWorkers;
        foreach (Cart _housing in housingUnits)
        {
            toBeHoused -= _housing.activeWorkers;
            while (_housing.activeWorkers < _housing.maxWorkers && toBeHoused > 0)
            {
                toBeHoused--;
                _housing.activeWorkers++;
            }
        }
    }

    public void RemoveDeadWorker()
    {
        foreach (Cart _farm in farms)
        {
            if (_farm.activeWorkers > 0)
            {
                _farm.activeWorkers--;
                return;
            }
        }
        foreach (Cart _reactor in reactors)
        {
            if (_reactor.activeWorkers > 0)
            {
                _reactor.activeWorkers--;
                return;
            }
        }
        foreach (Cart _extraEngine in extraEngines)
        {
            if (_extraEngine.activeWorkers > 0)
            {
                _extraEngine.activeWorkers--;
                return;
            }
        }
    }
    // Productions
    public float GetFarmsOutputProduction()
    {
        float output = 0;
        foreach (Cart _farm in farms)
        {
            if (!_farm.isDisabled && _farm.activeWorkers > 0)
            {
                if (_farm.activeWorkers == farmWorkersCapacityPerCart)
                {
                    output += farmValueAtMaxWorkers;
                }
                output += farmOutput * _farm.activeWorkers;
            }
        }
        return output;
    }

    public float GetActiveFarmCount()
    {
        int count = 0;
        foreach (Cart _farm in farms)
        {
            if (!_farm.isDisabled && _farm.activeWorkers > 0)
            {
                count++;
            }
        }
        return count;
    }

    public float GetReactorsOutputProduction()
    {
        float output = 0;
        foreach (Cart _reactor in reactors)
        {
            if (_reactor.activeWorkers == reactorWorkersRequiredPerCart)
            {
                output += reactorOutput;
            }
        }
        return output;
    }

    public float GetExtraEnginesSpeedOutput()
    {
        float output = 0;
        foreach (Cart _extraEngine in extraEngines)
        {
            if (_extraEngine.activeWorkers == extraEngineWorkersRequiredPerCart)
            {
                output += extraEngineSpeedOutput;
            }
        }
        return output;
    }

    public float GetFoodProduction()
    {
        float totalFoodProduction = GetFarmsOutputProduction() * weatherFoodModifier;
        float totalFoodConsumption = passengerFoodConsumption * CurrentPassenger;

        return totalFoodProduction - totalFoodConsumption;
    }

    public float GetEnergyProduction()
    {
        float energyProduction = (kwPerKmPerS * extraEngines.Count + kwPerKmPerS * CurrentSpeed + GetReactorsOutputProduction() - GetActiveFarmCount() * farmFoodProductionEnergyCost) * weatherEnergyModifier;
        float totalEnergyConsumption = cartsEnergyConsumption * cartsTotal + cartsEnergyConsumption * batteries.Count; // extraEngineEnergyConsumption * extraEngines.Count

        return energyProduction - totalEnergyConsumption;
    }

    public float GetSpeedOutput()
    {
        return initialSpeed + GetExtraEnginesSpeedOutput() + destinationsSpeedModifier * destinationsReached - ((cartsTotal + batteries.Count) / 5) * slowdownAmount;
    }

    // Storages
    public float GetFoodStorage()
    {
        return foodStorage + farmStorageModifier * farms.Count + stopsFoodStorageModifier;
    }

    public float GetEnergyStorage()
    {
        return energyStorage + batteryModifier * batteries.Count + stopsEnergyStorageModifier;
    }


    public void AddPassengers(int count)
    {
        CurrentPassenger += count;
        AutoAssignWorkers();
    }

    public bool CanBuildCart(CartInfo cartInfo)
    {
        switch (cartInfo.type)
        {
            case CartType.FARM:
                if (CurrentFarmEnergyCost > CurrentEnergy) { return false; }
                CurrentEnergy -= CurrentFarmEnergyCost;
                return true;
            case CartType.HOUSING:
                if ((CurrentHousingFoodCost > 0 && CurrentHousingFoodCost > CurrentFood) || CurrentHousingEnergyCost > CurrentEnergy) { return false; }
                CurrentEnergy -= CurrentHousingEnergyCost;
                CurrentFood -= CurrentHousingFoodCost;
                return true;
            case CartType.REACTOR:
                if (CurrentReactorFoodCost > CurrentFood || CurrentReactorEnergyCost > CurrentEnergy) { return false; }
                CurrentEnergy -= CurrentReactorEnergyCost;
                CurrentFood -= CurrentReactorFoodCost;
                return true;
            case CartType.BATTERY:
                if (CurrentBatteryFoodCost > CurrentFood || CurrentBatteryEnergyCost > CurrentEnergy) { return false; }
                CurrentEnergy -= CurrentBatteryEnergyCost;
                CurrentFood -= CurrentBatteryFoodCost;
                return true;
            case CartType.EXTRA_ENGINE:
                if (CurrentExtraEngineFoodCost > CurrentFood || CurrentExtraEngineEnergyCost > CurrentEnergy) { return false; }
                CurrentEnergy -= CurrentExtraEngineEnergyCost;
                CurrentFood -= CurrentExtraEngineFoodCost;
                return true;
            default:
                return false;
        }
    }

    public void ManufactureCart(Cart cart)
    {
        carts[cart.cartNumber] = cart;
        AutoAssignWorkers();
    }

    public void HandleDecouple(int cartNumber)
    {
        int cartsToDecouple = 0;
        int passengerCount = 0;
        foreach (Cart cart in carts)
        {
            if (cart.cartNumber >= cartNumber)
            {
                if (cart.info.type == CartType.HOUSING)
                {
                    passengerCount += cart.activeWorkers;
                }
                cartsToDecouple++;
            }
        }
        decoupleModal.SetData(cartsToDecouple, passengerCount, cartNumber);
    }

    public void ConfirmDecouple(bool keepPassengers, int passengerCount, int cartCount, int cartNumber)
    {
        cartsDecoupled += cartCount;
        foreach (Cart cart in carts)
        {
            if (cart.cartNumber >= cartNumber)
            {
                Destroy(cart.gameObject);
            }
        }
        if (!keepPassengers)
        {
            CurrentPassenger -= passengerCount;
        }
        carts.RemoveAll(cart => cart.cartNumber >= cartNumber);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ClaimStopReward(Stop stop)
    {
        placesVisited++;
        stop.visited = true;
        foreach (Reward reward in stop.rewards)
        {
            switch (reward.type)
            {
                case RewardType.MAX_ENERGY:
                    stopsEnergyStorageModifier += reward.value;
                    break;
                case RewardType.ENERGY:
                    CurrentEnergy += reward.value;
                    break;
                case RewardType.MAX_FOOD:
                    stopsFoodStorageModifier += reward.value;
                    break;
                case RewardType.FOOD:
                    CurrentFood += reward.value;
                    break;
                case RewardType.PASSENGER:
                    CurrentPassenger += (int)reward.value;
                    break;
                case RewardType.CART:
                    CartManager.Instance.SpawnCart();
                    break;
                default:
                    break;
            }
        }
    }
    public void ClaimDestinationRewards(Destination destination)
    {
        foreach (Reward reward in destination.rewards)
        {
            switch (reward.type)
            {
                case RewardType.MAX_ENERGY:
                    stopsEnergyStorageModifier += reward.value;
                    break;
                case RewardType.ENERGY:
                    CurrentEnergy += reward.value;
                    break;
                case RewardType.MAX_FOOD:
                    stopsFoodStorageModifier += reward.value;
                    break;
                case RewardType.FOOD:
                    CurrentFood += reward.value;
                    break;
                case RewardType.PASSENGER:
                    CurrentPassenger += (int)reward.value;
                    break;
                case RewardType.CART:
                    CartManager.Instance.SpawnCart();
                    break;
                default:
                    break;
            }
        }
    }

    public void DisableLastCarts(int count)
    {
        foreach (Cart cart in carts)
        {
            if (cart.cartNumber + count >= carts.Count)
            {
                cart.isDisabled = true;
            }
            else if (!cart.canBeDisabled)
            {
                cart.isDisabled = false;
            }
        }
    }
}
