using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RandomPowerUp : MonoBehaviour, IInteractable
{
    [SerializeField] RandomPUp[] listOfPowerUps;
    [SerializeField] float timeToLoop = 0.5f;

    int selectedIndex;
    RandomPUp selectedPowerUp;
    float timer;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ChangeSelected();
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;

        if (timer >= timeToLoop)
        {
            timer = 0;
            ChangeSelected();
        }

    }


    private void ChangeSelected()
    {
        
        selectedIndex = UnityEngine.Random.Range(0, listOfPowerUps.Length);
        selectedPowerUp = listOfPowerUps[selectedIndex];
        sr.sprite = selectedPowerUp.image;
    }


    public void Interact(GameObject agent)
    {
        switch (selectedPowerUp.name)
        {
            case "MachineGun":
                agent.GetComponent<SpaceshipHandler>().AddMachineGunPowerUp();
                break;
            case "IMAFIRINGUPMAHLAZOR":
                 agent.GetComponent<SpaceshipHandler>().AddLazerPowerUp();
                break;
            case "Shotgun":
                 agent.GetComponent<SpaceshipHandler>().AddShotgunPowerUp();
                break;
        }
       
        Destroy(gameObject);
    }




    [Serializable]
    private class RandomPUp
    {
        public Sprite image;
        public string name;
    }
    
}
