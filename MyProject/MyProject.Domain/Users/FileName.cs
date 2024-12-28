using Ardalis.SmartEnum;

namespace MyProject.Domain.Users;

public class ProfileType(string name, int value) : SmartEnum<ProfileType>(name, value)
{
    public static readonly ProfileType Admin = new(nameof(Admin), 0);
    public static readonly ProfileType Participant = new(nameof(Participant), 2);
    public static readonly ProfileType Trainer = new(nameof(Trainer), 1);
}