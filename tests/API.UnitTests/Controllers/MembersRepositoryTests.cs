using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.UnitTests.Controllers;

public class MembersRepositoryTests
{
    private AppDbContext _context;
    private IMembersRepository _membersRepository;
    
    [SetUp]
    public void Setup()
    {
        _context = GlobalTestSetup.AppDbContext;
        _membersRepository = new MembersRepository(_context);
    }

    [Test]
    public async Task GetMembersAsync_Valid_ShouldReturnEntities()
    {
        // Arrange & Act
        var members = await _membersRepository.GetMembersAsync();

        // Assert
        Assert.That(members, Is.Not.Null);
        Assert.That(members, Has.Count.EqualTo(10));
    }

    [Test]
    public async Task GetMemberAsync_Valid_ShouldReturnEntities()
    {
        // Arrange & Act
        Member expectedMember = GetTestMember();
        var member = await _membersRepository.GetMemberAsync(expectedMember.Id);

        // Assert
        Assert.That(member, Is.Not.Null);
        Assert.That(member.Id, Is.EqualTo(expectedMember.Id));
    }

    [Test]
    public async Task GetPhotosAsync_Valid_ShouldReturnEntities()
    {
        // Arrange & Act
        Member expectedMember = GetTestMember();
        List<Photo> expectedPhotos = [GetTestPhoto()];
        var photos = await _membersRepository.GetPhotosAsync(expectedMember.Id);

        // Assert
        Assert.That(photos, Is.Not.Null);
        Assert.That(photos.Count, Is.EqualTo(1));
        Assert.That(photos[0].Url, Is.EqualTo(expectedMember.ImageUrl));
    }
    
    [Test]
    public async Task GetPhotosAsync_Invalid_ShouldReturnEmptyList()
    {
        // Arrange & Act
        var photos = await _membersRepository.GetPhotosAsync("");

        // Assert
        Assert.That(photos, Is.Not.Null);
        Assert.That(photos.Count, Is.EqualTo(0));
    }
    
    private static Member GetTestMember()
    {
        return new Member
        {
            Id= "arenita-id",
            Gender= "female",
            BirthDay= DateOnly.Parse("1986-07-22"),
            DisplayName= "Arenita",
            Created= DateTime.Parse("2023-06-24"),
            LastActive= DateTime.Parse("2024-06-21"),
            Description= "Sunt esse aliqua ullamco in incididunt consequat commodo. Nisi ad esse elit ipsum commodo fugiat est ad. Incididunt nostrud incididunt nostrud sit excepteur occaecat.\r\n",
            City= "Greenbush",
            Country= "Martinique",
            ImageUrl= "https://randomuser.me/api/portraits/women/54.jpg"
        };
    }

    private static Photo GetTestPhoto()
    {
        return new Photo()
        {
            Id = 1,
            Url = "https://randomuser.me/api/portraits/women/54.jpg",
            PublicId = "",
            Member = GetTestMember(),
            MemberId = GetTestMember().Id
        };
    }
}
