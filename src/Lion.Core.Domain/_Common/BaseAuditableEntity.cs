namespace Lion.Core.Domain._Common;
public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public BaseEntityState BaseEntityState { get; protected set; }

    public void SetCreatedInformation(DateTime created, string createdBy)
    {
        this.BaseEntityState = BaseEntityState.Active;
        this.Created = created;
        this.CreatedBy = createdBy;
    }
    public void SetLastModifiedInformation(DateTime lastModified, string lastModifiedBy)
    {
        this.LastModified = lastModified;
        this.LastModifiedBy = lastModifiedBy;
    }
}
