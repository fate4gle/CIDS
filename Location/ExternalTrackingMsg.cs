using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS.Location
{
    /// <summary>
    /// Message containing the data obtained from the external tracking system.
    /// </summary>
    public class ExternalTrackingMsg : CIDSMessage
    {
        public ExternalTrackingMsg() { }

        [Tooltip("The origin of the coordinate system of the external tracking system.")]
        public Vector3 origin;
        [Tooltip("The tracked position of the user based on th external tracking system.")]
        public Vector3 trackedPosition;
    }
}
