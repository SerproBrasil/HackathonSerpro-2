using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBirds.Domain.Entities
{
    public class EntidadeBase : IEntidadeBase
    {
        #region fields and properties

        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        #endregion
    }

    #region interface

    public interface IEntidadeBase
    {
        int Id { get; set; }
    }

    #endregion
}
