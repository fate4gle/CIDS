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
    /// Component handling the context management of mutiple information zones.
    /// </summary>
    public class GlobalContextService : ContextConfiguration
    {
        public bool Debug = false;
        List<GameObject> informationZones= new List<GameObject>();
        private void Start()
        {
            informationZones = GameObject.FindGameObjectsWithTag("InformationZone").ToList();
            OnProfessionalPositionChanged();
            OnUserTaskChanged();
            
        }

        public override void OnProfessionalPositionChanged()
        {
            foreach(GameObject obj in informationZones)
            {
                if (obj.GetComponent<ContextConfiguration>() != null)
                {
                    obj.GetComponent<ContextConfiguration>().IProfessionalPosition = this.professionalPosition;
                }                 
            }
        }

        public override void OnUserTaskChanged()
        {
            foreach (GameObject obj in informationZones)
            {
                if (obj.GetComponent<ContextConfiguration>() != null)
                {
                    obj.GetComponent<ContextConfiguration>().IUserTask= this.userTask;
                }
            }
        }

       
        private void Update()
        {
            if (Debug)
            {
                OnProfessionalPositionChanged();
                OnUserTaskChanged();
                Debug= false;
            }
        }
    }
}
