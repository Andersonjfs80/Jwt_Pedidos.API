using System;
using Xunit;

namespace xJWT_Pedidos_v1.API
{
    public class UnitTest1
    {
        private string StringOfChar(string value, int characterQuantity)
        {
            string result = "";
            for (int i = 0; i < characterQuantity; i++)
            {
                result += value;
            }

            return result;
        }

        [Fact]
        public void Test1()
        { 


            var teste = StringOfChar(" ", 352);           

            Assert.Equal(352, teste.Length);
            
        }
    }
}
