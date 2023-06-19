using EagleResearch.CIDS.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS.Services
{
    /// <summary>
    /// Component handling the context management within a single information zone.
    /// </summary>
    public class LocalContextService : ContextConfiguration
    {
        [Tooltip("List of gamobjects where the Object Context Information is set to Technician")]
        public List<GameObject> TechnicianObjects;
        [Tooltip("List of gamobjects where the Object Context Information is set to FloorManager")]
        public List<GameObject> FloorManagerObjects;
        [Tooltip("List of gamobjects where the Object Context Information is set to Production Manager")]
        public List<GameObject> ProductionManagerObjects;
        [Tooltip("List of gamobjects where the Object Context Information is set to Factory Manager")]
        public List<GameObject> FactoryManagerObjects;

        [Tooltip("List of gamobjects where the Object Context Information is set to Operation")]
        public List<GameObject> OperationObjects;
        [Tooltip("List of gamobjects where the Object Context Information is set to Inspection")]
        public List<GameObject> InspectionObjects;
        [Tooltip("List of gamobjects where the Object Context Information is set to Maintenance")]
        public List<GameObject> MaintenanceObjects;
        [Tooltip("List of gamobjects where the Object Context Information is set to Performance")]
        public List<GameObject> PerformanceObjects;




        void Awake()
        {
            ObjectContextInformation[] objectContextInformation = GetComponentsInChildren<ObjectContextInformation>();
            foreach (ObjectContextInformation obi in objectContextInformation)
            {
                obi.gameObject.GetComponent<Canvas>().enabled = false;
                switch (obi.professionalPosition)
                {
                    case ProfessionalPosition.Technician:
                        {

                            TechnicianObjects.Add(obi.gameObject);
                        }
                        break;
                    case ProfessionalPosition.FloorManager:
                        {
                            FloorManagerObjects.Add(obi.gameObject);
                        }
                        break;
                    case ProfessionalPosition.ProductionManager:
                        {
                            ProductionManagerObjects.Add(obi.gameObject);
                        }
                        break;
                    case ProfessionalPosition.FactoryManager:
                        {
                            FactoryManagerObjects.Add(obi.gameObject);
                        }
                        break;
                }

                switch (obi.userTask)
                {
                    case UserTask.Operation:
                        {

                            OperationObjects.Add(obi.gameObject);
                        }
                        break;
                    case UserTask.Inspection:
                        {
                            InspectionObjects.Add(obi.gameObject);
                        }
                        break;
                    case UserTask.Maintenance:
                        {
                            MaintenanceObjects.Add(obi.gameObject);
                        }
                        break;
                    case UserTask.Performance:
                        {
                            PerformanceObjects.Add(obi.gameObject);
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Finds all Canvases available in ChildObjects, deactivates all of them 
        /// and activates the relevant one depending on the ProfessionalPosition State and the current userTask.
        /// </summary>
        public override void OnProfessionalPositionChanged()
        {
            Debug.Log("OnProfessionalPositionChanged");
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.GetComponent<Canvas>() != null)
                {
                    Debug.Log("Setting to False");
                    transform.GetChild(i).gameObject.GetComponent<Canvas>().enabled = false;
                }                
            }
            
            switch (professionalPosition)
            {
                
                case ProfessionalPosition.Technician:
                    {
                        foreach (GameObject go in TechnicianObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().userTask == userTask)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }                           
                        }
                    }
                    break;
                case ProfessionalPosition.FloorManager:
                    {
                        foreach (GameObject go in FloorManagerObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().userTask == userTask)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;

                case ProfessionalPosition.ProductionManager:
                    {
                        foreach (GameObject go in ProductionManagerObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().userTask == userTask)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;
                case ProfessionalPosition.FactoryManager:
                    {
                        foreach (GameObject go in FactoryManagerObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().userTask == userTask)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;
                default:
                    {
                        foreach (GameObject go in TechnicianObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().userTask == userTask)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;         
              }
            
        }
        /// <summary>
        /// Finds all Canvases available in ChildObjects, deactivates all of them 
        /// and activates the relevant one depending on the UserTask State and the current professionalPosition.
        /// </summary>

        public override void OnUserTaskChanged()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.GetComponent<Canvas>() != null)
                {
                    transform.GetChild(i).gameObject.GetComponent<Canvas>().enabled = false;
                }
            }
           
            switch (userTask)
            {
                case UserTask.Operation:
                    {
                        foreach (GameObject go in OperationObjects)
                        {
                            if(go.GetComponent<ObjectContextInformation>().professionalPosition == professionalPosition)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }                            
                        }
                    }
                    break;
                case UserTask.Inspection:
                    {
                        foreach (GameObject go in InspectionObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().professionalPosition == professionalPosition)
                            {                               
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;

                case UserTask.Maintenance:
                    {
                        foreach (GameObject go in MaintenanceObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().professionalPosition == professionalPosition)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;
                case UserTask.Performance:
                    {
                        foreach (GameObject go in PerformanceObjects)
                        {
                            if (go.GetComponent<ObjectContextInformation>().professionalPosition == professionalPosition)
                            {
                                go.GetComponent<Canvas>().enabled = true;
                                go.SetActive(false);
                            }
                        }
                    }
                    break;
                
            }
        }

        public void ActivateZone()
        {

        }
    }
}
