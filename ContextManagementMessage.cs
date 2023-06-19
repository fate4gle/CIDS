using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EagleResearch.CIDS
{
    /// <summary>
    /// Message Template
    /// </summary>

    public partial class CIDSMessage 
    {
        public void LoadFromMsg(string savedData)
        {
            JsonUtility.FromJsonOverwrite(savedData, this);
        }
        public string CreateJsonString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
