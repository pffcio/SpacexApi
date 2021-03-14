using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Rocketings;


public class browesController : MonoBehaviour
{
    public GameObject buttonTemplate,shipTemplate,loadingScreen,shipsVert;
    public GameObject popup;
    public Text flightDetails,debugText;
    public Button returnButton,mainMenuButton,shipImage;
    Launches launches;
    Ships ships;
    Rocketing rocket;
    string countryOrigin;

    public Sprite alreadyStarted, willStart;

    public List<Launch> allLaunches;
    public List<Ships> allShips;
    public List<Rocketing> allRockets;


    //public GameObject buttonTemplate;
    private void Start()
    {
        returnButton.onClick.AddListener(hidePopup);
        StartCoroutine(getCapsules());
    }

    public RectTransform cont;



    void drawUI()
    {
        GameObject tempButton;

        foreach (var item in allLaunches)
        {
            tempButton = Instantiate(buttonTemplate, transform);
            tempButton.transform.GetChild(1).GetComponent<Text>().text = String.Format("Mission name:{0}",
                item.MissionName);
                
            foreach(var country in allRockets)
            {
                if (item.Rocket.RocketID == country.RocketId)
                {
                    countryOrigin=country.Country;
                    break;
                }
            }
            tempButton.transform.GetChild(2).GetComponent<Text>().text = String.Format("\nPayloads number: {0}\n\nName of rocket: {1}\n\nCountry of origin: {2}\n",
                item.Rocket.SecondStage.Payloads.Count,
                item.Rocket.RocketName,                
                countryOrigin);

            
            if (item.Ships.Count > 0)
                {
                foreach (var shipItem in item.Ships)
                    {
                        tempButton.GetComponent<listOfShips>().shipList.Add(shipItem.ToString());
                    }
                }

            if (item.Upcoming == false)
            {
                tempButton.transform.GetChild(0).GetComponent<Image>().sprite = alreadyStarted;
            }
            else
            {
                tempButton.transform.GetChild(0).GetComponent<Image>().sprite = willStart;
            }       

        }
        loadingScreen.SetActive(false);
        //Destroy(buttonTemplate);
    }

    IEnumerator getCapsules()
    {
        string urlLaunches = "https://api.spacexdata.com/v3/launches";
        string urlShips = "http://api.spacexdata.com/v3/ships";
        string urlRockets = "https://api.spacexdata.com/v3/rockets/";

        UnityWebRequest requestL = UnityWebRequest.Get(urlLaunches);
        UnityWebRequest requestS = UnityWebRequest.Get(urlShips);
        UnityWebRequest requestR = UnityWebRequest.Get(urlRockets);

        yield return requestL.SendWebRequest();
        yield return requestS.SendWebRequest();
        yield return requestR.SendWebRequest();

        launches = new Launches();
        ships = new Ships();
        rocket = new Rocketing();

        allLaunches = JsonConvert.DeserializeObject<List<Launch>>(requestL.downloadHandler.text);
        allShips = JsonConvert.DeserializeObject<List<Ships>>(requestS.downloadHandler.text);
        allRockets = JsonConvert.DeserializeObject<List<Rocketing>>(requestR.downloadHandler.text);

        if (requestL.isNetworkError)
        {
            SceneManager.LoadScene("mainMenu");
        }
        else
        {
            if (requestL.isDone)
            {
                drawUI();
            }
        }
    }
    public void backToMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void hidePopup()
    {
        foreach (Transform child in shipsVert.transform)
        {
            Destroy(child.gameObject);
        }

        popup.SetActive(false);
        mainMenuButton.interactable = true;
        shipsVert.SetActive(false);
        flightDetails.enabled = false;
    }


    public void showPopup(List<string> shipList)
    {

        GameObject tempButton;
        if (shipList.Count > 0)
        {
            shipsVert.SetActive(true);
            foreach (string ship in shipList)
            {
                tempButton = Instantiate(shipTemplate, shipsVert.transform);

                int counterOfUsage;
                int counterOfShips;

                counterOfShips = 0;

                
                counterOfShips++;
                counterOfUsage = 0;
                foreach (var shipFromJSON in allShips)
                {
                    if (ship == shipFromJSON.ShipId.ToString())
                    {
                        counterOfUsage++;

                        tempButton.transform.GetChild(0).GetComponent<Text>().text = String.Format("Name: {1}, \nType: {2}, \nNumber of missions: {3} \nHome port: {4}",
                        counterOfShips,
                        shipFromJSON.ShipName,
                        shipFromJSON.ShipType,
                        shipFromJSON.Missions.Count.ToString(),
                        shipFromJSON.HomePort);
                        tempButton.transform.GetChild(1).GetComponent<imageButtonController>().ulrForImage = shipFromJSON.Image.ToString();
                    }
                }
            }
        }
        else
        {
            flightDetails.text = "According to API no ships were used during this mission";
            flightDetails.enabled=true;
        }

        popup.SetActive(true);
        mainMenuButton.interactable = false;

    }
}
