public class User
{
    public string Username;
    public string Password;

    public enum UserType
    {
        customer,
        admin
    }

    private UserType Type;

    public User(string username, string password, UserType type = UserType.customer)
    {
        this.Username = username;
        this.Password = password;
        this.Type = type;

    }


    public Boolean IsAdmin(){
        return Type == UserType.admin;
    }

    public override string ToString()
    {
        return this.Username;
    }
}
