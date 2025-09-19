namespace R5A08_TP1.Models.Mapper
{
    public interface IMapper<Entity, DTO>
    {
        DTO? FromEntity(Entity entity);
        Entity? FromDTO(DTO dto);

        IEnumerable<DTO> ToDTO(IEnumerable<Entity> entities)
        {
            return entities.Select(e => FromEntity(e));
        }

        IEnumerable<Entity> ToEntity(IEnumerable<DTO> dtos)
        {
            return dtos.Select(d => FromDTO(d));
        }
    }
}
