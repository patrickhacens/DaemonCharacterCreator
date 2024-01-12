// See https://aka.ms/new-console-template for more information
using Daemon.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Daemon.Converters;

public class PlayerTypeResolver : DefaultJsonTypeInfoResolver
{
	public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
	{
		JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

		Type baseItemType = typeof(Item);
		if (jsonTypeInfo.Type == baseItemType)
		{
			jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
			{
				TypeDiscriminatorPropertyName = "$type",
				IgnoreUnrecognizedTypeDiscriminators = true,
				UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
				DerivedTypes =
				{
					new JsonDerivedType(typeof(Weapon), nameof(Weapon)),
					new JsonDerivedType(typeof(Armor), nameof(Armor))
				}
			};
		}
		Type baseModifierType = typeof(IModifier);
		if (jsonTypeInfo.Type == baseModifierType)
		{
			jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
			{
				TypeDiscriminatorPropertyName = "$of",
				IgnoreUnrecognizedTypeDiscriminators = true,
				UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
				DerivedTypes =
				{
					new JsonDerivedType(typeof(Modifier<StatusType>), nameof(StatusType)),
					new JsonDerivedType(typeof(Modifier<AttributeType>), nameof(AttributeType)),
					new JsonDerivedType(typeof(Modifier<Skill>), nameof(Skill)),
				}
			};
		}

		return jsonTypeInfo;
	}
}