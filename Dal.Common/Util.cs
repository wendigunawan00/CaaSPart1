namespace Dal.Common
{
    public static  class Util
    {
        public static AdoTemplate? createAdoTemplate( IConnectionFactory connectionFactory)
        {
            // Reflexion method 1
            /*Type type = typeof(AdoTemplate);
            ConstructorInfo ctor = type.GetConstructor(new[] { typeof(IConnectionFactory) });
            object adoTemplate = ctor.Invoke(new object[] { connectionFactory});*/

            // Reflexion method 2
            Type classType = typeof(AdoTemplate);
            object[] constructorParameters = new object[]
            {
            connectionFactory
            };
            return Activator.CreateInstance(classType, constructorParameters) as AdoTemplate;
        }
    }
}
