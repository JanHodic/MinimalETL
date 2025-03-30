using MinimalETL.Server.Models.Bases;
using System;
namespace MinimalETL.Server.Contracts.Features
{
    /// <summary>
    /// Service reading files
    /// </summary>
    public interface IFileReader<T> where T : class
    {
        /// <summary>
        /// reading csv file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Returns content of csv file</returns>
        public Task<ICollection<T>> ReadCSVFile(string path);

        /// <summary>
        /// reading xml file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Returns content of csv file</returns>
        public Task<ICollection<T>> ReadXMLFile(string path);
        /// <summary>
        /// reading txt file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Returns content of txt file</returns>
        public Task<ICollection<T>> ReadTXTFile(string path);
    }
}

