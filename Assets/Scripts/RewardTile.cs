using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RewardTile : MonoBehaviour {
    public Reward reward;
    public Vector3Int position;
    public Vector3 worldPosition;
    public bool visited;

    // public void Init() {
    //     reward = (Reward) Reward.CreateInstance(typeof(Reward));
    // }

    // public void Init(Vector3Int position, Vector3 worldPosition) {
    //     reward = (Reward) Reward.CreateInstance(typeof(Reward));
    //     this.position = position;
    //     this.worldPosition = worldPosition;
    // }

    public void Init(Reward reward) {
        this.reward = Instantiate(reward);
    }

    public void Init(Vector3Int position, Vector3 worldPosition, Reward reward) {
        this.reward = Instantiate(reward);
        this.position = position;
        this.worldPosition = worldPosition;
    }

    public void ModifyTile(Reward reward) {
        this.reward = Instantiate(reward);
    }

    public Reward GetReward() {
        return reward;
    }
}