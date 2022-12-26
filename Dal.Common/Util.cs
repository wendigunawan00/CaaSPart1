using System.Text.RegularExpressions;

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

        public static string createID(string lastID = "prod-1", int count =1)
        {
            //Method 1
            //Regex regex = new Regex("-");  // Split on hyphens.
            //string[] substrings = regex.Split(lastID);
            //string str = "";
            //for (int i = 0; i < substrings.Length - 1; i++) 
            //{
            //    str = str + substrings[i];
            //}
            //str += (Int32.Parse(substrings[substrings.Length]) + 1).ToString();

            //Method 2
            string matches = Regex.Match(lastID, @"(?<![A-z\d])+\d+").Value;
            int matchPos = Regex.Match(lastID, @"(?<![A-z\d])+\d+").Index;

            return lastID.Substring(0,matchPos) + (count + 1).ToString();
           
        }
    }
}
