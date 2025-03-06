namespace TDDTestingMVC.Models
{
    public class AreaCirculo
    {
        public int Radio { get; set; }

        public double CalcularArea()
        {
            return Math.PI * Math.Pow(Radio, 2);
        }
    }
}