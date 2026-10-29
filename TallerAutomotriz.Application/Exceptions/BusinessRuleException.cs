namespace TallerAutomotriz.Application.Exceptions
{
    /// <summary>
    /// Se lanza cuando una operación viola una regla del negocio
    /// (ej. orden sin servicios, borrar un cliente que tiene vehículos).
    /// Los controladores la convierten en un mensaje para el usuario (MVC)
    /// o en una respuesta 400/409 (API).
    /// </summary>
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}
