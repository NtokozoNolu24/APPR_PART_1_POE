
namespace GiftoftheGivers.Functions
{
    internal class FunctionAttribute : Attribute
    {
        private string v;

        public FunctionAttribute(string v)
        {
            this.v = v;
        }
    }
}