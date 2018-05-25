namespace EssentialTools.Models
{
    public class DefaultDiscountHelper:IDiscountHelper
    {
        public decimal DiscountSize { get; set; }

        public decimal denominator;

        public DefaultDiscountHelper(decimal denominatorParam)
        {
            denominator = denominatorParam;
        }

        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (DiscountSize / denominator * totalParam));
        }
    }
}