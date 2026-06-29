namespace RegistroVentas.WinForms.Models
{
    public class TokenRequest
    {
        public string usuario { get; set; }
        public string rol { get; set; }
    }

    public class TokenResponse
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public int expiresInSeconds { get; set; }
    }

    public class EncryptRequest
    {
        public string valor { get; set; }
    }

    public class EncryptResponse
    {
        public string secreto { get; set; }
    }

    public class RegistrarVentaRequest
    {
        public string operacion { get; set; }
        public string importe { get; set; }
        public string cliente { get; set; }
        public string secreto { get; set; }
    }

    public class RegistroOperacionResponse
    {
        public int pk { get; set; }
        public string operacion { get; set; }
        public decimal importe { get; set; }
        public string cliente { get; set; }
        public string referencia { get; set; }
        public string estatus { get; set; }
    }

    public class PagedRegistrosResponse
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int total { get; set; }
        public RegistroOperacionResponse[] items { get; set; }
    }
}