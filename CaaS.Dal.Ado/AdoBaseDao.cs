using CaaS.Dal.Interfaces;
using Dal.Common;

namespace CaaS.Dal.Ado
{
    public class AdoBaseDao : IDao
    {
        protected readonly AdoTemplate template;

        public AdoBaseDao(IConnectionFactory connectionFactory)
        {
            template = Util.createAdoTemplate(connectionFactory) ?? throw new ArgumentNullException(nameof(connectionFactory));
            // not recommended
            //this.template = new AdoTemplate(connectionFactory);
        }
       
    }
}
