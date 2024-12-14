using SocialMedia.Core.Entities;
using SocialMedia.Core.ValueObjects;

namespace SocialMedia.Application.Models.ViewModel;

public class AccountViewModel
{
	public AccountViewModel(Guid id, string fullname, string email, DateTime birthdate, Phone phone, DateTime createdAt, DateTime? updatedAt)
	{
		Id = id;
		Fullname = fullname;
		Email = email;
		Birthdate = birthdate;
		Phone = phone.FullNumber;
		CreatedAt = createdAt;
		UpdatedAt = updatedAt;
	}

	public Guid Id { get; set; }
	public string Fullname { get; set; }
	public string Email { get; set; }
	public DateTime Birthdate { get; set; }
	public string Phone { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }

	public static AccountViewModel FromEntity(Account account)
	{
		return new AccountViewModel(
			account.Id, 
			account.Fullname, 
			account.Email, 
			account.Birthdate,
			account.Phone,
			account.CreatedAt, 
			account.UpdatedAt);
	}
}
