using System.Security.Claims;
using API.Controllers;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace API.UnitTests.Controllers;

public class MembersControllerTests
{
    private MembersController _membersController;
    private IMembersRepository _mockMembersRepository;

    private const int NOT_FOUND = 404;
    private const string NOT_FOUND_ERROR_MESSAGE = "Expected NotFoundObjectResult but got something else";
    private const string OK_ERROR_MESSAGE = "Expected OkObjectResult but got something else";

    [SetUp]
    public void Setup()
    {
        _mockMembersRepository = Substitute.For<IMembersRepository>();
        _membersController = new MembersController(_mockMembersRepository);






        var userId = "userId";
        DefaultHttpContext testHttpContext = new()
        {
@@ -36,32 +35,20 @@
        {
            HttpContext = testHttpContext
        };
    }

    [Test]
    public async Task GetMembers_Valid_ReturnMembers()
    {
        // Arrange
        IReadOnlyList<Member> expectedMembers = [GetTestMember() ];














        _mockMembersRepository.GetMembersAsync().Returns(expectedMembers);

        // Act & Assert
        var membersResult = await _membersController.GetMembers();
        var okResult = membersResult.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null, OK_ERROR_MESSAGE);

        var members = okResult.Value as IReadOnlyList<Member>;
        Assert.That(members, Is.Not.Null);
@@ -75,33 +62,7 @@
    public async Task GetMember_Valid_ReturnMembers()
    {
        // Arrange
        Member expectedMember = GetTestMember();



























        _mockMembersRepository.GetMemberAsync(expectedMember.Id).Returns(expectedMember);

@@ -123,19 +84,45 @@
    public async Task GetMember_Valid_ReturnNotFound()
    {
        // Arrange
        Member expectedMember = GetTestMember();

        _mockMembersRepository.GetMemberAsync(expectedMember.Id).ReturnsNull();

        // Act & Assert
        var memberResult = await _membersController.GetMember(expectedMember.Id);
        var notFoundResult = memberResult.Result as NotFoundResult;
        Assert.That(notFoundResult, Is.Not.Null, NOT_FOUND_ERROR_MESSAGE);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(NOT_FOUND), NOT_FOUND_ERROR_MESSAGE);

        var member = memberResult.Value;
        Assert.That(member, Is.Null);
    }

    [Test]
    public async Task GetPhotos_Valid_ReturnPhotos()
    {
        // Arrange
        var expectedMember = GetTestMember();
        IReadOnlyList<Photo> expectedPhotos = [GetTestPhoto() ];

        _mockMembersRepository.GetPhotosAsync(expectedMember.Id).Returns(expectedPhotos);

        // Act & Assert
        var photosResult = await _membersController.GetPhotos(expectedMember.Id);
        var okResult = photosResult.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null, OK_ERROR_MESSAGE);

        var photos = okResult.Value as IReadOnlyList<Photo>;
        Assert.That(photos, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(photos, Has.Count.EqualTo(1));
        });
    }

    private static Member GetTestMember()
    {
        return new Member
        {
            Id = "test-id",
            BirthDay = DateOnly.Parse("2000-01-01"),
@@ -150,15 +137,17 @@
            User = null!,
            Photos = []
        };
    }

    private static Photo GetTestPhoto()
    {
        return new Photo()
        {
            Id = 1,
            Url = "Url",
            PublicId = "",
            Member = GetTestMember(),
            MemberId = GetTestMember().Id
        };
    }
}