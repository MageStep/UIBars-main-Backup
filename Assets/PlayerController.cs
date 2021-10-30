// handles collision with enemies and firing magic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth hp;

    [SerializeField]
    private Rigidbody magicBullet;

    [SerializeField]
    private Transform hand;

    private PlayerSaveAndLoad save;

    private int hPotionCount = 0;

    private int mPotionCount = 0;

    public TextMeshProUGUI hPotionText;

    public TextMeshProUGUI mPotionText;

    bool canPickupHealth = false;
    bool canPickupMana = false;

    public GameObject ItemPickup;

    GameObject currentPickup;
    

    void Start() {
        if(hp == null) {
            hp = this.GetComponent<PlayerHealth>();
        }

        save = GetComponent<PlayerSaveAndLoad>();
    }

    // this should go in the input manager script.
    void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            Fire();
        }
        hPotionText.text = "= " + hPotionCount;
        mPotionText.text = "= " + mPotionCount;

        if(Input.GetKeyDown(KeyCode.Q) && hPotionCount > 0)
        {
            hPotionCount -= 1;
            hp.ChangeHealth(25);
        }
        if(Input.GetKeyDown(KeyCode.E) && mPotionCount > 0)
        {
            mPotionCount -= 1;
            hp.ChangeMana(25);
        }

        if(Input.GetKeyDown(KeyCode.F) && canPickupHealth == true)
        {
            HealthPickup();
        }

        if(Input.GetKeyDown(KeyCode.F) && canPickupMana == true)
        {
            ManaPickup();
        }
    }

    void Fire() {
        if(hp.GetMana() > 5) {
            hp.ChangeMana(-5);
            Rigidbody copy = Instantiate(magicBullet, hand.position, hand.rotation);
            copy.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse); // shoot bullet forward
            //copy.GetComponent<BulletController>().owner = hp;
            Destroy(copy.gameObject, 2);
        }
    }

    void OnTriggerEnter(Collider other) {
        // Debug.Log("I've hit " + other.gameObject.tag);
        if(other.gameObject.CompareTag("Enemy")) {
            // Debug.Log("I've hit an enemy!");
            //hp.ChangeHealth(-10);
        }
        else if(other.gameObject.CompareTag("HealthPotion")) {
            ItemPickup.SetActive(true);
            canPickupHealth = true;
            currentPickup = other.gameObject;
            // play drink audio clip
        }
        else if(other.gameObject.CompareTag("ManaPotion")) {
            ItemPickup.SetActive(true);
            canPickupMana = true;
            currentPickup = other.gameObject;
            // play drink audio clip
        }
        else if(other.gameObject.CompareTag("Checkpoint")) {
            // call the Save() function.
            save.Save();
        }
    }

    void OnTriggerExit(Collider other)
    {
        ItemPickup.SetActive(false);
        canPickupMana = false;
        canPickupHealth = false;
        currentPickup = null;
    }

    void HealthPickup()
    {
        
        hPotionCount += 1;
        Debug.Log("You have picked up the potion");
        Destroy(currentPickup);
        ItemPickup.SetActive(false);
        canPickupHealth = false;
        
    }

    void ManaPickup()
    {
        mPotionCount += 1;
        Debug.Log("You have picked up the potion");
        Destroy(currentPickup);
        ItemPickup.SetActive(false);
        canPickupMana = false;
    }
    
}