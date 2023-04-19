namespace seecreativa_backend.Core
{
    public abstract class UpdateDtoBase<T>
    {
        public abstract T ToEntity(T entity);
    }
}
