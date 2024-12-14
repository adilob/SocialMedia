
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.ValueObjects;

public class Address : ValueObjectBase
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Neighbourhood { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }

    public Address(string street, string number, string city, string state, string neighbourhood, string region, string country, string zipCode)
    {
		Street = street;
		Number = number;
		City = city;
		State = state;
		Neighbourhood = neighbourhood;
		Region = region;
		Country = country;
		ZipCode = zipCode;
	}

	[NotMapped]
    public string FullAddress => $"{Street} {Number}, {City}, {State}, {Neighbourhood}, {Region}, {Country}, {ZipCode}";

	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Street;
		yield return Number;
		yield return City;
		yield return State;
		yield return Neighbourhood;
		yield return Region;
		yield return Country;
		yield return ZipCode;
	}
}