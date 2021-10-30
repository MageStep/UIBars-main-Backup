using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private UIController ui;

    public int health = 100, mana = 100, xp = 0;

    private float regenTimer = 1, manaRegenInterval = 1;

    private bool waitingToLoseHealth = false;

    PlayerSaveAndLoad save;

    // Start is called before the first frame update
    void Start()
    {
        ui.SetHealthSlider(health);
        ui.SetManaSlider(mana);
        ui.SetXPSlider(xp);
        save = GetComponent<PlayerSaveAndLoad>();
    }

    void Update() {
        // regen mana at 5 per second.
        if(mana < 100) {
            if(regenTimer > manaRegenInterval) {
                mana += 5;
                regenTimer = 0;
                ui.SetManaSlider(mana);
            } else {
                regenTimer += Time.deltaTime;
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Enemy")) {
            if(!waitingToLoseHealth) {
                ChangeHealth(-5);
                StartCoroutine(TouchedByEnemyTimer());
            }
        }
    }

    IEnumerator TouchedByEnemyTimer() {
        waitingToLoseHealth = true;
        yield return new WaitForSeconds(0.5f);
        waitingToLoseHealth = false;
    }

    

    public void ChangeHealth(int byAmount) {
        health += byAmount;

        if(health <= 0) {
            // replace this with something better later.
            // Application.LoadLevel(0);

            // call the loadHealth function();
            save.Load();
        }

        if(health > 100) {
            health = 100;
        }

        ui.SetHealthSlider(health);
    }

    public void ChangeXP(int byAmount) {
        xp += byAmount;
        ui.SetXPSlider(xp);
    }

    public int GetHealth() {
        return health;
    }

    public int GetMana() { 
        return mana; 
    }

    public void ChangeMana(int byAmount) {
        mana += byAmount;
        if(mana > 100) mana = 100;
        ui.SetManaSlider(mana);
    }
}
