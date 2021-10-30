using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveAndLoad : MonoBehaviour
{
    PlayerHealth hp;

    private float posX = 0;
    private float posY = 0;
    private float posZ = 0;

    void Start() {
        hp = GetComponent<PlayerHealth>();
        Debug.Log("health = " + hp.health);
        // Debug.Log("health = " + hp.GetHealth());
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        posZ = this.transform.position.z;
    }

    public void Save() {
        // use playerPrefs to save health
        PlayerPrefs.SetInt("Health", hp.health);
        PlayerPrefs.SetFloat("Player xPos", this.transform.position.x);
        PlayerPrefs.SetFloat("Player yPos", this.transform.position.y);
        PlayerPrefs.SetFloat("Player zPos", this.transform.position.z);
    }

    public void Load() {
        // use playerPrefs to load health
        hp.health = PlayerPrefs.GetInt("Health", 100);
        posX = PlayerPrefs.GetFloat("Player xPos", 0);
        posY = PlayerPrefs.GetFloat("Player yPos", 0);
        posZ = PlayerPrefs.GetFloat("Player zPos", 0);
        this.transform.position = new Vector3(posX, posY, posZ);
    }
}