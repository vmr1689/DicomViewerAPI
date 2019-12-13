using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DicomViewerAPI.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByInstanceId(string id);
        TEntity GetByNotesId(long id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }
}
