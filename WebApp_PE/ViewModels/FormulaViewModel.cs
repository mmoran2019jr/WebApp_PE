namespace WebApp_PE.ViewModels
{
    public class FormulaViewModel
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal Cantidad { get; set; }

        public List<FormulaMaterialViewModel> Materiales { get; set; } = new();
    }

    public class FormulaMaterialViewModel
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal Cantidad { get; set; }
    }
}
