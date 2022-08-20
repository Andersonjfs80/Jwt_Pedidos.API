using System;

namespace Jwt_Pedidos_v1.API.Middlewares.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ValidarModelStateAttribute : Attribute
	{
		public Type TypeModel { get; set; }

		public ValidarModelStateAttribute(Type typeModel) => TypeModel = typeModel;
	}
}
