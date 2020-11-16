using System.Collections.Generic;

namespace MCHomem.NPoco.Proto.Models.Repositories
{
    interface ICrud<E>
    {
        void Create(E entity);
        void Update(E entity);
        void Delete(E entity);
        E Details(E entity);
        List<E> Retreave(E entity);
    }
}
