using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Core.ValueObjects;

public class Phone : ValueObjectBase
{
	public string CountryCode { get; private set; }
	public string AreaCode { get; private set; }
	public string Number { get; private set; }
	public string Extension { get; private set; }

	[NotMapped]
	public string FullNumber => $"+{CountryCode} ({AreaCode}) {Number} {Extension}";

	public Phone(string countryCode, string areaCode, string number, string extension)
	{
		CountryCode = countryCode;
		AreaCode = areaCode;
		Number = number;
		Extension = extension;
	}

	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return CountryCode;
		yield return AreaCode;
		yield return Number;
		yield return Extension;
	}
}