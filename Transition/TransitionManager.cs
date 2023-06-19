using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace EagleResearch.ContextManagment.InformationZone
{
    [RequireComponent(typeof(TransitionServiceProvider))]
    public class TransitionManager : MonoBehaviour
    {
        [Tooltip("The transitionServiceProvider of the application, use the normal standard for non XR applications, or the XR version for ARVR applications.")]
        public TransitionServiceProvider transitionServiceProvider;
        
        public enum DisplayMode
        {
            Stationary,
            HUD
        };

        [Tooltip("Select the desired display type, where Stationary are virtual monitors in the information zone and HUD is a heads-up display.")]
        public DisplayMode displayMode = DisplayMode.Stationary;


        public enum TransitionType
        {
            DistanceIndication,
            TimerIndication,
            ButtonActivation,
            ManualSelect
        };

        [Tooltip("Select the desired type of transition when a new information zone is entered.")]
        public TransitionType transitionType = TransitionType.DistanceIndication;

        [Tooltip("Attach the collider of the user. Typically, this is attached to the main Camera.")]
        public Collider userCollider;
        [Tooltip("Define the initial Zone where the user is launched in.")]
        public GameObject startZone;
        private GameObject newZone;
        [Tooltip("The current information zone the user is experiencing.")]
        public GameObject currentZone;
        //[Tooltip("The transform of the user. Used to determine the distance between the user and other information zones.")]
        //public Transform userPosition;
        [Tooltip("The distance of the user to the nearest other information zone.")]
        public float distanceToNewZone;
        [Tooltip("The UI panel which is used to display the distance indicator.")]
        public GameObject distanceIndicatorPanel;
        [Tooltip("The UI Text box displaying the distance of a user to the next information zone.")]
        public Text distanceIndicatorText;
        [SerializeField]
        [Tooltip("All GameObjects with the Tag 'InformationZone'. Is filled automatically within void Start().")]
        private GameObject[] informationZones;
        [Tooltip("The timer for a delayed automatic transition if 'TimerIndication' is selected as transition type.")]
        public float timeToTransition = 5f;
        [Tooltip("The UI (Pop-up) Panel which hosts timerIndication textbox.")]
        public GameObject TimerIndicationPanel;
        [Tooltip("The UI Text box displaying the timer until the next information zone is loaded in.")]
        public Text timerIndicationText;

        [Tooltip("Boolean indicaticting if the transitionManager is awaiting an input from the user UI panel.")]
        public bool isWaitingForButtonFeedback = false;
        [Tooltip("The UI panel which shall appear if 'ButtonActivation' is selected and a user enters a new information zone.")]
        public GameObject informationZoneTransitionUserPanel;

        [Tooltip("The dropdown in which a user can manually select an information zone to be activated.")]
        public Dropdown informationZoneSelectDropdown;
        /*
        [Tooltip("The UI text which shows a user the closest information zone. ")]
        public Text handMenuClosestZoneText;
        */

        public bool isTransitionToNewZone = false;




        // Start is called before the first frame update
        void Start()
        {
            informationZones = GameObject.FindGameObjectsWithTag("InformationZone");
            if (currentZone == null)
            {
                currentZone = startZone;
            }
            StartCoroutine(CheckDistanceToZones());
        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == startZone || other.gameObject == currentZone) { return; }

            Debug.Log("Transition triggered.");


            switch (transitionType)
            {
                case TransitionType.DistanceIndication:
                    {
                        StartTransition(other);
                    }
                    break;
                case TransitionType.TimerIndication:
                    {
                        TimerIndicationPanel.SetActive(true);
                        StartCoroutine(StartCountdownToTransition(other, timeToTransition));
                    }
                    break;
                case TransitionType.ButtonActivation:
                    {
                        if (!isWaitingForButtonFeedback)
                        {
                            isWaitingForButtonFeedback = true;
                            StartButtonSelection(other);
                        }
                    }
                    break;
                case TransitionType.ManualSelect:
                    {
                        Debug.Log("New Zone Interaction, no effect since ManualSelect is selected.");
                    }
                    break;
                default:
                    {
                        Debug.Log("Error using Switch case. No correct TransitionType found.");
                    }
                    break;
            }
        }

        /// <summary>
        /// Starts the ButtonSelection process. Sets the user panel active and starts a coroutine waiting for the user feedback.
        /// </summary>
        /// <param name="zoneCollider"> The zone collider of the entered information zone</param>
        public void StartButtonSelection(Collider zoneCollider)
        {
            informationZoneTransitionUserPanel.SetActive(true);
            StartCoroutine(WaitForUserFeedback(zoneCollider));
        }
        /// <summary>
        /// Receives the feedback of the user panel and adjusts the parameteres the coroutine waits for.
        /// </summary>
        /// <param name="feedback">The user feedback, if true, a transition will be triggered.</param>
        public void SendUserFeedback(bool feedback)
        {
            isTransitionToNewZone = feedback;
            isWaitingForButtonFeedback = false;
        }
        /// <summary>
        /// Awaits until the isWatinginForButtonFeedback bool is set to false (must be set externally). 
        /// Then starts the zone transition if isTransitionToNewZone is set to true (must be set externally).
        /// </summary>
        /// <param name="zoneCollider">The collider of the new information zone.</param>
        /// <returns></returns>
        IEnumerator WaitForUserFeedback(Collider zoneCollider)
        {
            yield return new WaitUntil(() => !isWaitingForButtonFeedback);
            if (isTransitionToNewZone)
            {
                StartTransition(zoneCollider);

                Debug.Log("ZoneTransition via button feedback completed.");
            }
            else
            {
                Debug.Log("ZoneTransition via button feedback declined.");
            }
        }
        /// <summary>
        /// Starts the transition to a new information zone, deactivating all childobjects of the old information zone and activates all  childobjects the new information zone. (Excluding the Collider of the zone.)
        /// </summary>
        /// <param name="zoneCollider">The collider of the entered information zone.</param>
        void StartTransition(Collider zoneCollider)
        {
            newZone = zoneCollider.gameObject;
            switch (displayMode)
            {
                case DisplayMode.Stationary:
                    {
                        for (int i = 0; i < currentZone.transform.childCount; i++)
                        {
                            if (currentZone.transform.GetChild(i).gameObject.tag != "SphereIndicator")
                            {
                                currentZone.transform.GetChild(i).gameObject.SetActive(false);
                            }
                            else
                            {
                                currentZone.transform.GetChild(i).gameObject.SetActive(true);
                            }
                        }
                        for (int i = 0; i < newZone.transform.childCount; i++)
                        {
                            if (newZone.transform.GetChild(i).gameObject.tag != "SphereIndicator")
                            {
                                newZone.transform.GetChild(i).gameObject.SetActive(true);
                            }
                            else
                            {
                                newZone.transform.GetChild(i).gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case DisplayMode.HUD:
                    {
                        Debug.Log("DisplayMode.HUD not implemented yet.");
                    }
                    break;

                default:
                    {
                        Debug.Log("DisplayMode Issue detected. No usable displayMode selected.");
                    }
                    break;
            }


            currentZone = newZone;
        }
        

        /// <summary>
        /// Checks <c>if(transitionType == TransitionType.ManualSelect)</c>  and triggers the transition to the next information zone.
        /// </summary>
        public void NewInformationZoneManuallySelected()
        {
            if (transitionType == TransitionType.ManualSelect)
            {
                int i = informationZoneSelectDropdown.value;
                StartTransition(informationZones[i].GetComponent<Collider>());
            }
        }

        public void EnterNewInformationZone(bool isEnterNewZone)
        {
            if (isEnterNewZone)
            {
                //StartTransition
            }
        }
        /// <summary>
        /// Continously checks the distance to the closest other information zone and displays it on the distanceIndicatorText UI field.
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckDistanceToZones()
        {
            Debug.Log("Distance evaluation Started.");
            while (true)
            {
                if (transitionType == TransitionType.DistanceIndication)
                {
                    float dis = 10;
                    foreach (GameObject go in informationZones)
                    {
                        if (Vector3.Distance(go.gameObject.transform.position, transitionServiceProvider.userPosition.transform.position) < dis && go.gameObject != currentZone)
                        {
                            dis = Vector3.Distance(go.gameObject.transform.position, transitionServiceProvider.userPosition.transform.position) - 2.5f;
                        }
                    }
                    distanceToNewZone = dis;
                    distanceIndicatorText.text = distanceToNewZone.ToString("F2");
                }

                yield return new WaitForSeconds(.05f);
            }
        }
        /// <summary>
        /// Initiates and handles the countdown until the transtion to a new information zone is triggered. (Default = 5 seconds)
        /// </summary>
        /// <param name="zoneCollider">The collider of the new information zone.</param>
        /// <param name="transitionDelay">The time delay in seconds.</param>
        /// <returns></returns>
        IEnumerator StartCountdownToTransition(Collider zoneCollider, float transitionDelay)
        {
            float timer = transitionDelay;
            Debug.Log("Timer Started.");

            while (timer >= 0)
            {
                timerIndicationText.text = timer.ToString() + " s";
                yield return new WaitForSeconds(1f);
                Debug.Log("Time to transition: " + timer);
                timer--;
            }
            TimerIndicationPanel.SetActive(false);
            StartTransition(zoneCollider);

            yield return null;
        }
        /// <summary>
        /// Initiates and handles the countdown until the transtion to a new information zone is triggered. (Default = 5 seconds)
        /// </summary>
        /// <param name="zoneCollider">The collider of the new information zone.</param>
        /// <returns></returns>
        IEnumerator StartCountdownToTransition(Collider zoneCollider)
        {
            float timer = 5f;
            Debug.Log("Timer Started.");

            while (timer > 0)
            {
                yield return new WaitForSeconds(1f);
                Debug.Log("Time to transition: " + timer);
                timer--;
            }
            StartTransition(zoneCollider);

            yield return null;
        }
    }
}

