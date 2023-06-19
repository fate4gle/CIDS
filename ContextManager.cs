using EagleResearch.CIDS.Config;
using EagleResearch.CIDS.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EagleResearch.CIDS
{
    /// <summary>
    /// Primary component used to change the context of the user.
    /// </summary>
    [RequireComponent(typeof(GlobalContextService))]
    public class ContextManager : MonoBehaviour
    {
        public GlobalContextService globalContextService;

        private void Start()
        {
            globalContextService = GetComponent<GlobalContextService>();
        }
        /// <summary>
        /// Changes the context of the scene based on the provided profesionalPosition and userTask
        /// </summary>
        /// <param name="professionalPosition">The new professionalPosition</param>
        /// <param name="userTask">The new userTask</param>
        public void ChangeContext(ContextConfiguration.ProfessionalPosition professionalPosition, ContextConfiguration.UserTask userTask)
        {
            globalContextService.IProfessionalPosition= professionalPosition;
            globalContextService.IUserTask= userTask;
        }
    }
}
