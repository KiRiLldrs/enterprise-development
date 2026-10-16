namespace HotelSystem.Application.Services;

/// <summary>
/// Определяет общие CRUD-операции для сервисов приложения
/// </summary>
public interface ICrudService<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public List<TEntity> GetAll();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public TEntity? GetById(int id);

    /// <summary>
    /// Добавляет сущность
    /// </summary>
    public TEntity Add(TEntity entity);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    public bool Update(int id, TEntity entity);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    public bool Delete(int id);
}