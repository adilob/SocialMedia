using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Core.Entities;

public class Account : Entity
{
	public Account(string fullname, string password, string email, DateTime birthdate, Phone phone)
		: this(fullname, password, email, birthdate)
	{
		Phone = phone;
	}

	public Account(string fullname, string password, string email, DateTime birthdate)
	{
		Fullname = fullname;
		Password = password;
		Email = email;
		Birthdate = birthdate;
	}

	public string Fullname { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public DateTime Birthdate { get; private set; }
	public Phone Phone { get; private set; }
	public List<Profile> Profiles { get; set; }

	public void Update(string fullname, string email, DateTime birthdate, Phone phone)
	{
		Fullname = fullname;
		Email = email;
		Birthdate = birthdate;
		Phone = phone;
		UpdatedAt = DateTime.Now;
	}

	public void ChangePassword(string password)
	{
		Password = password;
	}
}
