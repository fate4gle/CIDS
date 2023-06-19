using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS.Config
{
    /// <summary>
    /// Class containing all information of the user. E.g. The position or the task.
    /// </summary>
    [Serializable]
    public class UserProfile
    {
        public enum ProfessionalPosition
        {
            Technician,
            FloorManager,
            ProductionManager,
            FactoryManager
        }
        [Tooltip("Holds the state, use the IProfessionalPosition to change state via script! Do not change the state via script here!")]
        public ProfessionalPosition professionalPosition;

        /// <summary>
        /// Method to change the ProfessionalPosition state and trigger the <c>OnProfessionalPositionChanged()</c> Method
        /// </summary>
        public ProfessionalPosition IProfessionalPosition
        {
            get { return professionalPosition; }
            set
            {
                professionalPosition = value;
                OnProfessionalPositionChanged();
            }
        }
        public enum UserTask
        {
            Operation,
            Inspection,
            Maintenance,
            Performance
        }


        /// <summary>
        /// Method to change the UserTask state and trigger the <c>OnUserTaskChanged()</c> Method
        /// </summary>
        public UserTask IUserTask
        {
            get { return userTask; }
            set
            {
                userTask = value;
                OnUserTaskChanged();
            }
        }
        [Tooltip("Holds the state, use the IUserTask to change state via script! Do not change the state vai script here!")]
        public UserTask userTask;

        /// <summary>
        /// Method triggered when the <Method>IUserTask</Method> is changed.
        /// </summary>
        public virtual void OnUserTaskChanged()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Method triggered when the <Method>IProfessionalPosition</Method> is changed.
        /// </summary>
        public virtual void OnProfessionalPositionChanged()
        {
            throw new NotImplementedException();
        }
    }
}
