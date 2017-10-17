using connectingToDBTESTING.Models;
using System.Collections.Generic;
namespace connectingToDBTESTING.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}