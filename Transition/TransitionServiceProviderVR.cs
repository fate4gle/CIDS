using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace EagleResearch.ContextManagment.InformationZone
{
   
    public class TransitionServiceProviderVR : TransitionServiceProvider
    {

        public XRRayInteractor[] xRRayInteractor;
        public GameObject manualSelectMenuCanvas;

        // Start is called before the first frame update
        void Start()
        {
            if (transitionManager == null) { transitionManager = this.GetComponent<TransitionManager>(); }

            switch (transitionManager.transitionType)
            {
                case TransitionManager.TransitionType.DistanceIndication:
                    {
                        foreach (XRRayInteractor xRay in xRRayInteractor)
                        {
                            xRay.enabled = false;
                        }
                        manualSelectMenuCanvas.SetActive(true);
                    }
                    break;
                case TransitionManager.TransitionType.TimerIndication:
                    {
                        foreach (XRRayInteractor xRay in xRRayInteractor)
                        {
                            xRay.enabled = false;
                        }
                        manualSelectMenuCanvas.SetActive(true);
                    }
                    break;

                case TransitionManager.TransitionType.ButtonActivation:
                    {
                        foreach (XRRayInteractor xRay in xRRayInteractor)
                        {
                            xRay.enabled = true;
                        }
                        manualSelectMenuCanvas.SetActive(false);
                    }
                    break;
                case TransitionManager.TransitionType.ManualSelect:
                    {
                        foreach (XRRayInteractor xRay in xRRayInteractor)
                        {
                            xRay.enabled = false;
                        }
                        manualSelectMenuCanvas.SetActive(true);
                    }
                    break;
                default:
                    {
                        foreach (XRRayInteractor xRay in xRRayInteractor)
                        {
                            xRay.enabled = false;
                        }
                        manualSelectMenuCanvas.SetActive(false);
                    }
                    break;
            }

            /* To be implemented
            switch (transitionManager.displayMode)
            {
                case TransitionManager.DisplayMode.HUD:
                    {

                    }
                    break;
                case TransitionManager.DisplayMode.Stationary:
                    {

                    }
                    break;

                default:
                    {

                    }
                    break;            
            }
            */
        }
    }
}
