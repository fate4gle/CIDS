using EagleResearch.CIDS.Communication;
using EagleResearch.CIDS.Config;
using EagleResearch.CIDS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS.InformationZones
{
    /// <summary>
    /// Primary Component to manage the state of all informationzones
    /// </summary>
    public class InformationZoneManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The current information zone configuration.")]
        public InformationZoneConfiguration informationZoneConfiguration;
        [Tooltip("List of information zones present overall.")]
        public List<GameObject> informationZonesInScene;
        [Tooltip("List of currently available information zone configurations. Theses are already loaded into the application during runtime.")]
        public List<InformationZoneConfiguration> availableInformationZoneConfigurations;
        [Tooltip("Directory path where configurations are saved, starting in the parent directory.")]
        public string configSavePath = "/test/save/";
        [Tooltip("Directory path where configurations are saved, starting in the parent directory.")]
        public string configLoadPath = "/test/load/";
        [Tooltip("The prefix which is added to every file to save/load.")]
        public string saveFilePrefix = "Userprofile_";
        [Tooltip("The userName under which the configurations shall be saved, or the the file be loaded.")]
        public string userName = "Default";

        public List<string> storedUserProfiles= new List<string>();

        void Start()
        {
            informationZonesInScene = GameObject.FindGameObjectsWithTag("InformationZone").ToList();
        }

        /// <summary>
        /// Scans the local save file location for userprofiles and loads them into the storedUserProfiles List.
        /// </summary>
        /// <param name="fileExtension">The extension of the file</param>
        public void ScanForLocalUserProfiles(string fileExtension)
        {
            storedUserProfiles.Clear(); 
            storedUserProfiles = ExternalFileLoader.ScanForFilesAtLocation(configSavePath, fileExtension).ToList();
        }

        /// <summary>
        /// Loads an informationZoneConfiguration from a local .txt file.
        /// </summary>
        public void LoadInformationZoneConfig()
        {
            informationZoneConfiguration.LoadFromMsg(ExternalFileLoader.LoadStringFromFile(configLoadPath, saveFilePrefix + userName));
            availableInformationZoneConfigurations.Add(informationZoneConfiguration);
        }
        /// <summary>
        /// Loads an informationZoneConfig from the configurationString
        /// </summary>
        /// <param name="configurationString">The configurationString</param>
        public void LoadInformationZoneConfig(string configurationString)
        {
            informationZoneConfiguration.LoadFromMsg(configurationString);
            availableInformationZoneConfigurations.Add(informationZoneConfiguration);
        }
        /// <summary>
        /// Loads an informationConfiguration from a local .txt file depending on provided inputs
        /// </summary>
        /// <param name="configLoadPath">The location of the file from parent directory</param>
        /// <param name="saveFilePrefix">The prefix of the file [e.g. "Userprofile_"]</param>
        /// <param name="userName">The name of the user profile. [e.g. "Default" or "Steve"]</param>
        public void LoadInformationZoneConfig(string configLoadPath, string saveFilePrefix, string userName)
        {
            informationZoneConfiguration.LoadFromMsg(ExternalFileLoader.LoadStringFromFile(configLoadPath, saveFilePrefix + userName));
            availableInformationZoneConfigurations.Add(informationZoneConfiguration);
        }

        /// <summary>
        /// Adaptes the name property of the configuration to the current userName. Then saves the current InformationZoneConfiguration to a local .txt file as a JSON string.
        /// </summary>
        public void SaveInformationZoneConfig()
        {
            informationZoneConfiguration.name = userName;
            ExternalFileLoader.WriteStringToFile(configSavePath, saveFilePrefix + userName , informationZoneConfiguration.CreateJsonString());
        }

        /// <summary>
        /// Adaptes the name property of the configuration to the profileName. Then saves the current InformationZoneConfiguration to a local .txt file as a JSON string.
        /// </summary>
        /// <param name="profileName">The name for the userProfile.</param>
        public void SaveInformationZoneConfig(string profileName)
        {
            informationZoneConfiguration.name = profileName;
            ExternalFileLoader.WriteStringToFile(configSavePath, saveFilePrefix + userName, informationZoneConfiguration.CreateJsonString());
        }


        /// <summary>
        /// Sets the userprofile configurations of all informationzones defined in the InfromationZoneConfiguration. If an unknown informationzone is specified, a new gamobject is spawned and configured. 
        /// </summary>
        /// <param name="informationZoneConfiguration"></param>
        public void SetInformationZones(InformationZoneConfiguration informationZoneConfiguration)
        {
            this.informationZoneConfiguration = informationZoneConfiguration;
            for(int i = 0; i<informationZoneConfiguration.informationZoneSpecifications.Length; i++)
            {
                if ( informationZonesInScene.Where(obj => obj.name == informationZoneConfiguration.informationZoneSpecifications[i].name).SingleOrDefault() != null)
                {
                    GameObject go = informationZonesInScene.Where(obj => obj.name == informationZoneConfiguration.informationZoneSpecifications[i].name).SingleOrDefault();
                    go.transform.localPosition = informationZoneConfiguration.informationZoneSpecifications[i].location;
                    LocalContextService localContextService = go.GetComponent<LocalContextService>();
                    localContextService.IUserTask = (ContextConfiguration.UserTask) informationZoneConfiguration.informationZoneSpecifications[i].userProfile.userTask;                    
                    localContextService.IProfessionalPosition = (ContextConfiguration.ProfessionalPosition) informationZoneConfiguration.informationZoneSpecifications[i].userProfile.professionalPosition;
                }
                else
                {
                    informationZonesInScene.Add(InformationZoneGenrator.CreateNewInformationZone(informationZoneConfiguration.informationZoneSpecifications[i], true));
                }
            }
        }
    }
}
