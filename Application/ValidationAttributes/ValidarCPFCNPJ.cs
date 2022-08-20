using System.ComponentModel.DataAnnotations;

namespace Application.ValidationAttributes
{
	public class ValidarCPFCNPJ: ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			return true;
		}
	}
}
