using FluentAssertions;
using Xunit;
using XUnitSample.Infrastructure.Services;

namespace XUnitSample.Tests;

public class StudentTest
{
    [Fact]
    public void Student_Initialize_ContainsCourses()
    {
        var sut = new Student();

        sut.Courses.Should().HaveCount(3);
        sut.Courses.Should().OnlyHaveUniqueItems();
        sut.Courses.Should().NotBeNull();
        sut.Courses.Should().NotBeNullOrEmpty();
    }
}
