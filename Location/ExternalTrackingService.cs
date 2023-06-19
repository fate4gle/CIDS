using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS.Location
{
    /// <summary>
    /// Service provider to handle, apply and send external tracking data.
    /// </summary>
    public class ExternalTrackingService : MonoBehaviour
    {
        GameObject externalOrigin;
        [SerializeField]
        ExternalTrackingMsg externalTrackingMsg;

        private void Start()
        {
            externalOrigin = GameObject.Find("InformationZones");
        }
        public void SetExternalTrackingData(ExternalTrackingMsg externalTrackingMsg)
        {
            this.externalTrackingMsg = externalTrackingMsg;            
        }
        public void ApplyExternalTrackingData(ExternalTrackingMsg externalTrackingMsg)
        {
            this.externalTrackingMsg = externalTrackingMsg;
            externalOrigin.transform.position = this.externalTrackingMsg.origin;
        }
        public void SetTrackedPositon(Vector3 externalPosition)
        {
            externalTrackingMsg.trackedPosition = externalPosition;
        }

        public void SetTrackedPositon(Vector3 externalPosition, bool isRightHandCoordinateSystem)
        {
            externalTrackingMsg.trackedPosition = new Vector3 (externalPosition.x, externalPosition.z,externalPosition.y);
        }
        public Vector3 GetTrackedProsition()
        {
            return externalTrackingMsg.trackedPosition;
        }
        public Vector3 GetTrackedProsition(bool isRightHandCoordinateSystem)
        {
            return new Vector3 (externalTrackingMsg.trackedPosition.x, externalTrackingMsg.trackedPosition.z, externalTrackingMsg.trackedPosition.y);
        }
        public void SetExternalOrigin(Vector3 origin)
        {
            this.externalTrackingMsg.origin = origin;
            externalOrigin.transform.position = externalTrackingMsg.origin;
        }
        public void SetExternalOrigin(Vector3 origin, bool isRightHandCoordinateSystem)
        {
            this.externalTrackingMsg.origin = new Vector3(origin.x, origin.z, origin.y);
            externalOrigin.transform.position = externalTrackingMsg.origin;
        }
        public Vector3 GetExternalOrigin()
        {
            return externalTrackingMsg.origin;
        }
        public Vector3 GetExternalOrigin(bool isRightHandCoordinateSystem)
        {
            return new Vector3(externalTrackingMsg.origin.x, externalTrackingMsg.origin.z, externalTrackingMsg.origin.y);
        }

        public string GetExternalTrackingMsg()
        {
            return externalTrackingMsg.CreateJsonString();
        }

        public void SendExternalTrackingData()
        {
            throw new NotImplementedException();
        }
    }
}
