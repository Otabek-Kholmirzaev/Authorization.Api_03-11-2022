using Authorization.Api.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Authorization.Api.Services;

public class UsersService
{
	private readonly IConfigurationsService _configurationsService;
    private readonly string _filePath;
    public UsersService(IConfigurationsService configurationsService)
    {
        _configurationsService = configurationsService;

        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", _configurationsService.GetFilePaths().UsersJsonPath);
    }

    public void AddUser(UserEntity userEntity)
    {
        var usersList = ReadUsersFromJson();

        userEntity.Id = usersList.Count + 1;

        usersList.Add(userEntity);

        SaveUsersToJson(usersList);
    }

    public List<UserEntity> GetUsers()
    {
        var usersList = ReadUsersFromJson();

        return usersList;
    }

    public UserEntity? GetUserById(int id)
    {
        var usersList = ReadUsersFromJson();

        var user = usersList.FirstOrDefault(u => u.Id == id);

        return user;
    }

    public UserEntity? GetUserByToken(string token)
    {
        var usersList = ReadUsersFromJson();

        var user = usersList.FirstOrDefault(u => u.Token == token);

        return user;
    }

    private List<UserEntity> ReadUsersFromJson()
    {
        var jsonString = File.ReadAllText(_filePath);

        var usersList = JsonConvert.DeserializeObject<List<UserEntity>>(jsonString);

        if (usersList == null) 
            usersList = new List<UserEntity>();

        return usersList;
    }

    private void SaveUsersToJson(List<UserEntity> usersList)
    {
        var jsonWriteString = JsonConvert.SerializeObject(usersList);

        File.WriteAllText(_filePath, jsonWriteString);
    }
}
