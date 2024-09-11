using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Bookly.Models.ViewModels
{
   
    public class AddtoCartVM
    {
        public  IEnumerable<ShoppingCart> shoppingCarts { get; set; }

        public double TotalPrice { get; set; }



    }
}
