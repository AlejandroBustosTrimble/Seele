namespace RAK.Fwk.Common.DependencyInjection
{
    /// <summary>
    /// Accesor al motor de inyeccion de dependencia
    /// </summary>
    public static class DIEngineContainer
    {
        private static DIEngine engine;

        /// <summary>
        /// Instancia singleton
        /// </summary>
        public static DIEngine Instance
        {
            get
            {
                if(engine == null)
                {
                    engine = new DIEngine();
                }

                return engine;
            }
        }
    }
}
