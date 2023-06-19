using EagleResearch.CIDS.Config;
using EagleResearch.CIDS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

namespace EagleResearch.CIDS.InformationZones
{
    /// <summary>
    /// Static class handling the creation of a new zone via script and editor.
    /// </summary>
    public static class InformationZoneGenrator
    {
        public static GameObject informationZoneOrigin = GameObject.Find("InformationZones"); 

        
        /// <summary>
        /// Generates a new information zone based on InformatioNZoneSpecification provided. Optionally adds a spherical collider.
        /// </summary>
        /// <param name="informationZoneSpecification">The specification of the new zone</param>
        /// <param name="isSpehericalCollider">True if a spherical collider shall be added.</param>
        public static void GenerateNewInformationZone(InformationZoneSpecification informationZoneSpecification, bool isSpehericalCollider)
        {
            GameObject newZone = GameObject.Instantiate(new GameObject(), informationZoneOrigin.transform);
            newZone.transform.localPosition = informationZoneSpecification.location;
            if (isSpehericalCollider)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.parent = informationZoneOrigin.transform;
                sphere.transform.localPosition = Vector3.zero;
                sphere.transform.localScale = new Vector3(informationZoneSpecification.size, informationZoneSpecification.size, informationZoneSpecification.size);
            }

            newZone.tag = "InformationZone";
            newZone.name = informationZoneSpecification.name;
            LocalContextService localContextService = newZone.AddComponent<LocalContextService>();
            localContextService.IUserTask = (ContextConfiguration.UserTask)informationZoneSpecification.userProfile.userTask;
            localContextService.IProfessionalPosition = (ContextConfiguration.ProfessionalPosition)informationZoneSpecification.userProfile.professionalPosition;
        }
        /// <summary>
        /// Generates a new information zone based on InformatioNZoneSpecification provided. Optionally adds a spherical collider. Then returns the Gameobject.
        /// </summary>
        /// <param name="informationZoneSpecification">The specification of the new zone</param>
        /// <param name="isSpehericalCollider">True if a spherical collider shall be added.</param>
        public static GameObject CreateNewInformationZone(InformationZoneSpecification informationZoneSpecification, bool isSpehericalCollider)
        {
            GameObject newZone = GameObject.Instantiate(new GameObject(), informationZoneOrigin.transform);
            newZone.transform.localPosition = informationZoneSpecification.location;
            if (isSpehericalCollider)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.parent = informationZoneOrigin.transform;
                sphere.transform.localPosition = Vector3.zero;
                sphere.transform.localScale = new Vector3(informationZoneSpecification.size, informationZoneSpecification.size, informationZoneSpecification.size);
            }

            newZone.tag = "InformationZone";
            newZone.name = informationZoneSpecification.name;
            LocalContextService localContextService = newZone.AddComponent<LocalContextService>();
            localContextService.IUserTask = (ContextConfiguration.UserTask)informationZoneSpecification.userProfile.userTask;
            localContextService.IProfessionalPosition = (ContextConfiguration.ProfessionalPosition)informationZoneSpecification.userProfile.professionalPosition;
            return newZone;
        }
    }
}
