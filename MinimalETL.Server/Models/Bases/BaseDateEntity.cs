namespace MinimalETL.Server.Models.Bases
{
    public class BaseDateEntity : BaseIdentity
    {
        /// <summary>
        /// When was it created
        /// </summary>
        public DateTimeOffset Created { get; set; }
        /// <summary>
        /// When was it last modified
        /// </summary>
        public DateTimeOffset Updated { get; set; }
        /// <summary>
        /// When was it deleted
        /// </summary>
        public DateTimeOffset Deleted { get; set; }
    }
}
