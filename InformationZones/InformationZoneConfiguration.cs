using EagleResearch.CIDS.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS.InformationZones
{
    /// <summary>
    /// Message which contains the configuration of an entire information zone setup
    /// </summary>
    [Serializable]
    public class InformationZoneConfiguration : CIDSMessage
    {
        [Tooltip("The name of the infromation zone configuration.")]
        public string name = "someName";
        [Tooltip("List of all zones and their subsequent configurations present within the information zone setup.")]
        public InformationZoneSpecification[] informationZoneSpecifications;
    }

    /// <summary>
    /// Message which contains the specification of an individual information zone
    /// </summary>
    [Serializable]
    public class InformationZoneSpecification : CIDSMessage
    {
        [SerializeField]
        [Tooltip("The name of the individual information zone.")]
        public string name = "someZone";
        [SerializeField]
        [Tooltip("The local(!) position of the individual information zone in reference to the origin of the information zones.")]
        public Vector3 location = Vector3.zero;
        [SerializeField]
        [Tooltip("Size of the zone in [m] of the individual information zone.")]
        public float size = 1.0f;
        [SerializeField]
        [Tooltip("The user profile to applied to the individual information zone.")]
        public UserProfile userProfile;
    }
    
}
