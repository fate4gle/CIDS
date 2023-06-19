using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.ContextManagment.InformationZone
{
    
    public partial class TransitionServiceProvider : MonoBehaviour
    {

        [Tooltip("The transform of the user. Used to determine the distance between the user and other information zones.")]
        public Transform userPosition;

        public TransitionManager transitionManager;

        public void UpdateUserPosition(Vector3 userPosition, bool isLocalPosition)
        {
            if (isLocalPosition)
            {
                this.userPosition.localPosition = userPosition;
            }
            else
            {
                this.userPosition.position = userPosition;
            }
        }
        public void UpdateUserPosition(Vector3 userPosition)
        {
            this.userPosition.position = userPosition;
        }


    }
}
