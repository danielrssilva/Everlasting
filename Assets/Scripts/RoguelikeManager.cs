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
        NextDestinations = new List<Destination>();
        List<Reward> rewards = new List<Reward>();
        int randomRewardsAmount = Random.Range(3, 6);
        for (int i = 0; i < randomRewardsAmount; i++)
        {
            rewards.Add(GetRandomDestinationReward());
        }
        GameObject newDestination = new GameObject();
        newDestination.AddComponent(typeof(Destination));
        CurrentDestination = newDestination.GetComponent<Destination>();
        CurrentDestination.SetData(9f, "// São Paulo", "23.5558", "46.6396", rewards, new List<ExtraRewardData>());

        Stops = GenerateRandomStops();
        NextStop = Stops[0];
        int iposition = 7;
        foreach (Stop _stop in Stops)
        {
            Vector3Int position = new Vector3Int(0, iposition, 0);
            tileManager.CreateTile(position, _stop);
            iposition -= 3;
        }

        Vector3Int destinationTile = new Vector3Int(9, 4, 0);
        tileManager.CreateDestinationTile(destinationTile);
    }

    void Update()
    {

    }

    public void ReachCurrentDestination()
    {
        TrainManager.Instance.Pause();
        chooseDestinationCanvas.SetActive(true);
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
            GameObject newDestination = new GameObject();
            newDestination.AddComponent(typeof(Destination));
            Destination destination = newDestination.GetComponent<Destination>();
            destination.SetData(9f, "// Destination " + i, "23.5558", "46.6396", rewards, new List<ExtraRewardData>());
            NextDestinations.Add(destination);
        }

        foreach (Destination destination in NextDestinations)
        {
            destinationPrefab.Init(destination);
        }
        //Stops = GenerateRandomStops();
    }

    public void ChooseDestination(Destination destination)
    {
        CurrentDestination = destination;
        NextDestinations = new List<Destination>();
        chooseDestinationCanvas.SetActive(false);
        TrainManager.Instance.destinationsReached++;

        Stops = new List<Stop>();
        Stops = GenerateRandomStops();
        NextStop = Stops[0];
        int i = 7;
        foreach (Stop _stop in Stops)
        {
            Vector3Int position = new Vector3Int(17 * TrainManager.Instance.destinationsReached, i, 0);
            tileManager.CreateTile(position, _stop);
            i -= 3;
        }

        Vector3Int destinationTile = new Vector3Int((17 * TrainManager.Instance.destinationsReached) + 10, 4, 0);
        tileManager.CreateDestinationTile(destinationTile);
        TrainManager.Instance.Play();
    }

    public List<Stop> GenerateRandomStops()
    {
        Stops = new List<Stop>();
        for (int i = 0; i < 5; i++)
        {
            List<Reward> rewards = new List<Reward>();
            rewards.Add(GetRandomStopReward());
            GameObject newStop = new GameObject();
            newStop.AddComponent(typeof(Stop));
            Stop stop = newStop.GetComponent<Stop>();
            stop.SetData("Name " + rewards[0].type.ToString() + rewards[0].value.ToString(), rewards);
            Stops.Add(stop);
        }
        return Stops;
    }

    public Reward GetRandomStopReward()
    {
        GameObject newReward = new GameObject();
        newReward.AddComponent(typeof(Reward));
        Reward reward = newReward.GetComponent<Reward>();
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
        reward.SetData(rewardValue.ToString(), rewardType);
        return reward;
    }

    public Reward GetRandomDestinationReward()
    {
        GameObject newReward = new GameObject();
        newReward.AddComponent(typeof(Reward));
        Reward reward = newReward.GetComponent<Reward>();
        // Destroy(newReward);

        RewardType rewardType = RewardType.CART;
        float rewardValue = 1f;
        int randomRewardTypeNumber = Random.Range(1, 6);

        switch (randomRewardTypeNumber)
        {
            case 1:
                rewardType = RewardType.MAX_ENERGY;
                rewardValue = Random.Range(0.1f, 1f);
                break;
            case 2:
                rewardType = RewardType.ENERGY;
                rewardValue = Random.Range(0.1f, 1.3f);
                break;
            case 3:
                rewardType = RewardType.MAX_FOOD;
                rewardValue = Random.Range(0.1f, 0.5f);
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
        reward.SetData(rewardValue.ToString(), rewardType);
        return reward;
    }
}
