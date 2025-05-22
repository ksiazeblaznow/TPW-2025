using System.Collections.ObjectModel;
using System.Numerics;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void Repository_InitializesWithEmptyBallCollection()
        {
            Repository repository = new Repository();
            Assert.IsNotNull(repository.Balls);
            Assert.AreEqual(0, repository.Balls.Count);
        }

        [TestMethod]
        public void Repository_CanAddAndRemoveBalls()
        {
            Repository repository = new Repository();
            Ball ball = new Ball(new Vector2(1, 2), 2.0f, new Vector2(1, 1));

            repository.Balls.Add(ball);
            Assert.AreEqual(1, repository.Balls.Count);
            Assert.AreSame(ball, repository.Balls[0]);

            repository.Balls.Remove(ball);
            Assert.AreEqual(0, repository.Balls.Count);
        }

        [TestMethod]
        public void Repository_ProvidesLockObject()
        {
            Repository repository = new Repository();
            object lockObj = repository.Lock;

            Assert.IsNotNull(lockObj);

            lock (lockObj)
            {
                // If no exception, locking works
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Repository_SameLockObjectReturnedEachTime()
        {
            Repository repository = new Repository();
            object first = repository.Lock;
            object second = repository.Lock;

            Assert.AreSame(first, second);
        }
    }
}
