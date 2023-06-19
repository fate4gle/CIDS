using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Net.NetworkInformation;

namespace EagleResearch.CIDS.Communication
{
    /// <summary>
    /// Static class handling writing and reading local files
    /// </summary>
    public static class ExternalFileLoader 
    {

        public static string parentDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        /// <summary>
        /// Loads a local file at the specified location and returns it as a string.
        /// </summary>
        /// <param name="fileLocation"> The location of the file</param>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        public static string LoadStringFromFile(string fileLocation, string fileName)
        {
            string path = parentDirectory + fileLocation + "/" + fileName;

            string readString = "No file found.";
            try
            {
                readString = File.ReadAllText(path);
            }
            catch ( Exception e)
            {
                Debug.Log(e);                
            }
            
            return readString;
        }


        /// <summary>
        /// Writes a string to a local file (overrides or creates new file) at a specified location
        /// </summary>
        /// <param name="fileLocation">The location of the file.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileString">The content of the file.</param>
        public static void WriteStringToFile(string fileLocation, string fileName, string fileString)
        {
            string path = parentDirectory + fileLocation + "/" + fileName;
            using (var stream = File.CreateText(path))
            {
                stream.WriteLine(fileString);
            }
        }


        /// <summary>
        /// Checks for existance of files with specified fileExtension at fileLocation and returns all matching files as a string array.
        /// </summary>
        /// <param name="fileLocation">The location of the file.</param>
        /// <param name="fileFormat">The file extension to search for</param>
        /// <returns></returns>
        public static string[] ScanForFilesAtLocation(string fileLocation, string fileFormat)
        {           
            string path = parentDirectory + fileLocation + "/";
            string[] foundFiles = Directory.GetFiles(path,"*" + fileFormat);

            return foundFiles;
        }
    }
}
