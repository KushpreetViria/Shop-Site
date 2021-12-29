using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public class CurrencyMath
    {
        public static decimal addCurrency(params decimal[] numbers){
			decimal total = 0;
			foreach(decimal num in numbers){
				total += num*100;
			}
			return total/100;
		}

		public static decimal substractCurrency(decimal original, params decimal[] numbers){
			decimal total = original;
			foreach(decimal num in numbers){
				total -= num*100;
			}
			return total/100;
		}
    }
}