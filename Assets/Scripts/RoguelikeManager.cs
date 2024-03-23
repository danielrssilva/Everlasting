using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TileManager))]
public class RoguelikeManager : MonoBehaviour
{
    public static RoguelikeManager Instance;

    public Destination CurrentDestination { get; set; }
    public List<Destination> NextDestinations { get; set; }
    public Stop NextStop { get; set; }
    public List<Stop> Stops { get; set; }
    private int stopsGenerated;
    private int destinationsGenerated;

    public GameObject chooseDestinationCanvas;
    public DestinationOption destinationPrefab;
    private TileManager tileManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        tileManager = GetComponent<TileManager>();
    }

    void Start()
    {
        stopsGenerated = 0;
        destinationsGenerated = 1;
        NextDestinations = new List<Destination>();
        List<Reward> rewards = new List<Reward>();
        int randomRewardsAmount = Random.Range(3, 6);
        for (int i = 0; i < randomRewardsAmount; i++)
        {
            rewards.Add(GetRandomDestinationReward());
        }
        // GameObject newDestination = new GameObject();
        // newDestination.AddComponent(typeof(Destination));
        CurrentDestination = new Destination();
        CurrentDestination.SetData(9f, "// São Paulo", 23.5558f, 46.6396f, NormalizeRewards(rewards), new List<ExtraRewardData>());

        Stops = GenerateRandomStops();
        NextStop = Stops[0];
        int iposition = 7;
        foreach (Stop _stop in Stops)
        {
            Vector3Int position = new Vector3Int(0, iposition, 0);
            tileManager.CreateTile(position, _stop);
            iposition -= 3;
        }
        stopsGenerated += Stops.Count;

        Vector3Int destinationTile = new Vector3Int(9, 4, 0);
        tileManager.CreateDestinationTile(destinationTile, CurrentDestination);
    }

    public void ReachCurrentDestination()
    {
        TrainManager.Instance.Pause();
        chooseDestinationCanvas.SetActive(true);
        if (destinationsGenerated == 1)
        {
            TutorialModalManager.Instance.RenderDestinationTutorial();
        }
        if (NextDestinations.Count > 0) { return; }
        TrainManager.Instance.ClaimDestinationRewards(CurrentDestination);
        int destinationsCount = Random.Range(2, 4);
        foreach (Transform child in GameObject.FindGameObjectWithTag("AvailableDestinations").transform)
        {
            Destroy(child.gameObject);
        }
        // Rewards
        for (int i = 0; i < destinationsCount; i++)
        {
            List<Reward> rewards = new List<Reward>();
            int randomRewardsAmount = Random.Range(3, 6);
            for (int j = 0; j < randomRewardsAmount; j++)
            {
                rewards.Add(GetRandomDestinationReward());
            }
            // GameObject newDestination = new GameObject();
            // newDestination.AddComponent(typeof(Destination));
            Destination destination = new Destination();
            List<ExtraRewardData> extraRewards = new List<ExtraRewardData>();
            List<DestinationPenaltyData> penalties = new List<DestinationPenaltyData>();
            List<DestinationWeatherData> weatherChanges = new List<DestinationWeatherData>();
            int randomChance = Random.Range(1, 100);
            if (randomChance > 85)
            {
                penalties.Add(GetPenalty());
            }
            randomChance = Random.Range(1, 100);
            if (randomChance > 95 || (penalties.Count > 0 && randomChance > 70))
            {
                extraRewards.Add(GetExtraReward());
            }
            randomChance = Random.Range(1, 100);
            if (randomChance >= 60)
            {
                weatherChanges.Add(GetWeatherCondition());
            }
            int stopsCount = Random.Range(4, 16);
            float dd = Random.Range(10f, 49f);
            float dms = Random.Range(50f, 99f);
            destination.SetData(9f, "// Destination " + (destinationsGenerated + i), dd, dms, NormalizeRewards(rewards), extraRewards, penalties, weatherChanges, stopsCount);
            NextDestinations.Add(destination);
        }
        destinationsGenerated += destinationsCount;

        foreach (Destination destination in NextDestinations)
        {
            destinationPrefab.Init(destination);
        }
        foreach (Stop stop in Stops)
        {
            stop.visited = true;
        }
    }

    public void ChooseDestination(Destination destination)
    {
        CurrentDestination = destination;
        TrainManager.Instance.DisableLastCarts(0);
        NextDestinations = new List<Destination>();
        chooseDestinationCanvas.SetActive(false);
        TrainManager.Instance.destinationsReached++;

        Stops = new List<Stop>();
        Stops = GenerateRandomStops(destination.stopsCount);
        NextStop = Stops[0];
        int i = 7;
        foreach (Stop _stop in Stops)
        {
            Vector3Int position = new Vector3Int(17 * TrainManager.Instance.destinationsReached, i, 0);
            tileManager.CreateTile(position, _stop);
            i -= 3;
        }

        Vector3Int destinationTile = new Vector3Int((17 * TrainManager.Instance.destinationsReached) + 10, 4, 0);
        tileManager.CreateDestinationTile(destinationTile, destination);
        TrainManager.Instance.Play();
        stopsGenerated += Stops.Count;
    }

    public List<Stop> GenerateRandomStops(int stopsCount = 5)
    {
        Stops = new List<Stop>();
        for (int i = 0; i < stopsCount; i++)
        {
            List<Reward> rewards = new List<Reward>();
            rewards.Add(GetRandomStopReward());
            // GameObject newStop = new GameObject();
            // newStop.AddComponent(typeof(Stop));
            Stop stop = new Stop();
            stop.SetData("Stop " + (stopsGenerated + i), rewards);
            Stops.Add(stop);
        }
        return Stops;
    }

    public Reward GetRandomStopReward()
    {
        // GameObject newReward = new GameObject();
        // newReward.AddComponent(typeof(Reward));
        Reward reward = new Reward();
        // Destroy(newReward);

        RewardType rewardType = RewardType.CART;
        int rewardValue = 1;
        int randomRewardTypeNumber = Random.Range(0, 2);

        switch (randomRewardTypeNumber)
        {
            case 1:
                rewardType = RewardType.PASSENGER;
                rewardValue = Random.Range(1, 7);
                break;
            default:
                break;
        }
        reward.SetData(rewardValue, rewardType);
        return reward;
    }

    public ExtraRewardData GetExtraReward()
    {
        ExtraRewardData extraRewardData = new ExtraRewardData();

        extraRewardData.label = "Engine upgrade";
        return extraRewardData;
    }

    public DestinationPenaltyData GetPenalty()
    {
        DestinationPenaltyData penaltyData = new DestinationPenaltyData();

        penaltyData.label = "Disables the last <b>2</b> carts until this destination is reached";
        penaltyData.type = DestinationPenaltyType.CART_DISABLER;
        return penaltyData;
    }

    public DestinationWeatherData GetWeatherCondition()
    {
        DestinationWeatherData destinationWeather = new DestinationWeatherData();

        int randomRewardTypeNumber = Random.Range(1, 3);
        float value = Random.Range(0.88f, 1.5f);
        switch (randomRewardTypeNumber)
        {
            case 1:
                destinationWeather.label = string.Format("Food output <b>{0:N0}<size=5>%</size></b> effective", value * 100);
                destinationWeather.type = DestinationWeatherType.FOOD;
                destinationWeather.value = value;
                break;
            case 2:
                destinationWeather.label = string.Format("Energy output <b>{0:N0}<size=5>%</size></b> effective", value * 100);
                destinationWeather.type = DestinationWeatherType.ENERGY;
                destinationWeather.value = value;
                break;
            default:
                break;
        }
        return destinationWeather;
    }

    public Reward GetRandomDestinationReward()
    {
        // GameObject newReward = new GameObject();
        // newReward.AddComponent(typeof(Reward));
        Reward reward = new Reward();
        // Destroy(newReward);

        RewardType rewardType = RewardType.CART;
        float rewardValue = 1f;
        int randomRewardTypeNumber = Random.Range(1, 6);

        switch (randomRewardTypeNumber)
        {
            case 1:
                rewardType = RewardType.MAX_ENERGY;
                rewardValue = Random.Range(0.1f, 0.2f);
                break;
            case 2:
                rewardType = RewardType.ENERGY;
                rewardValue = Random.Range(0.1f, 1f);
                break;
            case 3:
                rewardType = RewardType.MAX_FOOD;
                rewardValue = Random.Range(0.1f, 0.2f);
                break;
            case 4:
                rewardType = RewardType.FOOD;
                rewardValue = Random.Range(0.2f, 0.4f);
                break;
            case 5:
                rewardType = RewardType.PASSENGER;
                rewardValue = (float)Random.Range(1, 5);
                break;
            case 6:
                rewardType = RewardType.CART;
                rewardValue = 1f;
                break;
            default:
                break;
        }
        reward.SetData(rewardValue, rewardType);
        return reward;
    }

    public List<Reward> NormalizeRewards(List<Reward> rewards)
    {
        List<Reward> normalizedRewards = new List<Reward>();
        float maxEnergy = 0;
        float energy = 0;
        float maxFood = 0;
        float food = 0;
        float passenger = 0;
        float cart = 0;
        foreach (Reward reward in rewards)
        {
            switch (reward.type)
            {
                case RewardType.MAX_ENERGY:
                    maxEnergy += reward.value;
                    break;
                case RewardType.ENERGY:
                    energy += reward.value;
                    break;
                case RewardType.MAX_FOOD:
                    maxFood += reward.value;
                    break;
                case RewardType.FOOD:
                    food += reward.value;
                    break;
                case RewardType.PASSENGER:
                    passenger += reward.value;
                    break;
                case RewardType.CART:
                    cart += reward.value;
                    break;
                default:
                    break;
            }
        }
        Reward maxEnergyReward = new Reward(maxEnergy, RewardType.MAX_ENERGY);
        Reward energyReward = new Reward(energy, RewardType.ENERGY);
        Reward maxFoodReward = new Reward(maxFood, RewardType.MAX_FOOD);
        Reward foodReward = new Reward(food, RewardType.FOOD);
        Reward passengerReward = new Reward(passenger, RewardType.PASSENGER);
        Reward cartReward = new Reward(cart, RewardType.CART);
        if (maxEnergy > 0)
        {
            normalizedRewards.Add(maxEnergyReward);
        }
        if (energy > 0)
        {
            normalizedRewards.Add(energyReward);
        }
        if (maxFood > 0)
        {
            normalizedRewards.Add(maxFoodReward);
        }
        if (food > 0)
        {
            normalizedRewards.Add(foodReward);
        }
        if (passenger > 0)
        {
            normalizedRewards.Add(passengerReward);
        }
        if (cart > 0)
        {
            normalizedRewards.Add(cartReward);
        }
        return normalizedRewards;
    }
}
