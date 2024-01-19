using EmployeeTrainningClassLibrary.BusinessLayer;
using EmployeeTrainningClassLibrary.DAL;
using EmployeeTrainningClassLibrary.Models;
using Moq;

namespace NUnitTestForTrainningApplication
{
    [TestFixture]
    public class Tests
    {
        private Mock<ITrainningDAL> _stubTrainningDAL;
        private Mock<IUserDAL> _stubRetrieveUser;
        TrainningService _service;
        UserService _userService;
        LoginModel loginModel;
        [SetUp]
        public void Setup()
        {
            List<Trainning> trainningList = new List<Trainning>()
            {
                new Trainning {TrainingName = "MVC", Description = "MVC Description", Capacity = 6, Deadline = new DateTime(2023, 12, 31), PriorityDepartment = "Development", TrainingId =1},
                new Trainning {TrainingName = "SQL", Description = "SQL Description", Capacity = 6, Deadline = new DateTime(2023, 12, 31), PriorityDepartment = "Development", TrainingId =2},
                new Trainning {TrainingName = "Agile", Description = "Agile Description", Capacity = 6, Deadline = new DateTime(2023, 12, 31), PriorityDepartment = "Development", TrainingId =3},
                new Trainning {TrainingName = "Cloud", Description = "Cloud Description", Capacity = 6, Deadline = new DateTime(2023, 12, 31), PriorityDepartment = "Development", TrainingId =4},
                new Trainning {TrainingName = "Git", Description = "Git Description", Capacity = 6, Deadline = new DateTime(2023, 12, 31), PriorityDepartment = "Development", TrainingId =5},
                new Trainning {TrainingName = "Scrum", Description = "Scrum Description", Capacity = 6, Deadline = new DateTime(2023, 12, 31), PriorityDepartment = "Development", TrainingId =6}

            };
            var userList = new List<LoginModel>
            {
                new LoginModel { Email = "umair@gmail.com", Password = "password1"},
                new LoginModel { Email = "zubair@gmail.com", Password = "password12"},
                new LoginModel { Email = "widaad@gmail.com", Password = "password13"}
            };

            _stubTrainningDAL = new Mock<ITrainningDAL>();
            _stubTrainningDAL
                .Setup(list => list.GetTrainningList())
                .Returns(trainningList);
            _stubTrainningDAL
               .Setup(delete => delete.DeleteTrainningAsync(It.IsAny<int>()))
               .ReturnsAsync(true);

            _stubRetrieveUser = new Mock<IUserDAL>();
            _stubRetrieveUser
                .Setup(userCheck => userCheck.RetrieveUserAsync(It.IsAny<LoginModel>()))
                .ReturnsAsync((LoginModel model) =>
                {
                    var user = userList.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
                    return user != null ? (true, 2, 2) : (false, 0, 0);
                });

            _service = new TrainningService(_stubTrainningDAL.Object);
            _userService = new UserService(_stubRetrieveUser.Object);

        }

        [Test]
        public void TestGetTrainningForAdmin()
        {
            //Arrange

            //Act
            var trainningList = _service.GetTrainningList();

            //Assert
            Assert.IsTrue(6 == trainningList.Count);
        }
        [Test]
        public async Task TestDeleteTainningForAdmin()
        {
            //Arrange
            int trainningIdToDelete = 6;
            //Act
            bool result =  await _service.DeleteTrainningAsync(trainningIdToDelete);

            //Assert
            Assert.IsTrue(result, "Expected the training to be deleted successfully.");
        }
        [Test]
        public async Task TestLogin()
        {
            //Arrange
            loginModel = new LoginModel()
            {
                Email = "umair@gmail.com",
                Password = "password1"
            };
            //Act
             (bool result, int UserId, int RoleId) = await _userService.IsUserExistsAsync(loginModel);

            //Assert
            Assert.IsTrue(result);

        }
    }
}