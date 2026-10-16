using HotelSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Базовый контроллер с общей реализацией CRUD-операций
/// </summary>
public abstract class BaseCrudController<TEntity, TResponse, TCreateRequest, TUpdateRequest>(
    ICrudService<TEntity> service,
    ILogger logger) : ControllerBase
    where TEntity : class
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<TResponse>> GetAll()
    {
        var entities = service.GetAll();

        logger.LogInformation(
            "Получен список сущностей типа {EntityType}. Количество: {Count}",
            typeof(TEntity).Name,
            entities.Count);

        return Ok(entities.Select(ToResponse).ToList());
    }

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TResponse> GetById(int id)
    {
        var entity = service.GetById(id);

        if (entity is null)
        {
            logger.LogWarning(
                "Сущность типа {EntityType} с ID {Id} не найдена.",
                typeof(TEntity).Name,
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Получена сущность типа {EntityType} с ID {Id}.",
            typeof(TEntity).Name,
            id);

        return Ok(ToResponse(entity));
    }

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] TCreateRequest request)
    {
        var validationResult = ValidateCreateRequest(request);

        if (validationResult is not null)
        {
            return validationResult;
        }

        var entity = MapCreateRequest(request);
        var createdEntity = service.Add(entity);
        var id = GetEntityId(createdEntity);

        logger.LogInformation(
            "Создана сущность типа {EntityType} с ID {Id}.",
            typeof(TEntity).Name,
            id);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            ToResponse(createdEntity));
    }

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] TUpdateRequest request)
    {
        var existingEntity = service.GetById(id);

        if (existingEntity is null)
        {
            logger.LogWarning(
                "Не удалось обновить сущность типа {EntityType}: ID {Id} не найден.",
                typeof(TEntity).Name,
                id);

            return NotFound();
        }

        var validationResult = ValidateUpdateRequest(id, request);

        if (validationResult is not null)
        {
            return validationResult;
        }

        var updatedEntity = MapUpdateRequest(id, request);

        if (!service.Update(id, updatedEntity))
        {
            logger.LogWarning(
                "Не удалось обновить сущность типа {EntityType} с ID {Id}.",
                typeof(TEntity).Name,
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Обновлена сущность типа {EntityType} с ID {Id}.",
            typeof(TEntity).Name,
            id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        var deleted = service.Delete(id);

        if (deleted)
        {
            logger.LogInformation(
                "Удалена сущность типа {EntityType} с ID {Id}.",
                typeof(TEntity).Name,
                id);
        }
        else
        {
            logger.LogWarning(
                "Не удалось удалить сущность типа {EntityType} с ID {Id}: сущность не найдена.",
                typeof(TEntity).Name,
                id);
        }

        return NoContent();
    }

    /// <summary>
    /// Преобразует сущность в DTO ответ
    /// </summary>
    protected abstract TResponse ToResponse(TEntity entity);

    /// <summary>
    /// Преобразует DTO создания в сущность
    /// </summary>
    protected abstract TEntity MapCreateRequest(TCreateRequest request);

    /// <summary>
    /// Преобразует DTO обновления в сущность
    /// </summary>
    protected abstract TEntity MapUpdateRequest(int id, TUpdateRequest request);

    /// <summary>
    /// Возвращает идентификатор сущности
    /// </summary>
    protected abstract int GetEntityId(TEntity entity);

    /// <summary>
    /// Проверяет данные перед созданием сущности
    /// </summary>
    protected virtual IActionResult? ValidateCreateRequest(TCreateRequest request)
        => null;

    /// <summary>
    /// Проверяет данные перед обновлением сущности
    /// </summary>
    protected virtual IActionResult? ValidateUpdateRequest(
        int id,
        TUpdateRequest request)
        => null;
}