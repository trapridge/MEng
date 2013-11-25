namespace ProjectWork
{
    public class LocalizedStrings
    {
        /// <summary>
        /// Note that we do not need to specify the namespace when creating the instance
        /// </summary>
        private static L10N localizedResources = new L10N();

        /// <summary>
        /// Gets of AppResources class instance
        /// </summary>
        public L10N L10N
        {
            get { return localizedResources; }
        }
    }
}

