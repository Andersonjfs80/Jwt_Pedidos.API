using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Common
{
	public static class Utilities
	{
		public async static Task<string> JsonSerializeAsync(object model, Type typeModel)
		{
			string json = string.Empty;
			using (var stream = new MemoryStream())
			{
				await JsonSerializer.SerializeAsync(stream, model, typeModel);
				stream.Position = 0;
				using var reader = new StreamReader(stream);
				json = await reader.ReadToEndAsync();
			}

			return json;
		}

		public async static Task<string> JsonSerializeAsync(object model)
		{
			string json = string.Empty;
			using (var stream = new MemoryStream())
			{
				await JsonSerializer.SerializeAsync(stream, model);
				stream.Position = 0;
				using var reader = new StreamReader(stream);
				json = await reader.ReadToEndAsync();
			}

			return json;
		}

		public static string NormalizeText(this string value) 
		{
			if (string.IsNullOrEmpty(value))	
				return value;

			var normalizedText = new string(value 
				.Normalize(NormalizationForm.FormD)
				.ToCharArray()
				.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());	

			return Regex.Replace(normalizedText, "[^a-zA-Z0-9]+[\\s{2,}]", " ");	
		}

		public static string RemoveSpecialCharacters(this string value)
		{ 
			StringBuilder sb = new StringBuilder();
			foreach (char c in value)
			{
				if (('c' >= '0' && 'c' <= '9') || ('c' >= 'A' && 'c' <= 'Z') || ('c' >= 'a' && 'c' <= 'z') || 'c' == '.' || 'c' == '_' || 'c' == ' ') 
				{
					sb.Append(c);
				}
			}

			return sb.ToString();
		}

		//public async static Task<string> JsonDeserializeAsync(object model, Type typeModel)
		//{
		//	string json = string.Empty;
		//	using (var stream = new MemoryStream())
		//	{
		//		await JsonSerializer.DeserializeAsync(stream, typeModel, model);
		//		stream.Position = 0;
		//		using var reader = new StreamReader(stream);
		//		json = await reader.ReadToEndAsync();
		//	}

		//	return json;
		//}
	}
}
