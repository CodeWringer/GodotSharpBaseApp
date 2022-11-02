using app.business.dataaccess.util;
using NUnit.Framework;

namespace test.app.business.dataaccess.util
{
    public class PathUtilityTest
    {
        [Test]
        public void IsFilePathValid_Windows()
        {
            // Given
            string path = "C:/temp/ab c/data";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsTrue(r);
        }

        [Test]
        public void IsFilePathValid_WindowsWithFile()
        {
            // Given
            string path = "C:/temp/ab c/data.json";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsTrue(r);
        }

        [Test]
        public void IsFilePathValid_User()
        {
            // Given
            string path = "user://a/bc/def_gh i/ijk";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsTrue(r);
        }

        [Test]
        public void IsFilePathValid_UserWithFile()
        {
            // Given
            string path = "user://a/bc/def_gh i/ijk/data.png";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsTrue(r);
        }

        [Test]
        public void IsFilePathValid_Unix()
        {
            // Given
            string path = "~/Library/Application Support/Godot/app_userdata/app/abc";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsTrue(r);
        }

        [Test]
        public void IsFilePathValid_UnixWithFile()
        {
            // Given
            string path = "~/Library/Application Support/Godot/app_userdata/app/abc.json";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsTrue(r);
        }

        [Test]
        public void IsFilePathValid_BadFormat()
        {
            // Given
            string path = "bad_format";
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsFalse(r);
        }

        [Test]
        public void IsFilePathValid_EmptyString()
        {
            // Given
            string path = string.Empty;
            // When
            bool r = PathUtility.IsFilePathValid(path);
            // Then
            Assert.IsFalse(r);
        }
    }
}