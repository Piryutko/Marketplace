using Microsoft.Extensions.DependencyInjection;
using UserRegistrationService.Data;
using UserRegistrationService.Models;
using UserRegistrationService.Repositories;

namespace Tests;

[TestClass]
public class UserRegistrationServiceTest
{

    private UserRepositoryLocal _userRepository;

    public UserRegistrationServiceTest()
    {   
        _userRepository = new UserRepositoryLocal();
    }

    [TestMethod]
    [DataRow("John","Malkovich","SuperStar","04.04.1994 0:00:00","john@gmail.com")]
    [DataRow("Zik","Zak","ZikoZuko","01.01.2003 0:00:00","zikoziku@gmail.com")]
    public void ShouldCreateUser(string name, string surname, string nickname, string datebirth, string email)
    {
        //prepare
        var convertedDatebirth = Convert.ToDateTime(datebirth);
        var user = new User(name, surname, nickname, convertedDatebirth, email);

        //act
        var isCreate = _userRepository.TryCreateUser(user);

        //validation
        var expectedUserCount = 1;
        var expectedResult = true;
        var users = _userRepository.GetAllUsers();

        Assert.AreEqual(expectedUserCount, users.Count());
        Assert.AreEqual(expectedResult, isCreate);

        foreach (var testUser in users)
        {
            Assert.AreEqual(name, testUser.Name);
            Assert.AreEqual(surname, testUser.Surname);
            Assert.AreEqual(nickname, testUser.Nickname);
            Assert.AreEqual(datebirth, testUser.Datebirth.ToString());
            Assert.AreEqual(email, testUser.Email);
        }

    }

    [TestMethod]
    [DataRow("12345","34567","SuperStar","04.04.1994 0:00:00","  ")]
    [DataRow("34567","444","ZikoZuko","01.01.2003 0:00:00","  ")]
    public void TryCreateUser_ThrowFalse(string name, string surname, string nickname, string datebirth, string email)
    {
        //prepare
        var convertedDatebirth = Convert.ToDateTime(datebirth);
        var user = new User(name, surname, nickname, convertedDatebirth, email);

        //act

        var isCreate = _userRepository.TryCreateUser(user);

        //validation
        var expectedUserCount = 0;
        var expectedResult = false;
        var users = _userRepository.GetAllUsers();

        Assert.AreEqual(expectedUserCount, users.Count());
        Assert.AreEqual(expectedResult, isCreate);

    }
}