using RG.OrbitalElements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moveRoadster : MonoBehaviour
{
    int semimajorAxisLocation, eccentricityLocation, inclinationLocation, longitudeOfAscendingNodeLocation, periapsisArgumentLocation, trueAnomalyLocation, dataLocation;
    string epochJD = "Epoch JD";
    string stringCapDate = "2019-10-08 00:00:00";
    Vector3 startingPosition = new Vector3(1, 1, 1);
    Vector3 targetPosition = new Vector3(0, 0, 0);
    public List<GameObject> routeList;
    public List<Text> textBoxesList;
    public GameObject route;
    public float speedOfSimulation = 1f;
    public Text epochJDt, speedText;
    public StreamReader strReader;
    public TextAsset txtCos;

    int counter = 0;
    DateTime dateCap;

    void Start()
    {
        dataLocation = 1;
        semimajorAxisLocation = 2;
        eccentricityLocation = 3;
        inclinationLocation = 4;
        longitudeOfAscendingNodeLocation = 5;
        periapsisArgumentLocation = 6;
        trueAnomalyLocation = 8;

        textBoxesList.Add(epochJDt);
        dateCap = DateTime.ParseExact(stringCapDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        StartCoroutine(DoSomething());
    }

    void readCSVData()
    {
        bool endOfFile = false;
        while (!endOfFile)
        {
            string dataString = strReader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }
            var dataValues = dataString.Split(',');

            if (dataValues[0].ToString() != epochJD)
            {
                DateTime dateTime = DateTime.ParseExact(dataValues[dataLocation], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                if (dateTime < dateCap)
                {
                    counter++;
                    {
                            targetPosition.x =
                                  ToSingle(Math.Round(
                                      Calculations.CalculateOrbitalPosition(
                                      double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                      ).x / -1000, 2));
                            targetPosition.x =
                                 ToSingle(Math.Round(
                                      Calculations.CalculateOrbitalPosition(
                                      double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                      ).y / -1000, 2));
                            targetPosition.x =
                                 ToSingle(Math.Round(
                                      Calculations.CalculateOrbitalPosition(
                                      double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                      double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                      ).z / 1000, 2));
                            this.transform.position = targetPosition;
                    }
                }
                else
                {
                    endOfFile = true;
                    break;
                }

            }

        }

    }

    private IEnumerator DoSomething()
    {

        while (true)
        {
            string text = txtCos.text;
            string[] lines = text.Split(System.Environment.NewLine.ToCharArray());
            
            foreach (string line in lines)
            {
                string dataString = line;
                var dataValues = dataString.Split(',');

                if (dataValues[0].ToString() != epochJD)
                {
                    yield return new WaitForSeconds(speedOfSimulation);

                    DateTime dateTime = DateTime.ParseExact(dataValues[dataLocation], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    if (dateTime < dateCap)
                    {
                        if (startingPosition.x == 1 && startingPosition.y == 1 && startingPosition.z == 1)
                        {
                            startingPosition.x = ToSingle(Math.Round(
                                Calculations.CalculateOrbitalPosition(
                                double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                ).x / 10000, 2));
                            startingPosition.y = ToSingle(Math.Round(
                                Calculations.CalculateOrbitalPosition(
                                double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                ).y / 10000, 2));
                            startingPosition.z = ToSingle(Math.Round(
                                 Calculations.CalculateOrbitalPosition(
                                 double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                 double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                 double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                 double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                 double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                 double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                 ).z / 10000, 2));
                        }

                        DateTime dateCSV = DateTime.ParseExact(dataValues[1], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime utcDate = DateTime.SpecifyKind(dateCSV, DateTimeKind.Utc);
                        var localTime = utcDate.ToLocalTime();

                        epochJDt.text = String.Format("Epoch JD: \n{0}\nDate: \n{1}\nSemi-major axis au: \n{2}\nEccentricity: \n{3}\nInclination degrees: \n{4}\nLongitude of asc. node degrees: \n{5}\nArgument of periapsis degrees: \n{6}\nMean Anomaly degrees: \n{7}\nTrue Anomaly degrees: \n{8}",
                        dataValues[0],
                        localTime,
                        dataValues[2],
                        dataValues[3],
                        dataValues[4],
                        dataValues[5],
                        dataValues[6],
                        dataValues[7],
                        dataValues[8]);

                        targetPosition.x =
                              ToSingle(Math.Round(
                                  Calculations.CalculateOrbitalPosition(
                                  double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                  ).x / 10000, 2));
                        targetPosition.y =
                             ToSingle(Math.Round(
                                  Calculations.CalculateOrbitalPosition(
                                  double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                  ).y / 10000, 2));
                        targetPosition.z =
                             ToSingle(Math.Round(
                                  Calculations.CalculateOrbitalPosition(
                                  double.Parse(dataValues[semimajorAxisLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[eccentricityLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[inclinationLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[longitudeOfAscendingNodeLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[periapsisArgumentLocation], System.Globalization.CultureInfo.InvariantCulture),
                                  double.Parse(dataValues[trueAnomalyLocation], System.Globalization.CultureInfo.InvariantCulture)
                                  ).z / 10000, 2));
                        GameObject clone;
                        clone = Instantiate(route, transform.position, transform.rotation);

                        routeList.Add(clone);
                        if (routeList.Count > 19)
                        {
                            Destroy(routeList[0]);
                            routeList.RemoveAt(0);
                        }

                        this.transform.LookAt(new Vector3(0, targetPosition.y, 0));
                        this.transform.position = targetPosition;
                    }
                    else
                    {
                        this.transform.position = startingPosition;
                        break;
                    }
                }
            }
        }
    }
    public static float ToSingle(double value)
    {
        return (float)value;
    }
    public void speedUpSimulation()
    {
        if (speedOfSimulation < 0.2f)
        {
            speedOfSimulation = 0.1f;
        }
        else
        {
            speedOfSimulation = speedOfSimulation - 0.1f;
        }
        speedText.text = String.Format("24h / {0}s",
            ToSingle(Math.Round(speedOfSimulation, 1)));
    }

    public void slowDownSimulation()
    {
        if (speedOfSimulation > 0.89f)
        {
            speedOfSimulation = 1f;
        }
        else
        {
            speedOfSimulation = speedOfSimulation + 0.1f;
        }
        speedText.text = String.Format("24h / {0}s",
            ToSingle(Math.Round(speedOfSimulation, 1)));
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
