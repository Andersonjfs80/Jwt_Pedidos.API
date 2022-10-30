using System.ComponentModel.DataAnnotations;

namespace Domain.ValidationAttributes
{
	public class ValidarCPFCNPJ: ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return true;
		}
	}
}
